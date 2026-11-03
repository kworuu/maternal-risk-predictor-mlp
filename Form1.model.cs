using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace MaternalRiskPredictor
{
    // Creating, training, evaluating and tuning the network.
    // Uses trainSet, testSet and scaler from Form1.Data.cs without changing them.
    public partial class Form1
    {
        private NetworkTrainer trainer;   // null until Create NN is clicked

        // One record per logged epoch; drawn on the loss chart and exported to CSV
        private List<TrainingRecord> trainingHistory = new List<TrainingRecord>();

        private void WireModelEvents()
        {
            btnCreate.Click += new EventHandler(btnCreate_Click);

            // training runs on a background thread so the window stays responsive
            btnTrain.Click += new EventHandler(btnTrain_Click);
            btnStop.Click += new EventHandler(btnStop_Click);
            bgwTrain.DoWork += new DoWorkEventHandler(bgwTrain_DoWork);
            bgwTrain.ProgressChanged += new ProgressChangedEventHandler(bgwTrain_ProgressChanged);
            bgwTrain.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bgwTrain_RunWorkerCompleted);
            this.FormClosing += new FormClosingEventHandler(Form1_FormClosingModel);
            btnStop.Enabled = false;

            // loss chart + CSV export
            SetupLossChart();
            btnExport.Click += new EventHandler(btnExport_Click);

            // evaluation on the test set
            btnEvaluate.Click += new EventHandler(btnEvaluate_Click);
            ClearEvaluation();

            // classify one patient
            btnPredict.Click += new EventHandler(btnPredict_Click);
            ClearPrediction();

            // tuning grid search (its own background worker; Stop cancels it too)
            btnRunTuning.Click += new EventHandler(btnRunTuning_Click);
            bgwTuning.DoWork += new DoWorkEventHandler(bgwTuning_DoWork);
            bgwTuning.ProgressChanged += new ProgressChangedEventHandler(bgwTuning_ProgressChanged);
            bgwTuning.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bgwTuning_RunWorkerCompleted);
        }

 
        // Runs whenever the data is reloaded or re-split (called from Form1.Data.cs).
        // A network trained on the old split is no longer valid, so it is discarded.
        
        partial void OnDataChanged()
        {
            if (trainer != null)
            {
                trainer = null;
                trainingHistory.Clear();
                RefreshLossChart();
                ClearEvaluation();
                ClearPrediction();
                Log("Data changed: the old network was discarded. Click Create NN again.");
            }
        }


        // Create NN

        private void btnCreate_Click(object sender, EventArgs e)
        {
            if (trainSet == null)
            {
                Warn("Load the CSV and click \"Clean + Split + Normalize\" first.");
                return;
            }

            int hidden = (int)numHidden.Value;
            trainer = new NetworkTrainer(hidden, (int)numSeed.Value);

            // Epoch 0 = the untrained network. It is the first point of the loss curve.
            trainingHistory.Clear();
            TrainingRecord start = MakeRecord();
            trainingHistory.Add(start);
            RefreshLossChart();
            ClearEvaluation();
            ClearPrediction();

            Log(string.Format("Created a {0}-{1}-{2} network with random weights.",
                MaternalData.NumFeatures, hidden, MaternalData.NumClasses));
            Log(string.Format("  Before training: train MSE {0:F5}, test accuracy {1:P1} (random guessing)",
                start.TrainMse, start.TestAccuracy));
            Log("");
            tabMain.SelectedTab = tabLog;
            SetStatus("Network created. Next: Train NN.");
        }

        // Train / Stop

        private class TrainJob
        {
            public int Epochs;      // how many MORE epochs to train
            public int LogEvery;    // log a line every N epochs
        }

        private void btnTrain_Click(object sender, EventArgs e)
        {
            if (trainer == null)
            {
                Warn("Click \"Create NN\" first.");
                return;
            }
            if (bgwTrain.IsBusy) return;

            // Read the controls HERE: the background thread must never touch the UI.
            TrainJob job = new TrainJob();
            job.Epochs = (int)numEpochs.Value;
            job.LogEvery = Math.Max(1, (int)numLogEvery.Value);

            Log(string.Format("Training for {0} epochs ({1} training rows per epoch), logging every {2}...",
                job.Epochs, trainSet.Count, job.LogEvery));
            Log("   Epoch | Train MSE | Test MSE | Train acc | Test acc");

            SetBusy(true);            // locks the other buttons, enables Stop, resets the bar
            tabMain.SelectedTab = tabLog;
            SetStatus("Training...");
            bgwTrain.RunWorkerAsync(job);
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            if (bgwTrain.IsBusy)
            {
                bgwTrain.CancelAsync();   // the loop checks this flag before each epoch
                btnStop.Enabled = false;
                SetStatus("Stopping after the current epoch...");
            }
            if (bgwTuning.IsBusy)
            {
                bgwTuning.CancelAsync();
                btnStop.Enabled = false;
                SetStatus("Stopping the tuning run...");
            }
        }

        // Runs on a background thread: no UI calls here, only ReportProgress.
        private void bgwTrain_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = (BackgroundWorker)sender;
            TrainJob job = (TrainJob)e.Argument;
            int lastPercent = -1;

            for (int i = 1; i <= job.Epochs; i++)
            {
                if (worker.CancellationPending)
                {
                    e.Cancel = true;
                    return;
                }

                trainer.TrainOneEpoch(trainSet);   // one pass over all 361 training rows

                int percent = (int)(100L * i / job.Epochs);
                bool logThis = (i % job.LogEvery == 0) || (i == job.Epochs);

                if (logThis)
                    worker.ReportProgress(percent, MakeRecord());   // record goes to the UI thread
                else if (percent != lastPercent)
                    worker.ReportProgress(percent, null);           // only moves the progress bar
                lastPercent = percent;
            }
        }

        private void bgwTrain_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBar.Value = Math.Min(Math.Max(e.ProgressPercentage, 0), 100);

            TrainingRecord r = e.UserState as TrainingRecord;
            if (r != null)
            {
                trainingHistory.Add(r);
                AddChartPoint(r);
                Log(FormatRecord(r));
                SetStatus(string.Format("Training... epoch {0}", r.Epoch));
            }
        }

        // UI THREAD. Runs once when training finishes, is stopped, or crashes.
        private void bgwTrain_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            SetBusy(false);

            if (e.Error != null)
            {
                Log("Training failed: " + e.Error.Message);
                Warn("Training failed: " + e.Error.Message);
                SetStatus("Training failed.");
                return;
            }

            // If it was stopped between log lines, record where it actually ended.
            TrainingRecord last = trainingHistory[trainingHistory.Count - 1];
            if (last.Epoch != trainer.EpochsTrained)
            {
                last = MakeRecord();
                trainingHistory.Add(last);
                AddChartPoint(last);
                Log(FormatRecord(last));
            }

            if (e.Cancelled)
                Log(string.Format("Stopped by the user at epoch {0}.", trainer.EpochsTrained));
            else
                Log(string.Format("Training finished: {0} epochs in total.", trainer.EpochsTrained));

            Log(string.Format("  Final: train MSE {0:F5}, test MSE {1:F5}, train acc {2:P1}, test acc {3:P1}",
                last.TrainMse, last.TestMse, last.TrainAccuracy, last.TestAccuracy));
            Log("  Click Train NN again to continue from here, or Create NN to start over.");
            Log("  The Loss Curve tab shows the chart; Export Log saves training_log.csv.");
            Log("");
            SetStatus(string.Format("Trained {0} epochs. Test accuracy {1:P1}.",
                trainer.EpochsTrained, last.TestAccuracy));
        }


        private void Form1_FormClosingModel(object sender, FormClosingEventArgs e)
        {
            if (bgwTrain.IsBusy) bgwTrain.CancelAsync();
            if (bgwTuning.IsBusy) bgwTuning.CancelAsync();
        }

        // Loss chart + export training_log.csv


        private const string TrainSeries = "Train MSE";
        private const string TestSeries = "Test MSE";

        // Built in code rather than in the Designer; it takes the place of picLoss.
        private Chart chartLoss;


        private void SetupLossChart()
        {
            chartLoss = new Chart();
            chartLoss.Name = "chartLoss";
            chartLoss.Dock = DockStyle.Fill;
            chartLoss.BackColor = Color.White;
            picLoss.Visible = false;              // the old PictureBox placeholder from the 3.5 plan
            tabLoss.Controls.Add(chartLoss);
            chartLoss.BringToFront();

            ChartArea area = new ChartArea("Loss");
            area.AxisX.Title = "Epoch";
            area.AxisY.Title = "Mean squared error (MSE)";
            area.AxisX.Minimum = 0;
            area.AxisY.Minimum = 0;
            area.AxisY.LabelStyle.Format = "0.000";
            area.AxisX.MajorGrid.LineColor = Color.Gainsboro;
            area.AxisY.MajorGrid.LineColor = Color.Gainsboro;
            chartLoss.ChartAreas.Add(area);

            chartLoss.Titles.Add("Loss per epoch (lower is better)");
            Legend legend = new Legend("Legend");
            legend.Docking = Docking.Top;
            chartLoss.Legends.Add(legend);

            chartLoss.Series.Add(MakeSeries(TrainSeries, Color.FromArgb(31, 119, 180)));
            chartLoss.Series.Add(MakeSeries(TestSeries, Color.FromArgb(255, 127, 14)));
        }

        private static Series MakeSeries(string name, Color color)
        {
            Series s = new Series(name);
            s.ChartType = SeriesChartType.Line;
            s.ChartArea = "Loss";
            s.Legend = "Legend";
            s.BorderWidth = 2;
            s.Color = color;
            s.MarkerStyle = MarkerStyle.Circle;
            s.MarkerSize = 4;
            return s;
        }

        private void RefreshLossChart()
        {
            chartLoss.Series[TrainSeries].Points.Clear();
            chartLoss.Series[TestSeries].Points.Clear();
            foreach (TrainingRecord r in trainingHistory) AddChartPoint(r);
        }

        private void AddChartPoint(TrainingRecord r)
        {
            chartLoss.Series[TrainSeries].Points.AddXY(r.Epoch, r.TrainMse);
            chartLoss.Series[TestSeries].Points.AddXY(r.Epoch, r.TestMse);
        }


        // Saves trainingHistory as CSV, one row per logged epoch. Numbers use a dot as the
        // decimal point (InvariantCulture) so Excel, Python and the professor all read it the same.

        private void btnExport_Click(object sender, EventArgs e)
        {
            if (trainer == null || trainingHistory.Count <= 1)
            {
                Warn("Create and train the network first: there is no training log to export yet.");
                return;
            }

            using (SaveFileDialog dlg = new SaveFileDialog())
            {
                dlg.Title = "Export training log";
                dlg.Filter = "CSV files (*.csv)|*.csv";
                dlg.FileName = "training_log.csv";
                dlg.InitialDirectory = Application.StartupPath;
                if (dlg.ShowDialog(this) != DialogResult.OK) return;

                CultureInfo inv = CultureInfo.InvariantCulture;
                StringBuilder sb = new StringBuilder();
                sb.AppendLine("epoch,train_mse,test_mse,train_accuracy,test_accuracy");
                foreach (TrainingRecord r in trainingHistory)
                {
                    sb.AppendLine(string.Format(inv, "{0},{1:F6},{2:F6},{3:F4},{4:F4}",
                        r.Epoch, r.TrainMse, r.TestMse, r.TrainAccuracy, r.TestAccuracy));
                }

                try
                {
                    File.WriteAllText(dlg.FileName, sb.ToString());
                }
                catch (Exception ex)
                {
                    Warn("Could not save the file: " + ex.Message);
                    return;
                }

                Log(string.Format("Exported {0} rows to {1}", trainingHistory.Count, dlg.FileName));
                Log(string.Format("  Network {0}-{1}-{2}, {3} epochs trained.",
                    MaternalData.NumFeatures, trainer.HiddenNodes, MaternalData.NumClasses, trainer.EpochsTrained));
                Log("");
                SetStatus("Training log exported.");
            }
        }

        // Evaluation (confusion matrix, per-class metrics, baseline)

        private static readonly Color CorrectCell = Color.FromArgb(214, 240, 214);  // diagonal: right answers
        private static readonly Color WrongCell = Color.FromArgb(250, 215, 215);    // off-diagonal, non-zero: mistakes
        private static readonly Color TotalCell = Color.FromArgb(240, 240, 240);

        private void btnEvaluate_Click(object sender, EventArgs e)
        {
            if (trainer == null)
            {
                Warn("Create and train the network first.");
                return;
            }
            if (trainer.EpochsTrained == 0)
                Log("Note: the network is untrained, so these scores are close to random guessing.");

            ConfusionMatrix cm = ConfusionMatrix.Build(trainer, testSet);
            double trainAcc = trainer.ComputeAccuracy(trainSet);
            int majority;
            double baseline = Metrics.MajorityBaseline(trainSet, testSet, out majority);

            ShowConfusion(cm);
            ShowMetrics(cm);

            lblAccuracy.Text = string.Format(
                "Test accuracy: {0:P1} ({1} of {2} correct)   |   Baseline (always \"{3}\"): {4:P1}   |   Train accuracy: {5:P1}",
                cm.Accuracy(), cm.Correct(), cm.Total, MaternalData.ClassNames[majority], baseline, trainAcc);

            LogEvaluation(cm, trainAcc, baseline, majority);
            tabMain.SelectedTab = tabEval;
            SetStatus(string.Format("Evaluated after {0} epochs: test accuracy {1:P1}, macro F1 {2:F3}.",
                trainer.EpochsTrained, cm.Accuracy(), cm.MacroF1()));
        }

        // Rows = actual class, columns = predicted class, plus totals 
        private void ShowConfusion(ConfusionMatrix cm)
        {
            int n = cm.NumClasses;
            dgvConfusion.Columns.Clear();
            dgvConfusion.Rows.Clear();
            AddColumn(dgvConfusion, "colActual", "Actual \\ Predicted");
            for (int k = 0; k < n; k++)
                AddColumn(dgvConfusion, "colPred" + k, MaternalData.ClassNames[k]);
            AddColumn(dgvConfusion, "colTotal", "Total (actual)");

            for (int i = 0; i < n; i++)
            {
                object[] cells = new object[n + 2];
                cells[0] = MaternalData.ClassNames[i];
                for (int j = 0; j < n; j++) cells[j + 1] = cm.Counts[i, j];
                cells[n + 1] = cm.Support(i);
                int r = dgvConfusion.Rows.Add(cells);

                for (int j = 0; j < n; j++)
                {
                    DataGridViewCell c = dgvConfusion.Rows[r].Cells[j + 1];
                    if (i == j) c.Style.BackColor = CorrectCell;
                    else if (cm.Counts[i, j] > 0) c.Style.BackColor = WrongCell;
                }
                dgvConfusion.Rows[r].Cells[n + 1].Style.BackColor = TotalCell;
            }

            object[] totals = new object[n + 2];
            totals[0] = "Total (predicted)";
            for (int j = 0; j < n; j++) totals[j + 1] = cm.PredictedCount(j);
            totals[n + 1] = cm.Total;
            int t = dgvConfusion.Rows.Add(totals);
            dgvConfusion.Rows[t].DefaultCellStyle.BackColor = TotalCell;
            dgvConfusion.Rows[t].DefaultCellStyle.Font = new Font(dgvConfusion.Font, FontStyle.Bold);
            dgvConfusion.ClearSelection();
        }

        // Precision, recall, F1 and support per class, then the macro average
        private void ShowMetrics(ConfusionMatrix cm)
        {
            dgvMetrics.Columns.Clear();
            dgvMetrics.Rows.Clear();
            AddColumn(dgvMetrics, "colClass", "Class");
            AddColumn(dgvMetrics, "colPrecision", "Precision");
            AddColumn(dgvMetrics, "colRecall", "Recall");
            AddColumn(dgvMetrics, "colF1", "F1");
            AddColumn(dgvMetrics, "colSupport", "Support (test rows)");

            for (int k = 0; k < cm.NumClasses; k++)
            {
                dgvMetrics.Rows.Add(MaternalData.ClassNames[k],
                    cm.Precision(k).ToString("0.000"), cm.Recall(k).ToString("0.000"),
                    cm.F1(k).ToString("0.000"), cm.Support(k));
            }
            int r = dgvMetrics.Rows.Add("Macro average",
                cm.MacroPrecision().ToString("0.000"), cm.MacroRecall().ToString("0.000"),
                cm.MacroF1().ToString("0.000"), cm.Total);
            dgvMetrics.Rows[r].DefaultCellStyle.BackColor = TotalCell;
            dgvMetrics.Rows[r].DefaultCellStyle.Font = new Font(dgvMetrics.Font, FontStyle.Bold);
            dgvMetrics.ClearSelection();
        }

        // The same results as text, so they are also in the log (and in screenshots of it)
        private void LogEvaluation(ConfusionMatrix cm, double trainAcc, double baseline, int majority)
        {
            Log(string.Format("Evaluation on the TEST set ({0} rows) after {1} epochs:", cm.Total, trainer.EpochsTrained));
            Log(string.Format("  Test accuracy {0:P1} ({1}/{2}) | train accuracy {3:P1} | baseline (always {4}) {5:P1}",
                cm.Accuracy(), cm.Correct(), cm.Total, trainAcc, MaternalData.ClassNames[majority], baseline));
            Log("  Confusion matrix (rows = actual, columns = predicted: Low, Mid, High):");
            for (int i = 0; i < cm.NumClasses; i++)
            {
                Log(string.Format("    {0,-9} {1,4} {2,4} {3,4}", MaternalData.ClassNames[i],
                    cm.Counts[i, 0], cm.Counts[i, 1], cm.Counts[i, 2]));
            }
            for (int k = 0; k < cm.NumClasses; k++)
            {
                Log(string.Format("  {0,-9} precision {1:F3}  recall {2:F3}  F1 {3:F3}  (support {4})",
                    MaternalData.ClassNames[k], cm.Precision(k), cm.Recall(k), cm.F1(k), cm.Support(k)));
            }
            Log(string.Format("  Macro     precision {0:F3}  recall {1:F3}  F1 {2:F3}",
                cm.MacroPrecision(), cm.MacroRecall(), cm.MacroF1()));
            Log("");
        }

        // Empties the Evaluation tab (new network or new data = old results no longer valid)
        private void ClearEvaluation()
        {
            dgvConfusion.Columns.Clear();
            dgvConfusion.Rows.Clear();
            dgvMetrics.Columns.Clear();
            dgvMetrics.Rows.Clear();
            lblAccuracy.Text = "Train the network, then click Evaluate.";
        }

        private static void AddColumn(DataGridView grid, string name, string header)
        {
            int i = grid.Columns.Add(name, header);
            grid.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
        }

        // L5: Classify one patient

        private static readonly Color[] RiskColors =
        {
            Color.FromArgb(30, 130, 60),    // Low risk: green
            Color.FromArgb(200, 120, 0),    // Mid risk: orange
            Color.FromArgb(200, 30, 30)     // High risk: red
        };

        private void btnPredict_Click(object sender, EventArgs e)
        {
            if (trainer == null || scaler == null)
            {
                Warn("Prepare the data, then create and train the network first.");
                return;
            }
            if (trainer.EpochsTrained == 0)
                Log("Note: the network is untrained, so this prediction is a random guess.");

            // 1. Raw vitals, in the SAME order as the CSV columns (MaternalData.FeatureNames).
            double[] raw =
            {
                (double)numAge.Value,
                (double)numSystolic.Value,
                (double)numDiastolic.Value,
                (double)numBloodSugar.Value,
                (double)numBodyTemp.Value,
                (double)numHeartRate.Value
            };

            // 2. Scale with the min/max learned from the TRAINING set (never refit on new input).
            double[] scaled = scaler.Transform(raw);

            // 3. Forward pass, then ArgMax picks the strongest of the 3 outputs.
            double[] outputs = trainer.Predict(scaled);
            int predicted = NetworkTrainer.ArgMax(outputs);

            lblPrediction.Text = string.Format("{0}  (output {1:F2})",
                MaternalData.ClassNames[predicted], outputs[predicted]);
            lblPrediction.ForeColor = RiskColors[predicted];

            LogPrediction(raw, scaled, outputs, predicted);
            SetStatus("Predicted: " + MaternalData.ClassNames[predicted] + ".");
        }

        private void LogPrediction(double[] raw, double[] scaled, double[] outputs, int predicted)
        {
            Log("Prediction for one patient:");
            List<string> outside = new List<string>();
            for (int i = 0; i < MaternalData.NumFeatures; i++)
            {
                Log(string.Format("  {0,-12} raw {1,7:0.##}  ->  scaled {2:0.000}",
                    MaternalData.FeatureNames[i], raw[i], scaled[i]));
                if (scaled[i] < 0 || scaled[i] > 1) outside.Add(MaternalData.FeatureNames[i]);
            }
            Log(string.Format("  Outputs: Low {0:F3}, Mid {1:F3}, High {2:F3}  ->  {3}",
                outputs[0], outputs[1], outputs[2], MaternalData.ClassNames[predicted]));

            // Scaled values outside 0..1 mean the input is outside what the network saw in training.
            if (outside.Count > 0)
                Log("  Warning: outside the training range for " + string.Join(", ", outside) +
                    ". The network never saw values like these, so treat this answer with caution.");
            if (raw[MaternalData.HeartRateIndex] < MaternalData.MinPossibleHeartRate)
                Log("  Warning: a heart rate below 40 bpm is the kind of value removed as impossible during cleaning.");
            Log("  (This is a class project, not a medical tool.)");
            Log("");
        }

        private void ClearPrediction()
        {
            lblPrediction.Text = "Enter the vitals and click Run NN.";
            lblPrediction.ForeColor = SystemColors.ControlText;
        }


        // Tuning (grid search over hidden nodes x epochs, repeated runs)
        // Filled by the background thread, read by the UI thread only after it finishes.
        private List<TuningResult> tuningResults = new List<TuningResult>();

        private void btnRunTuning_Click(object sender, EventArgs e)
        {
            if (trainSet == null)
            {
                Warn("Load the CSV and click \"Clean + Split + Normalize\" first.");
                return;
            }
            if (bgwTuning.IsBusy || bgwTrain.IsBusy) return;

            List<int> hiddenList, epochList;
            string error;
            if (!Tuning.TryParseList(txtHiddenList.Text, 1, 100, out hiddenList, out error))
            {
                Warn("Hidden nodes list: " + error);
                return;
            }
            if (!Tuning.TryParseList(txtEpochList.Text, 1, 100000, out epochList, out error))
            {
                Warn("Epochs list: " + error);
                return;
            }

            TuningJob job = new TuningJob();
            job.HiddenList = hiddenList;
            job.EpochList = epochList;
            job.Repeats = (int)numRepeats.Value;
            job.Seed = (int)numSeed.Value;

            int maxEpochs = epochList[epochList.Count - 1];
            long totalEpochs = (long)hiddenList.Count * job.Repeats * maxEpochs;
            Log(string.Format("Tuning: hidden {{{0}}} x epochs {{{1}}}, {2} runs each.",
                string.Join(", ", hiddenList), string.Join(", ", epochList), job.Repeats));
            Log(string.Format("  Each run trains a NEW network to {0} epochs and is measured at every listed epoch,", maxEpochs));
            Log(string.Format("  so this is {0} runs and {1:N0} epochs in total. The split stays the same (seed {2}).",
                hiddenList.Count * job.Repeats, totalEpochs, job.Seed));
            Log("  The network on the Training tab is not changed.");

            tuningResults = new List<TuningResult>();
            dgvTuning.Columns.Clear();
            dgvTuning.Rows.Clear();
            lblTuningNote.Text = "Tuning is running...";

            SetBusy(true);
            tabMain.SelectedTab = tabTuning;
            SetStatus("Tuning...");
            bgwTuning.RunWorkerAsync(job);
        }

        // BACKGROUND THREAD. Trains every (hidden, repeat) network and scores it at each listed epoch
        private void bgwTuning_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = (BackgroundWorker)sender;
            TuningJob job = (TuningJob)e.Argument;
            int maxEpochs = job.EpochList[job.EpochList.Count - 1];
            long total = (long)job.HiddenList.Count * job.Repeats * maxEpochs, done = 0;
            int lastPercent = -1;

            // One result object per (hidden, epochs) cell of the grid.
            Dictionary<string, TuningResult> cell = new Dictionary<string, TuningResult>();
            foreach (int h in job.HiddenList)
                foreach (int ep in job.EpochList)
                {
                    TuningResult r = new TuningResult();
                    r.Hidden = h;
                    r.Epochs = ep;
                    tuningResults.Add(r);
                    cell[h + "x" + ep] = r;
                }

            foreach (int h in job.HiddenList)
            {
                for (int run = 0; run < job.Repeats; run++)
                {
                    // New random weights every run (the engine's own Random) + a different shuffle order.
                    NetworkTrainer t = new NetworkTrainer(h, job.Seed * 1000 + run);

                    for (int ep = 1; ep <= maxEpochs; ep++)
                    {
                        if (worker.CancellationPending)
                        {
                            e.Cancel = true;
                            return;
                        }
                        t.TrainOneEpoch(trainSet);
                        done++;

                        TuningResult r;
                        if (cell.TryGetValue(h + "x" + ep, out r))
                        {
                            r.TestAccuracy.Add(t.ComputeAccuracy(testSet));
                            r.TrainAccuracy.Add(t.ComputeAccuracy(trainSet));
                            r.TestMse.Add(t.ComputeMse(testSet));
                            r.MacroF1.Add(ConfusionMatrix.Build(t, testSet).MacroF1());
                        }

                        int percent = (int)(100L * done / total);
                        if (percent != lastPercent)
                        {
                            lastPercent = percent;
                            worker.ReportProgress(percent, string.Format(
                                "Tuning... {0} hidden, run {1}/{2}, epoch {3}", h, run + 1, job.Repeats, ep));
                        }
                    }
                }
            }
        }

        private void bgwTuning_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBar.Value = Math.Min(Math.Max(e.ProgressPercentage, 0), 100);
            string msg = e.UserState as string;
            if (msg != null) SetStatus(msg);
        }

        private void bgwTuning_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            SetBusy(false);
            if (e.Error != null)
            {
                Log("Tuning failed: " + e.Error.Message);
                Warn("Tuning failed: " + e.Error.Message);
                SetStatus("Tuning failed.");
                return;
            }

            // After a Stop, keep only the settings that finished at least one run.
            List<TuningResult> rows = tuningResults.FindAll(r => r.Runs > 0);
            if (rows.Count == 0)
            {
                lblTuningNote.Text = "Tuning was stopped before any run finished.";
                Log("Tuning stopped before any run finished.");
                Log("");
                SetStatus("Tuning stopped.");
                return;
            }

            TuningResult best = ShowTuningResults(rows);
            LogTuningResults(rows, best, e.Cancelled);

            lblTuningNote.Text = string.Format(
                "Best: {0} hidden nodes, {1} epochs -> test accuracy {2:P1} \u00B1 {3:P1} over {4} runs (green row).{5}",
                best.Hidden, best.Epochs, Tuning.Mean(best.TestAccuracy), Tuning.Std(best.TestAccuracy), best.Runs,
                e.Cancelled ? "  Stopped early: some settings have fewer runs." : "");
            SetStatus(e.Cancelled ? "Tuning stopped; partial results shown." : "Tuning finished.");
        }

        // Fills dgvTuning and highlights the best row. Returns the best setting
        private TuningResult ShowTuningResults(List<TuningResult> rows)
        {
            dgvTuning.Columns.Clear();
            dgvTuning.Rows.Clear();
            AddColumn(dgvTuning, "colHidden", "Hidden nodes");
            AddColumn(dgvTuning, "colEpochs", "Epochs");
            AddColumn(dgvTuning, "colRuns", "Runs");
            AddColumn(dgvTuning, "colTestAcc", "Test acc (mean)");
            AddColumn(dgvTuning, "colTestStd", "\u00B1 std");
            AddColumn(dgvTuning, "colTrainAcc", "Train acc (mean)");
            AddColumn(dgvTuning, "colTestMse", "Test MSE (mean)");
            AddColumn(dgvTuning, "colF1", "Macro F1 (mean)");

            // Best = highest mean test accuracy; on a tie, the smaller (cheaper) network/epoch count.
            TuningResult best = rows[0];
            foreach (TuningResult r in rows)
            {
                double m = Tuning.Mean(r.TestAccuracy), b = Tuning.Mean(best.TestAccuracy);
                if (m > b + 1e-12) best = r;
            }

            foreach (TuningResult r in rows)
            {
                int i = dgvTuning.Rows.Add(r.Hidden, r.Epochs, r.Runs,
                    Tuning.Mean(r.TestAccuracy).ToString("P1"), Tuning.Std(r.TestAccuracy).ToString("P1"),
                    Tuning.Mean(r.TrainAccuracy).ToString("P1"), Tuning.Mean(r.TestMse).ToString("0.0000"),
                    Tuning.Mean(r.MacroF1).ToString("0.000"));
                if (r == best)
                {
                    dgvTuning.Rows[i].DefaultCellStyle.BackColor = CorrectCell;
                    dgvTuning.Rows[i].DefaultCellStyle.Font = new Font(dgvTuning.Font, FontStyle.Bold);
                }
            }
            dgvTuning.ClearSelection();
            return best;
        }

        private void LogTuningResults(List<TuningResult> rows, TuningResult best, bool stopped)
        {
            Log(stopped ? "Tuning stopped early. Partial results:" : "Tuning results (mean over runs, test set):");
            Log("  Hidden | Epochs | Runs | Test acc \u00B1 std   | Train acc | Test MSE | Macro F1");
            foreach (TuningResult r in rows)
            {
                Log(string.Format("  {0,6} | {1,6} | {2,4} | {3,6:P1} \u00B1 {4,5:P1} | {5,8:P1} | {6:0.0000}   | {7:0.000}{8}",
                    r.Hidden, r.Epochs, r.Runs, Tuning.Mean(r.TestAccuracy), Tuning.Std(r.TestAccuracy),
                    Tuning.Mean(r.TrainAccuracy), Tuning.Mean(r.TestMse), Tuning.Mean(r.MacroF1),
                    r == best ? "  <- best" : ""));
            }
            Log(string.Format("  Best: {0} hidden nodes, {1} epochs.", best.Hidden, best.Epochs));
            Log("  To use it: set Hidden nodes and Epochs on the left, then Create NN and Train NN.");
            Log("");
        }

        // ---- helpers ----

        // Measures the current network on both sets. Safe on either thread
        private TrainingRecord MakeRecord()
        {
            TrainingRecord r = new TrainingRecord();
            r.Epoch = trainer.EpochsTrained;
            r.TrainMse = trainer.ComputeMse(trainSet);
            r.TestMse = trainer.ComputeMse(testSet);
            r.TrainAccuracy = trainer.ComputeAccuracy(trainSet);
            r.TestAccuracy = trainer.ComputeAccuracy(testSet);
            return r;
        }

        private static string FormatRecord(TrainingRecord r)
        {
            return string.Format("  {0,6} |  {1:F5}  | {2:F5}  |  {3,6:P1}  | {4,6:P1}",
                r.Epoch, r.TrainMse, r.TestMse, r.TrainAccuracy, r.TestAccuracy);
        }
    }

    // One line of the training log: the network's scores after a given epoch
    public class TrainingRecord
    {
        public int Epoch;
        public double TrainMse;
        public double TestMse;
        public double TrainAccuracy;
        public double TestAccuracy;
    }
}