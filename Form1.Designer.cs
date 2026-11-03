namespace MaternalRiskPredictor
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.lblStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.progressBar = new System.Windows.Forms.ToolStripProgressBar();
            this.tabMain = new System.Windows.Forms.TabControl();
            this.tabLog = new System.Windows.Forms.TabPage();
            this.txtLog = new System.Windows.Forms.TextBox();
            this.tabLoss = new System.Windows.Forms.TabPage();
            this.picLoss = new System.Windows.Forms.PictureBox();
            this.tabEval = new System.Windows.Forms.TabPage();
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.lblMetricsTitle = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.lblConfusionTitle = new System.Windows.Forms.Label();
            this.lblAccuracy = new System.Windows.Forms.Label();
            this.tabData = new System.Windows.Forms.TabPage();
            this.lblDataLegend = new System.Windows.Forms.Label();
            this.rdoScaled = new System.Windows.Forms.RadioButton();
            this.rdoCleaned = new System.Windows.Forms.RadioButton();
            this.rdoRaw = new System.Windows.Forms.RadioButton();
            this.dgvData = new System.Windows.Forms.DataGridView();
            this.tabTuning = new System.Windows.Forms.TabPage();
            this.grpData = new System.Windows.Forms.GroupBox();
            this.btnPrepare = new System.Windows.Forms.Button();
            this.chkRemoveImpossible = new System.Windows.Forms.CheckBox();
            this.chkRemoveDuplicates = new System.Windows.Forms.CheckBox();
            this.chkStratified = new System.Windows.Forms.CheckBox();
            this.numSeed = new System.Windows.Forms.NumericUpDown();
            this.numTrainPct = new System.Windows.Forms.NumericUpDown();
            this.lblSeed = new System.Windows.Forms.Label();
            this.lblTrainPct = new System.Windows.Forms.Label();
            this.lblDataInfo = new System.Windows.Forms.Label();
            this.lblFile = new System.Windows.Forms.Label();
            this.btnLoad = new System.Windows.Forms.Button();
            this.grpNetwork = new System.Windows.Forms.GroupBox();
            this.btnStop = new System.Windows.Forms.Button();
            this.btnTrain = new System.Windows.Forms.Button();
            this.btnCreate = new System.Windows.Forms.Button();
            this.lblLearnRate = new System.Windows.Forms.Label();
            this.numLogEvery = new System.Windows.Forms.NumericUpDown();
            this.lblLogEvery = new System.Windows.Forms.Label();
            this.numHidden = new System.Windows.Forms.NumericUpDown();
            this.lblEpochs = new System.Windows.Forms.Label();
            this.numEpochs = new System.Windows.Forms.NumericUpDown();
            this.lblHidden = new System.Windows.Forms.Label();
            this.lblArch = new System.Windows.Forms.Label();
            this.grpEvaluate = new System.Windows.Forms.GroupBox();
            this.btnExport = new System.Windows.Forms.Button();
            this.btnEvaluate = new System.Windows.Forms.Button();
            this.grpPredict = new System.Windows.Forms.GroupBox();
            this.lblPrediction = new System.Windows.Forms.Label();
            this.btnPredict = new System.Windows.Forms.Button();
            this.numHeartRate = new System.Windows.Forms.NumericUpDown();
            this.numBodyTemp = new System.Windows.Forms.NumericUpDown();
            this.lblHeartRate = new System.Windows.Forms.Label();
            this.lblBodyTemp = new System.Windows.Forms.Label();
            this.numBloodSugar = new System.Windows.Forms.NumericUpDown();
            this.lblBloodSugar = new System.Windows.Forms.Label();
            this.numDiastolic = new System.Windows.Forms.NumericUpDown();
            this.lblDiastolic = new System.Windows.Forms.Label();
            this.numSystolic = new System.Windows.Forms.NumericUpDown();
            this.lblSystolic = new System.Windows.Forms.Label();
            this.numAge = new System.Windows.Forms.NumericUpDown();
            this.lblAge = new System.Windows.Forms.Label();
            this.txtHiddenList = new System.Windows.Forms.TextBox();
            this.lblEpochList = new System.Windows.Forms.Label();
            this.lblRepeats = new System.Windows.Forms.Label();
            this.lblTuningNote = new System.Windows.Forms.Label();
            this.lblHiddenList = new System.Windows.Forms.Label();
            this.txtEpochList = new System.Windows.Forms.TextBox();
            this.btnRunTuning = new System.Windows.Forms.Button();
            this.numRepeats = new System.Windows.Forms.NumericUpDown();
            this.dgvTuning = new System.Windows.Forms.DataGridView();
            this.statusStrip.SuspendLayout();
            this.tabMain.SuspendLayout();
            this.tabLog.SuspendLayout();
            this.tabLoss.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picLoss)).BeginInit();
            this.tabEval.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.tabData.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvData)).BeginInit();
            this.tabTuning.SuspendLayout();
            this.grpData.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numSeed)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numTrainPct)).BeginInit();
            this.grpNetwork.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numLogEvery)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numHidden)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numEpochs)).BeginInit();
            this.grpEvaluate.SuspendLayout();
            this.grpPredict.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numHeartRate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numBodyTemp)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numBloodSugar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numDiastolic)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numSystolic)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numAge)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numRepeats)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTuning)).BeginInit();
            this.SuspendLayout();
            // 
            // statusStrip
            // 
            this.statusStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblStatus,
            this.progressBar});
            this.statusStrip.Location = new System.Drawing.Point(0, 727);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(1262, 26);
            this.statusStrip.TabIndex = 0;
            this.statusStrip.Text = "statusStrip1";
            // 
            // lblStatus
            // 
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(1045, 20);
            this.lblStatus.Spring = true;
            this.lblStatus.Text = "Ready";
            this.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // progressBar
            // 
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(200, 18);
            // 
            // tabMain
            // 
            this.tabMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabMain.Controls.Add(this.tabLog);
            this.tabMain.Controls.Add(this.tabLoss);
            this.tabMain.Controls.Add(this.tabEval);
            this.tabMain.Controls.Add(this.tabData);
            this.tabMain.Controls.Add(this.tabTuning);
            this.tabMain.Location = new System.Drawing.Point(424, 18);
            this.tabMain.Name = "tabMain";
            this.tabMain.SelectedIndex = 0;
            this.tabMain.Size = new System.Drawing.Size(828, 690);
            this.tabMain.TabIndex = 1;
            // 
            // tabLog
            // 
            this.tabLog.Controls.Add(this.txtLog);
            this.tabLog.Location = new System.Drawing.Point(4, 25);
            this.tabLog.Name = "tabLog";
            this.tabLog.Padding = new System.Windows.Forms.Padding(3);
            this.tabLog.Size = new System.Drawing.Size(820, 661);
            this.tabLog.TabIndex = 0;
            this.tabLog.Text = "Training Log";
            this.tabLog.UseVisualStyleBackColor = true;
            // 
            // txtLog
            // 
            this.txtLog.BackColor = System.Drawing.Color.White;
            this.txtLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtLog.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLog.Location = new System.Drawing.Point(3, 3);
            this.txtLog.Multiline = true;
            this.txtLog.Name = "txtLog";
            this.txtLog.ReadOnly = true;
            this.txtLog.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtLog.Size = new System.Drawing.Size(814, 655);
            this.txtLog.TabIndex = 0;
            this.txtLog.WordWrap = false;
            // 
            // tabLoss
            // 
            this.tabLoss.Controls.Add(this.picLoss);
            this.tabLoss.Location = new System.Drawing.Point(4, 25);
            this.tabLoss.Name = "tabLoss";
            this.tabLoss.Padding = new System.Windows.Forms.Padding(3);
            this.tabLoss.Size = new System.Drawing.Size(820, 661);
            this.tabLoss.TabIndex = 1;
            this.tabLoss.Text = "Loss curve";
            this.tabLoss.UseVisualStyleBackColor = true;
            // 
            // picLoss
            // 
            this.picLoss.BackColor = System.Drawing.Color.White;
            this.picLoss.Dock = System.Windows.Forms.DockStyle.Fill;
            this.picLoss.Location = new System.Drawing.Point(3, 3);
            this.picLoss.Name = "picLoss";
            this.picLoss.Size = new System.Drawing.Size(814, 655);
            this.picLoss.TabIndex = 0;
            this.picLoss.TabStop = false;
            // 
            // tabEval
            // 
            this.tabEval.Controls.Add(this.dataGridView2);
            this.tabEval.Controls.Add(this.lblMetricsTitle);
            this.tabEval.Controls.Add(this.dataGridView1);
            this.tabEval.Controls.Add(this.lblConfusionTitle);
            this.tabEval.Controls.Add(this.lblAccuracy);
            this.tabEval.Location = new System.Drawing.Point(4, 25);
            this.tabEval.Name = "tabEval";
            this.tabEval.Padding = new System.Windows.Forms.Padding(3);
            this.tabEval.Size = new System.Drawing.Size(820, 661);
            this.tabEval.TabIndex = 2;
            this.tabEval.Text = "Evaluation";
            this.tabEval.UseVisualStyleBackColor = true;
            // 
            // dataGridView2
            // 
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView2.Location = new System.Drawing.Point(12, 235);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.RowHeadersWidth = 51;
            this.dataGridView2.RowTemplate.Height = 24;
            this.dataGridView2.Size = new System.Drawing.Size(700, 150);
            this.dataGridView2.TabIndex = 4;
            // 
            // lblMetricsTitle
            // 
            this.lblMetricsTitle.AutoSize = true;
            this.lblMetricsTitle.Location = new System.Drawing.Point(13, 215);
            this.lblMetricsTitle.Name = "lblMetricsTitle";
            this.lblMetricsTitle.Size = new System.Drawing.Size(173, 16);
            this.lblMetricsTitle.TabIndex = 3;
            this.lblMetricsTitle.Text = "Per-class metrics on test set";
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(12, 84);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.Size = new System.Drawing.Size(700, 110);
            this.dataGridView1.TabIndex = 2;
            // 
            // lblConfusionTitle
            // 
            this.lblConfusionTitle.AutoSize = true;
            this.lblConfusionTitle.Location = new System.Drawing.Point(12, 66);
            this.lblConfusionTitle.Name = "lblConfusionTitle";
            this.lblConfusionTitle.Size = new System.Drawing.Size(381, 16);
            this.lblConfusionTitle.TabIndex = 1;
            this.lblConfusionTitle.Text = "Confusion matrix on test set (rows = actual, columns = predicted)";
            // 
            // lblAccuracy
            // 
            this.lblAccuracy.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAccuracy.Location = new System.Drawing.Point(12, 12);
            this.lblAccuracy.Name = "lblAccuracy";
            this.lblAccuracy.Size = new System.Drawing.Size(700, 44);
            this.lblAccuracy.TabIndex = 0;
            this.lblAccuracy.Text = "Train the network, then click Evaluate on test set.";
            // 
            // tabData
            // 
            this.tabData.Controls.Add(this.lblDataLegend);
            this.tabData.Controls.Add(this.rdoScaled);
            this.tabData.Controls.Add(this.rdoCleaned);
            this.tabData.Controls.Add(this.rdoRaw);
            this.tabData.Controls.Add(this.dgvData);
            this.tabData.Location = new System.Drawing.Point(4, 25);
            this.tabData.Name = "tabData";
            this.tabData.Padding = new System.Windows.Forms.Padding(3);
            this.tabData.Size = new System.Drawing.Size(820, 661);
            this.tabData.TabIndex = 3;
            this.tabData.Text = "Data";
            this.tabData.UseVisualStyleBackColor = true;
            // 
            // lblDataLegend
            // 
            this.lblDataLegend.AutoSize = true;
            this.lblDataLegend.ForeColor = System.Drawing.Color.DimGray;
            this.lblDataLegend.Location = new System.Drawing.Point(288, 8);
            this.lblDataLegend.Name = "lblDataLegend";
            this.lblDataLegend.Size = new System.Drawing.Size(281, 16);
            this.lblDataLegend.TabIndex = 4;
            this.lblDataLegend.Text = "Grey = duplicate row    Red = impossible value";
            // 
            // rdoScaled
            // 
            this.rdoScaled.AutoSize = true;
            this.rdoScaled.Enabled = false;
            this.rdoScaled.Location = new System.Drawing.Point(160, 8);
            this.rdoScaled.Name = "rdoScaled";
            this.rdoScaled.Size = new System.Drawing.Size(102, 20);
            this.rdoScaled.TabIndex = 3;
            this.rdoScaled.Text = "Scaled [0, 1]";
            this.rdoScaled.UseVisualStyleBackColor = true;
            // 
            // rdoCleaned
            // 
            this.rdoCleaned.AutoSize = true;
            this.rdoCleaned.Enabled = false;
            this.rdoCleaned.Location = new System.Drawing.Point(70, 8);
            this.rdoCleaned.Name = "rdoCleaned";
            this.rdoCleaned.Size = new System.Drawing.Size(79, 20);
            this.rdoCleaned.TabIndex = 2;
            this.rdoCleaned.Text = "Cleaned";
            this.rdoCleaned.UseVisualStyleBackColor = true;
            // 
            // rdoRaw
            // 
            this.rdoRaw.AutoSize = true;
            this.rdoRaw.Checked = true;
            this.rdoRaw.Location = new System.Drawing.Point(8, 8);
            this.rdoRaw.Name = "rdoRaw";
            this.rdoRaw.Size = new System.Drawing.Size(55, 20);
            this.rdoRaw.TabIndex = 1;
            this.rdoRaw.TabStop = true;
            this.rdoRaw.Text = "Raw";
            this.rdoRaw.UseVisualStyleBackColor = true;
            // 
            // dgvData
            // 
            this.dgvData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvData.Location = new System.Drawing.Point(3, 32);
            this.dgvData.Name = "dgvData";
            this.dgvData.RowHeadersWidth = 51;
            this.dgvData.RowTemplate.Height = 24;
            this.dgvData.Size = new System.Drawing.Size(720, 589);
            this.dgvData.TabIndex = 0;
            // 
            // tabTuning
            // 
            this.tabTuning.Controls.Add(this.dgvTuning);
            this.tabTuning.Controls.Add(this.numRepeats);
            this.tabTuning.Controls.Add(this.btnRunTuning);
            this.tabTuning.Controls.Add(this.txtEpochList);
            this.tabTuning.Controls.Add(this.lblHiddenList);
            this.tabTuning.Controls.Add(this.lblTuningNote);
            this.tabTuning.Controls.Add(this.lblRepeats);
            this.tabTuning.Controls.Add(this.lblEpochList);
            this.tabTuning.Controls.Add(this.txtHiddenList);
            this.tabTuning.Location = new System.Drawing.Point(4, 25);
            this.tabTuning.Name = "tabTuning";
            this.tabTuning.Padding = new System.Windows.Forms.Padding(3);
            this.tabTuning.Size = new System.Drawing.Size(820, 661);
            this.tabTuning.TabIndex = 4;
            this.tabTuning.Text = "Tuning";
            this.tabTuning.UseVisualStyleBackColor = true;
            // 
            // grpData
            // 
            this.grpData.Controls.Add(this.btnPrepare);
            this.grpData.Controls.Add(this.chkRemoveImpossible);
            this.grpData.Controls.Add(this.chkRemoveDuplicates);
            this.grpData.Controls.Add(this.chkStratified);
            this.grpData.Controls.Add(this.numSeed);
            this.grpData.Controls.Add(this.numTrainPct);
            this.grpData.Controls.Add(this.lblSeed);
            this.grpData.Controls.Add(this.lblTrainPct);
            this.grpData.Controls.Add(this.lblDataInfo);
            this.grpData.Controls.Add(this.lblFile);
            this.grpData.Controls.Add(this.btnLoad);
            this.grpData.Location = new System.Drawing.Point(12, 12);
            this.grpData.Name = "grpData";
            this.grpData.Size = new System.Drawing.Size(400, 218);
            this.grpData.TabIndex = 2;
            this.grpData.TabStop = false;
            this.grpData.Text = "1. Data";
            // 
            // btnPrepare
            // 
            this.btnPrepare.Location = new System.Drawing.Point(12, 178);
            this.btnPrepare.Name = "btnPrepare";
            this.btnPrepare.Size = new System.Drawing.Size(306, 28);
            this.btnPrepare.TabIndex = 10;
            this.btnPrepare.Text = "Clean + Split + Normalize (Min-Max to [0, 1])";
            this.btnPrepare.UseVisualStyleBackColor = true;
            // 
            // chkRemoveImpossible
            // 
            this.chkRemoveImpossible.AutoSize = true;
            this.chkRemoveImpossible.Checked = true;
            this.chkRemoveImpossible.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkRemoveImpossible.Location = new System.Drawing.Point(15, 154);
            this.chkRemoveImpossible.Name = "chkRemoveImpossible";
            this.chkRemoveImpossible.Size = new System.Drawing.Size(287, 20);
            this.chkRemoveImpossible.TabIndex = 9;
            this.chkRemoveImpossible.Text = "Remove impossible values (heart rate < 40)";
            this.chkRemoveImpossible.UseVisualStyleBackColor = true;
            // 
            // chkRemoveDuplicates
            // 
            this.chkRemoveDuplicates.AutoSize = true;
            this.chkRemoveDuplicates.Checked = true;
            this.chkRemoveDuplicates.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkRemoveDuplicates.Location = new System.Drawing.Point(15, 132);
            this.chkRemoveDuplicates.Name = "chkRemoveDuplicates";
            this.chkRemoveDuplicates.Size = new System.Drawing.Size(268, 20);
            this.chkRemoveDuplicates.TabIndex = 8;
            this.chkRemoveDuplicates.Text = "Remove duplicate rows (before splitting)";
            this.chkRemoveDuplicates.UseVisualStyleBackColor = true;
            // 
            // chkStratified
            // 
            this.chkStratified.AutoSize = true;
            this.chkStratified.Checked = true;
            this.chkStratified.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkStratified.Location = new System.Drawing.Point(15, 110);
            this.chkStratified.Name = "chkStratified";
            this.chkStratified.Size = new System.Drawing.Size(272, 20);
            this.chkStratified.TabIndex = 7;
            this.chkStratified.Text = "Stratified split (same share of each class)";
            this.chkStratified.UseVisualStyleBackColor = true;
            // 
            // numSeed
            // 
            this.numSeed.Location = new System.Drawing.Point(262, 81);
            this.numSeed.Maximum = new decimal(new int[] {
            9999,
            0,
            0,
            0});
            this.numSeed.Name = "numSeed";
            this.numSeed.Size = new System.Drawing.Size(80, 22);
            this.numSeed.TabIndex = 6;
            this.numSeed.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // numTrainPct
            // 
            this.numTrainPct.Location = new System.Drawing.Point(90, 81);
            this.numTrainPct.Maximum = new decimal(new int[] {
            95,
            0,
            0,
            0});
            this.numTrainPct.Minimum = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.numTrainPct.Name = "numTrainPct";
            this.numTrainPct.Size = new System.Drawing.Size(60, 22);
            this.numTrainPct.TabIndex = 5;
            this.numTrainPct.Value = new decimal(new int[] {
            80,
            0,
            0,
            0});
            // 
            // lblSeed
            // 
            this.lblSeed.AutoSize = true;
            this.lblSeed.Location = new System.Drawing.Point(180, 84);
            this.lblSeed.Name = "lblSeed";
            this.lblSeed.Size = new System.Drawing.Size(70, 16);
            this.lblSeed.TabIndex = 4;
            this.lblSeed.Text = "Split seed:";
            // 
            // lblTrainPct
            // 
            this.lblTrainPct.AutoSize = true;
            this.lblTrainPct.Location = new System.Drawing.Point(12, 84);
            this.lblTrainPct.Name = "lblTrainPct";
            this.lblTrainPct.Size = new System.Drawing.Size(74, 16);
            this.lblTrainPct.TabIndex = 3;
            this.lblTrainPct.Text = "Training %:\t";
            // 
            // lblDataInfo
            // 
            this.lblDataInfo.AutoSize = true;
            this.lblDataInfo.ForeColor = System.Drawing.Color.DimGray;
            this.lblDataInfo.Location = new System.Drawing.Point(12, 55);
            this.lblDataInfo.Name = "lblDataInfo";
            this.lblDataInfo.Size = new System.Drawing.Size(143, 16);
            this.lblDataInfo.TabIndex = 2;
            this.lblDataInfo.Text = "Load the CSV to begin.";
            // 
            // lblFile
            // 
            this.lblFile.AutoSize = true;
            this.lblFile.Location = new System.Drawing.Point(120, 27);
            this.lblFile.Name = "lblFile";
            this.lblFile.Size = new System.Drawing.Size(91, 16);
            this.lblFile.TabIndex = 1;
            this.lblFile.Text = "No file loaded";
            // 
            // btnLoad
            // 
            this.btnLoad.Location = new System.Drawing.Point(12, 20);
            this.btnLoad.Name = "btnLoad";
            this.btnLoad.Size = new System.Drawing.Size(100, 28);
            this.btnLoad.TabIndex = 0;
            this.btnLoad.Text = "Load CSV...\t";
            this.btnLoad.UseVisualStyleBackColor = true;
            // 
            // grpNetwork
            // 
            this.grpNetwork.Controls.Add(this.btnStop);
            this.grpNetwork.Controls.Add(this.btnTrain);
            this.grpNetwork.Controls.Add(this.btnCreate);
            this.grpNetwork.Controls.Add(this.lblLearnRate);
            this.grpNetwork.Controls.Add(this.numLogEvery);
            this.grpNetwork.Controls.Add(this.lblLogEvery);
            this.grpNetwork.Controls.Add(this.numHidden);
            this.grpNetwork.Controls.Add(this.lblEpochs);
            this.grpNetwork.Controls.Add(this.numEpochs);
            this.grpNetwork.Controls.Add(this.lblHidden);
            this.grpNetwork.Controls.Add(this.lblArch);
            this.grpNetwork.Location = new System.Drawing.Point(12, 236);
            this.grpNetwork.Name = "grpNetwork";
            this.grpNetwork.Size = new System.Drawing.Size(400, 206);
            this.grpNetwork.TabIndex = 3;
            this.grpNetwork.TabStop = false;
            this.grpNetwork.Text = "2. Network and training";
            // 
            // btnStop
            // 
            this.btnStop.Enabled = false;
            this.btnStop.Location = new System.Drawing.Point(222, 160);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(96, 32);
            this.btnStop.TabIndex = 10;
            this.btnStop.Text = "Stop";
            this.btnStop.UseVisualStyleBackColor = true;
            // 
            // btnTrain
            // 
            this.btnTrain.Location = new System.Drawing.Point(117, 160);
            this.btnTrain.Name = "btnTrain";
            this.btnTrain.Size = new System.Drawing.Size(96, 32);
            this.btnTrain.TabIndex = 9;
            this.btnTrain.Text = "Train NN";
            this.btnTrain.UseVisualStyleBackColor = true;
            // 
            // btnCreate
            // 
            this.btnCreate.Location = new System.Drawing.Point(12, 160);
            this.btnCreate.Name = "btnCreate";
            this.btnCreate.Size = new System.Drawing.Size(96, 32);
            this.btnCreate.TabIndex = 8;
            this.btnCreate.Text = "Create NN";
            this.btnCreate.UseVisualStyleBackColor = true;
            // 
            // lblLearnRate
            // 
            this.lblLearnRate.AutoSize = true;
            this.lblLearnRate.ForeColor = System.Drawing.Color.DimGray;
            this.lblLearnRate.Location = new System.Drawing.Point(12, 132);
            this.lblLearnRate.Name = "lblLearnRate";
            this.lblLearnRate.Size = new System.Drawing.Size(272, 16);
            this.lblLearnRate.TabIndex = 7;
            this.lblLearnRate.Text = "Learning rate: fixed in engine (0.2 out / 0.15 in)";
            // 
            // numLogEvery
            // 
            this.numLogEvery.Location = new System.Drawing.Point(222, 104);
            this.numLogEvery.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.numLogEvery.Name = "numLogEvery";
            this.numLogEvery.Size = new System.Drawing.Size(120, 22);
            this.numLogEvery.TabIndex = 6;
            this.numLogEvery.Value = new decimal(new int[] {
            200,
            0,
            0,
            0});
            // 
            // lblLogEvery
            // 
            this.lblLogEvery.AutoSize = true;
            this.lblLogEvery.Location = new System.Drawing.Point(12, 106);
            this.lblLogEvery.Name = "lblLogEvery";
            this.lblLogEvery.Size = new System.Drawing.Size(131, 16);
            this.lblLogEvery.TabIndex = 5;
            this.lblLogEvery.Text = "Log every N epochs:";
            // 
            // numHidden
            // 
            this.numHidden.Location = new System.Drawing.Point(224, 44);
            this.numHidden.Maximum = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.numHidden.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numHidden.Name = "numHidden";
            this.numHidden.Size = new System.Drawing.Size(118, 22);
            this.numHidden.TabIndex = 4;
            this.numHidden.Value = new decimal(new int[] {
            6,
            0,
            0,
            0});
            // 
            // lblEpochs
            // 
            this.lblEpochs.AutoSize = true;
            this.lblEpochs.Location = new System.Drawing.Point(12, 78);
            this.lblEpochs.Name = "lblEpochs";
            this.lblEpochs.Size = new System.Drawing.Size(143, 16);
            this.lblEpochs.TabIndex = 3;
            this.lblEpochs.Text = "Epochs per Train click:";
            // 
            // numEpochs
            // 
            this.numEpochs.Increment = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.numEpochs.Location = new System.Drawing.Point(222, 76);
            this.numEpochs.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.numEpochs.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numEpochs.Name = "numEpochs";
            this.numEpochs.Size = new System.Drawing.Size(120, 22);
            this.numEpochs.TabIndex = 2;
            this.numEpochs.Value = new decimal(new int[] {
            2000,
            0,
            0,
            0});
            // 
            // lblHidden
            // 
            this.lblHidden.AutoSize = true;
            this.lblHidden.Location = new System.Drawing.Point(12, 50);
            this.lblHidden.Name = "lblHidden";
            this.lblHidden.Size = new System.Drawing.Size(95, 16);
            this.lblHidden.TabIndex = 1;
            this.lblHidden.Text = "Hidden nodes:";
            // 
            // lblArch
            // 
            this.lblArch.AutoSize = true;
            this.lblArch.Location = new System.Drawing.Point(12, 22);
            this.lblArch.Name = "lblArch";
            this.lblArch.Size = new System.Drawing.Size(238, 16);
            this.lblArch.TabIndex = 0;
            this.lblArch.Text = "\tInputs: 6 Outputs: 3 (low / mid / high risk)\t";
            // 
            // grpEvaluate
            // 
            this.grpEvaluate.Controls.Add(this.btnExport);
            this.grpEvaluate.Controls.Add(this.btnEvaluate);
            this.grpEvaluate.Location = new System.Drawing.Point(12, 448);
            this.grpEvaluate.Name = "grpEvaluate";
            this.grpEvaluate.Size = new System.Drawing.Size(400, 64);
            this.grpEvaluate.TabIndex = 4;
            this.grpEvaluate.TabStop = false;
            this.grpEvaluate.Text = "3. Evaluate";
            // 
            // btnExport
            // 
            this.btnExport.Location = new System.Drawing.Point(171, 22);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(171, 30);
            this.btnExport.TabIndex = 1;
            this.btnExport.Text = "Export loss log (CSV)...";
            this.btnExport.UseVisualStyleBackColor = true;
            // 
            // btnEvaluate
            // 
            this.btnEvaluate.Location = new System.Drawing.Point(12, 22);
            this.btnEvaluate.Name = "btnEvaluate";
            this.btnEvaluate.Size = new System.Drawing.Size(147, 30);
            this.btnEvaluate.TabIndex = 0;
            this.btnEvaluate.Text = "Evaluate on test set";
            this.btnEvaluate.UseVisualStyleBackColor = true;
            // 
            // grpPredict
            // 
            this.grpPredict.Controls.Add(this.lblPrediction);
            this.grpPredict.Controls.Add(this.btnPredict);
            this.grpPredict.Controls.Add(this.numHeartRate);
            this.grpPredict.Controls.Add(this.numBodyTemp);
            this.grpPredict.Controls.Add(this.lblHeartRate);
            this.grpPredict.Controls.Add(this.lblBodyTemp);
            this.grpPredict.Controls.Add(this.numBloodSugar);
            this.grpPredict.Controls.Add(this.lblBloodSugar);
            this.grpPredict.Controls.Add(this.numDiastolic);
            this.grpPredict.Controls.Add(this.lblDiastolic);
            this.grpPredict.Controls.Add(this.numSystolic);
            this.grpPredict.Controls.Add(this.lblSystolic);
            this.grpPredict.Controls.Add(this.numAge);
            this.grpPredict.Controls.Add(this.lblAge);
            this.grpPredict.Location = new System.Drawing.Point(12, 518);
            this.grpPredict.Name = "grpPredict";
            this.grpPredict.Size = new System.Drawing.Size(400, 190);
            this.grpPredict.TabIndex = 5;
            this.grpPredict.TabStop = false;
            this.grpPredict.Text = "4. Classify one patient (raw values)";
            // 
            // lblPrediction
            // 
            this.lblPrediction.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblPrediction.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPrediction.Location = new System.Drawing.Point(143, 127);
            this.lblPrediction.Name = "lblPrediction";
            this.lblPrediction.Size = new System.Drawing.Size(199, 38);
            this.lblPrediction.TabIndex = 13;
            this.lblPrediction.Text = "Prediction appears here";
            this.lblPrediction.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnPredict
            // 
            this.btnPredict.Location = new System.Drawing.Point(16, 130);
            this.btnPredict.Name = "btnPredict";
            this.btnPredict.Size = new System.Drawing.Size(96, 32);
            this.btnPredict.TabIndex = 12;
            this.btnPredict.Text = "Run NN";
            this.btnPredict.UseVisualStyleBackColor = true;
            // 
            // numHeartRate
            // 
            this.numHeartRate.Location = new System.Drawing.Point(289, 81);
            this.numHeartRate.Maximum = new decimal(new int[] {
            150,
            0,
            0,
            0});
            this.numHeartRate.Minimum = new decimal(new int[] {
            40,
            0,
            0,
            0});
            this.numHeartRate.Name = "numHeartRate";
            this.numHeartRate.Size = new System.Drawing.Size(58, 22);
            this.numHeartRate.TabIndex = 11;
            this.numHeartRate.Value = new decimal(new int[] {
            76,
            0,
            0,
            0});
            // 
            // numBodyTemp
            // 
            this.numBodyTemp.DecimalPlaces = 1;
            this.numBodyTemp.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.numBodyTemp.Location = new System.Drawing.Point(117, 77);
            this.numBodyTemp.Maximum = new decimal(new int[] {
            106,
            0,
            0,
            0});
            this.numBodyTemp.Minimum = new decimal(new int[] {
            95,
            0,
            0,
            0});
            this.numBodyTemp.Name = "numBodyTemp";
            this.numBodyTemp.Size = new System.Drawing.Size(58, 22);
            this.numBodyTemp.TabIndex = 10;
            this.numBodyTemp.Value = new decimal(new int[] {
            980,
            0,
            0,
            65536});
            // 
            // lblHeartRate
            // 
            this.lblHeartRate.AutoSize = true;
            this.lblHeartRate.Location = new System.Drawing.Point(193, 80);
            this.lblHeartRate.Name = "lblHeartRate";
            this.lblHeartRate.Size = new System.Drawing.Size(69, 16);
            this.lblHeartRate.TabIndex = 9;
            this.lblHeartRate.Text = "Heart rate:";
            // 
            // lblBodyTemp
            // 
            this.lblBodyTemp.AutoSize = true;
            this.lblBodyTemp.Location = new System.Drawing.Point(12, 83);
            this.lblBodyTemp.Name = "lblBodyTemp";
            this.lblBodyTemp.Size = new System.Drawing.Size(98, 16);
            this.lblBodyTemp.TabIndex = 8;
            this.lblBodyTemp.Text = "Body temp (°F):";
            // 
            // numBloodSugar
            // 
            this.numBloodSugar.DecimalPlaces = 1;
            this.numBloodSugar.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.numBloodSugar.Location = new System.Drawing.Point(289, 53);
            this.numBloodSugar.Maximum = new decimal(new int[] {
            25,
            0,
            0,
            0});
            this.numBloodSugar.Minimum = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.numBloodSugar.Name = "numBloodSugar";
            this.numBloodSugar.Size = new System.Drawing.Size(58, 22);
            this.numBloodSugar.TabIndex = 7;
            this.numBloodSugar.Value = new decimal(new int[] {
            70,
            0,
            0,
            65536});
            // 
            // lblBloodSugar
            // 
            this.lblBloodSugar.AutoSize = true;
            this.lblBloodSugar.Location = new System.Drawing.Point(193, 53);
            this.lblBloodSugar.Name = "lblBloodSugar";
            this.lblBloodSugar.Size = new System.Drawing.Size(83, 16);
            this.lblBloodSugar.TabIndex = 6;
            this.lblBloodSugar.Text = "Blood sugar:";
            // 
            // numDiastolic
            // 
            this.numDiastolic.Location = new System.Drawing.Point(117, 52);
            this.numDiastolic.Maximum = new decimal(new int[] {
            130,
            0,
            0,
            0});
            this.numDiastolic.Minimum = new decimal(new int[] {
            40,
            0,
            0,
            0});
            this.numDiastolic.Name = "numDiastolic";
            this.numDiastolic.Size = new System.Drawing.Size(58, 22);
            this.numDiastolic.TabIndex = 5;
            this.numDiastolic.Value = new decimal(new int[] {
            80,
            0,
            0,
            0});
            // 
            // lblDiastolic
            // 
            this.lblDiastolic.AutoSize = true;
            this.lblDiastolic.Location = new System.Drawing.Point(12, 55);
            this.lblDiastolic.Name = "lblDiastolic";
            this.lblDiastolic.Size = new System.Drawing.Size(83, 16);
            this.lblDiastolic.TabIndex = 4;
            this.lblDiastolic.Text = "Diastolic BP:";
            // 
            // numSystolic
            // 
            this.numSystolic.Location = new System.Drawing.Point(289, 24);
            this.numSystolic.Maximum = new decimal(new int[] {
            200,
            0,
            0,
            0});
            this.numSystolic.Minimum = new decimal(new int[] {
            60,
            0,
            0,
            0});
            this.numSystolic.Name = "numSystolic";
            this.numSystolic.Size = new System.Drawing.Size(58, 22);
            this.numSystolic.TabIndex = 3;
            this.numSystolic.Value = new decimal(new int[] {
            120,
            0,
            0,
            0});
            // 
            // lblSystolic
            // 
            this.lblSystolic.AutoSize = true;
            this.lblSystolic.Location = new System.Drawing.Point(193, 27);
            this.lblSystolic.Name = "lblSystolic";
            this.lblSystolic.Size = new System.Drawing.Size(78, 16);
            this.lblSystolic.TabIndex = 2;
            this.lblSystolic.Text = "Systolic BP:";
            // 
            // numAge
            // 
            this.numAge.Location = new System.Drawing.Point(117, 24);
            this.numAge.Maximum = new decimal(new int[] {
            80,
            0,
            0,
            0});
            this.numAge.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numAge.Name = "numAge";
            this.numAge.Size = new System.Drawing.Size(58, 22);
            this.numAge.TabIndex = 1;
            this.numAge.Value = new decimal(new int[] {
            25,
            0,
            0,
            0});
            // 
            // lblAge
            // 
            this.lblAge.AutoSize = true;
            this.lblAge.Location = new System.Drawing.Point(12, 27);
            this.lblAge.Name = "lblAge";
            this.lblAge.Size = new System.Drawing.Size(35, 16);
            this.lblAge.TabIndex = 0;
            this.lblAge.Text = "Age:";
            // 
            // txtHiddenList
            // 
            this.txtHiddenList.Location = new System.Drawing.Point(105, 13);
            this.txtHiddenList.Name = "txtHiddenList";
            this.txtHiddenList.Size = new System.Drawing.Size(150, 22);
            this.txtHiddenList.TabIndex = 1;
            this.txtHiddenList.Text = "2, 4, 6, 8";
            // 
            // lblEpochList
            // 
            this.lblEpochList.AutoSize = true;
            this.lblEpochList.Location = new System.Drawing.Point(275, 16);
            this.lblEpochList.Name = "lblEpochList";
            this.lblEpochList.Size = new System.Drawing.Size(91, 16);
            this.lblEpochList.TabIndex = 3;
            this.lblEpochList.Text = "Epoch counts:";
            // 
            // lblRepeats
            // 
            this.lblRepeats.AutoSize = true;
            this.lblRepeats.Location = new System.Drawing.Point(580, 16);
            this.lblRepeats.Name = "lblRepeats";
            this.lblRepeats.Size = new System.Drawing.Size(103, 16);
            this.lblRepeats.TabIndex = 4;
            this.lblRepeats.Text = "Runs per config:";
            // 
            // lblTuningNote
            // 
            this.lblTuningNote.AutoSize = true;
            this.lblTuningNote.ForeColor = System.Drawing.Color.DimGray;
            this.lblTuningNote.Location = new System.Drawing.Point(175, 54);
            this.lblTuningNote.Name = "lblTuningNote";
            this.lblTuningNote.Size = new System.Drawing.Size(512, 16);
            this.lblTuningNote.TabIndex = 5;
            this.lblTuningNote.Text = "Each configuration is trained several times and averaged. The best row is highlig" +
    "hted.";
            // 
            // lblHiddenList
            // 
            this.lblHiddenList.AutoSize = true;
            this.lblHiddenList.Location = new System.Drawing.Point(12, 16);
            this.lblHiddenList.Name = "lblHiddenList";
            this.lblHiddenList.Size = new System.Drawing.Size(88, 16);
            this.lblHiddenList.TabIndex = 6;
            this.lblHiddenList.Text = "Hidden sizes:";
            // 
            // txtEpochList
            // 
            this.txtEpochList.Location = new System.Drawing.Point(370, 13);
            this.txtEpochList.Name = "txtEpochList";
            this.txtEpochList.Size = new System.Drawing.Size(190, 22);
            this.txtEpochList.TabIndex = 7;
            this.txtEpochList.Text = "10, 50, 100, 500, 1000, 2000";
            // 
            // btnRunTuning
            // 
            this.btnRunTuning.Location = new System.Drawing.Point(12, 46);
            this.btnRunTuning.Name = "btnRunTuning";
            this.btnRunTuning.Size = new System.Drawing.Size(150, 32);
            this.btnRunTuning.TabIndex = 8;
            this.btnRunTuning.Text = "Run grid search";
            this.btnRunTuning.UseVisualStyleBackColor = true;
            // 
            // numRepeats
            // 
            this.numRepeats.Location = new System.Drawing.Point(690, 13);
            this.numRepeats.Maximum = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.numRepeats.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numRepeats.Name = "numRepeats";
            this.numRepeats.Size = new System.Drawing.Size(60, 22);
            this.numRepeats.TabIndex = 9;
            this.numRepeats.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            // 
            // dgvTuning
            // 
            this.dgvTuning.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvTuning.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTuning.Location = new System.Drawing.Point(12, 90);
            this.dgvTuning.Name = "dgvTuning";
            this.dgvTuning.RowHeadersWidth = 51;
            this.dgvTuning.RowTemplate.Height = 24;
            this.dgvTuning.Size = new System.Drawing.Size(805, 565);
            this.dgvTuning.TabIndex = 11;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1262, 753);
            this.Controls.Add(this.grpPredict);
            this.Controls.Add(this.grpEvaluate);
            this.Controls.Add(this.grpNetwork);
            this.Controls.Add(this.grpData);
            this.Controls.Add(this.tabMain);
            this.Controls.Add(this.statusStrip);
            this.MinimumSize = new System.Drawing.Size(1280, 800);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Maternal Health Risk Classification - Backpropagation Neural Network";
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.tabMain.ResumeLayout(false);
            this.tabLog.ResumeLayout(false);
            this.tabLog.PerformLayout();
            this.tabLoss.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picLoss)).EndInit();
            this.tabEval.ResumeLayout(false);
            this.tabEval.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.tabData.ResumeLayout(false);
            this.tabData.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvData)).EndInit();
            this.tabTuning.ResumeLayout(false);
            this.tabTuning.PerformLayout();
            this.grpData.ResumeLayout(false);
            this.grpData.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numSeed)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numTrainPct)).EndInit();
            this.grpNetwork.ResumeLayout(false);
            this.grpNetwork.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numLogEvery)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numHidden)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numEpochs)).EndInit();
            this.grpEvaluate.ResumeLayout(false);
            this.grpPredict.ResumeLayout(false);
            this.grpPredict.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numHeartRate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numBodyTemp)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numBloodSugar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numDiastolic)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numSystolic)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numAge)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numRepeats)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTuning)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel lblStatus;
        private System.Windows.Forms.ToolStripProgressBar progressBar;
        private System.Windows.Forms.TabControl tabMain;
        private System.Windows.Forms.TabPage tabLog;
        private System.Windows.Forms.TabPage tabLoss;
        private System.Windows.Forms.TabPage tabEval;
        private System.Windows.Forms.TabPage tabData;
        private System.Windows.Forms.TabPage tabTuning;
        private System.Windows.Forms.TextBox txtLog;
        private System.Windows.Forms.DataGridView dgvData;
        private System.Windows.Forms.GroupBox grpData;
        private System.Windows.Forms.Label lblSeed;
        private System.Windows.Forms.Label lblTrainPct;
        private System.Windows.Forms.Label lblDataInfo;
        private System.Windows.Forms.Label lblFile;
        private System.Windows.Forms.Button btnLoad;
        private System.Windows.Forms.NumericUpDown numTrainPct;
        private System.Windows.Forms.NumericUpDown numSeed;
        private System.Windows.Forms.Button btnPrepare;
        private System.Windows.Forms.CheckBox chkRemoveImpossible;
        private System.Windows.Forms.CheckBox chkRemoveDuplicates;
        private System.Windows.Forms.CheckBox chkStratified;
        private System.Windows.Forms.RadioButton rdoScaled;
        private System.Windows.Forms.RadioButton rdoCleaned;
        private System.Windows.Forms.RadioButton rdoRaw;
        private System.Windows.Forms.Label lblDataLegend;
        private System.Windows.Forms.GroupBox grpNetwork;
        private System.Windows.Forms.Label lblLogEvery;
        private System.Windows.Forms.NumericUpDown numHidden;
        private System.Windows.Forms.Label lblEpochs;
        private System.Windows.Forms.NumericUpDown numEpochs;
        private System.Windows.Forms.Label lblHidden;
        private System.Windows.Forms.Label lblArch;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.Button btnTrain;
        private System.Windows.Forms.Button btnCreate;
        private System.Windows.Forms.Label lblLearnRate;
        private System.Windows.Forms.NumericUpDown numLogEvery;
        private System.Windows.Forms.GroupBox grpEvaluate;
        private System.Windows.Forms.Button btnExport;
        private System.Windows.Forms.Button btnEvaluate;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label lblConfusionTitle;
        private System.Windows.Forms.Label lblAccuracy;
        private System.Windows.Forms.DataGridView dataGridView2;
        private System.Windows.Forms.Label lblMetricsTitle;
        private System.Windows.Forms.PictureBox picLoss;
        private System.Windows.Forms.GroupBox grpPredict;
        private System.Windows.Forms.Label lblBloodSugar;
        private System.Windows.Forms.NumericUpDown numDiastolic;
        private System.Windows.Forms.Label lblDiastolic;
        private System.Windows.Forms.NumericUpDown numSystolic;
        private System.Windows.Forms.Label lblSystolic;
        private System.Windows.Forms.NumericUpDown numAge;
        private System.Windows.Forms.Label lblAge;
        private System.Windows.Forms.NumericUpDown numBloodSugar;
        private System.Windows.Forms.NumericUpDown numHeartRate;
        private System.Windows.Forms.NumericUpDown numBodyTemp;
        private System.Windows.Forms.Label lblHeartRate;
        private System.Windows.Forms.Label lblBodyTemp;
        private System.Windows.Forms.Label lblPrediction;
        private System.Windows.Forms.Button btnPredict;
        private System.Windows.Forms.NumericUpDown numRepeats;
        private System.Windows.Forms.Button btnRunTuning;
        private System.Windows.Forms.TextBox txtEpochList;
        private System.Windows.Forms.Label lblHiddenList;
        private System.Windows.Forms.Label lblTuningNote;
        private System.Windows.Forms.Label lblRepeats;
        private System.Windows.Forms.Label lblEpochList;
        private System.Windows.Forms.TextBox txtHiddenList;
        private System.Windows.Forms.DataGridView dgvTuning;
    }
}

