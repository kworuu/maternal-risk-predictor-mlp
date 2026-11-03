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
    /// <summary>
    /// Model part of the main window (owner: logic partner).
    /// L1: Create NN.   L2: Train / Stop.   L3: loss chart + export.   L4: evaluation.
    /// L5: classify one patient.   L6: tuning.
    /// Reads trainSet, testSet and scaler from Form1.Data.cs but never changes them.
    /// </summary>
    public partial class Form1
    {
        // ---- network ----
        private NetworkTrainer trainer;   // null until Create NN is clicked

        // ---- training history (one record per logged epoch; L3 draws and exports it) ----
        private List<TrainingRecord> trainingHistory = new List<TrainingRecord>();

        /// <summary>Connects the model buttons. Called once from the constructor.</summary>
        private void WireModelEvents()
        {
            btnCreate.Click += new EventHandler(btnCreate_Click);

            // L2: training runs on a background thread so the window stays responsive
            btnTrain.Click += new EventHandler(btnTrain_Click);
            btnStop.Click += new EventHandler(btnStop_Click);
            bgwTrain.DoWork += new DoWorkEventHandler(bgwTrain_DoWork);
            bgwTrain.ProgressChanged += new ProgressChangedEventHandler(bgwTrain_ProgressChanged);
            bgwTrain.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bgwTrain_RunWorkerCompleted);
            this.FormClosing += new FormClosingEventHandler(Form1_FormClosingModel);
            btnStop.Enabled = false;

            // L3: loss chart + CSV export
            SetupLossChart();
            btnExport.Click += new EventHandler(btnExport_Click);

            // L4: evaluation on the test set
            btnEvaluate.Click += new EventHandler(btnEvaluate_Click);
            ClearEvaluation();

            // L5: btnPredict
            // L6: btnRunTuning, bgwTuning events
        }

        /// <summary>
        /// Runs whenever the data is reloaded or re-split (called from Form1.Data.cs).
        /// A network trained on the old split is no longer valid, so it is discarded.
        /// </summary>
        partial void OnDataChanged()
        {
            if (trainer != null)
            {
                trainer = null;
                trainingHistory.Clear();
                RefreshLossChart();
                ClearEvaluation();
                Log("Data changed: the old network was discarded. Click Create NN again.");
            }
        }

        // =====================================================================
        // L1: Create NN
        // =====================================================================

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

            Log(string.Format("Created a {0}-{1}-{2} network with random weights.",
                MaternalData.NumFeatures, hidden, MaternalData.NumClasses));
            Log(string.Format("  Before training: train MSE {0:F5}, test accuracy {1:P1} (random guessing)",
                start.TrainMse, start.TestAccuracy));
            Log("");
            tabMain.SelectedTab = tabLog;
            SetStatus("Network created. Next: Train NN.");
        }

        // =====================================================================
        // L2: Train / Stop
        // =====================================================================

        /// <summary>The settings one training run needs, read on the UI thread before it starts.</summary>
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
        }

        /// <summary>BACKGROUND THREAD. Runs the epochs. No UI calls here, only ReportProgress.</summary>
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

        /// <summary>UI THREAD. Moves the bar and, when a record arrives, logs and stores it.</summary>
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

        /// <summary>UI THREAD. Runs once when training finishes, is stopped, or crashes.</summary>
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

        /// <summary>Closing the window mid-training: ask the worker to stop first.</summary>
        private void Form1_FormClosingModel(object sender, FormClosingEventArgs e)
        {
            if (bgwTrain.IsBusy) bgwTrain.CancelAsync();
        }

        // =====================================================================
        // L3: Loss chart + export training_log.csv
        // =====================================================================

        private const string TrainSeries = "Train MSE";
        private const string TestSeries = "Test MSE";

        // The loss chart is created in code, not in the Designer, so Form1.Designer.cs
        // (owned by the UI partner) never has to change. It replaces the old picLoss box.
        private Chart chartLoss;

        /// <summary>
        /// Creates the chart, puts it on the Loss curve tab in place of picLoss, and sets it up:
        /// one area, x = epoch, y = MSE, two lines (train in blue, test in orange).
        /// </summary>
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

        /// <summary>Redraws every point from trainingHistory (after Create NN or a data change).</summary>
        private void RefreshLossChart()
        {
            chartLoss.Series[TrainSeries].Points.Clear();
            chartLoss.Series[TestSeries].Points.Clear();
            foreach (TrainingRecord r in trainingHistory) AddChartPoint(r);
        }

        /// <summary>Adds one record to both lines (called live while training).</summary>
        private void AddChartPoint(TrainingRecord r)
        {
            chartLoss.Series[TrainSeries].Points.AddXY(r.Epoch, r.TrainMse);
            chartLoss.Series[TestSeries].Points.AddXY(r.Epoch, r.TestMse);
        }

        /// <summary>
        /// Saves trainingHistory as CSV, one row per logged epoch. Numbers use a dot as the
        /// decimal point (InvariantCulture) so Excel, Python and the professor all read it the same.
        /// </summary>
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

        // =====================================================================
        // L4: Evaluation (confusion matrix, per-class metrics, baseline)
        // =====================================================================

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

        /// <summary>Rows = actual class, columns = predicted class, plus totals.</summary>
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

        /// <summary>Precision, recall, F1 and support per class, then the macro average.</summary>
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

        /// <summary>The same results as text, so they are also in the log (and in screenshots of it).</summary>
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

        /// <summary>Empties the Evaluation tab (new network or new data = old results no longer valid).</summary>
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

        // ---- helpers ----

        /// <summary>Measures the current network on both sets. Safe on either thread.</summary>
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

    /// <summary>One line of the training log: the network's scores after a given epoch.</summary>
    public class TrainingRecord
    {
        public int Epoch;
        public double TrainMse;
        public double TestMse;
        public double TrainAccuracy;
        public double TestAccuracy;
    }
}