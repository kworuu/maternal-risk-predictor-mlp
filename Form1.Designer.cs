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
            this.tabEval = new System.Windows.Forms.TabPage();
            this.tabData = new System.Windows.Forms.TabPage();
            this.dgvData = new System.Windows.Forms.DataGridView();
            this.tabTuning = new System.Windows.Forms.TabPage();
            this.grpData = new System.Windows.Forms.GroupBox();
            this.btnLoad = new System.Windows.Forms.Button();
            this.lblFile = new System.Windows.Forms.Label();
            this.lblDataInfo = new System.Windows.Forms.Label();
            this.lblTrainPct = new System.Windows.Forms.Label();
            this.lblSeed = new System.Windows.Forms.Label();
            this.numTrainPct = new System.Windows.Forms.NumericUpDown();
            this.numSeed = new System.Windows.Forms.NumericUpDown();
            this.chkStratified = new System.Windows.Forms.CheckBox();
            this.chkRemoveDuplicates = new System.Windows.Forms.CheckBox();
            this.chkRemoveImpossible = new System.Windows.Forms.CheckBox();
            this.btnPrepare = new System.Windows.Forms.Button();
            this.rdoRaw = new System.Windows.Forms.RadioButton();
            this.rdoCleaned = new System.Windows.Forms.RadioButton();
            this.rdoScaled = new System.Windows.Forms.RadioButton();
            this.lblDataLegend = new System.Windows.Forms.Label();
            this.grpNetwork = new System.Windows.Forms.GroupBox();
            this.lblArch = new System.Windows.Forms.Label();
            this.lblHidden = new System.Windows.Forms.Label();
            this.numEpochs = new System.Windows.Forms.NumericUpDown();
            this.lblEpochs = new System.Windows.Forms.Label();
            this.numHidden = new System.Windows.Forms.NumericUpDown();
            this.lblLogEvery = new System.Windows.Forms.Label();
            this.numLogEvery = new System.Windows.Forms.NumericUpDown();
            this.lblLearnRate = new System.Windows.Forms.Label();
            this.btnCreate = new System.Windows.Forms.Button();
            this.btnTrain = new System.Windows.Forms.Button();
            this.btnStop = new System.Windows.Forms.Button();
            this.grpEvaluate = new System.Windows.Forms.GroupBox();
            this.btnEvaluate = new System.Windows.Forms.Button();
            this.btnExport = new System.Windows.Forms.Button();
            this.lblAccuracy = new System.Windows.Forms.Label();
            this.lblConfusionTitle = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.lblMetricsTitle = new System.Windows.Forms.Label();
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.statusStrip.SuspendLayout();
            this.tabMain.SuspendLayout();
            this.tabLog.SuspendLayout();
            this.tabEval.SuspendLayout();
            this.tabData.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvData)).BeginInit();
            this.grpData.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numTrainPct)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numSeed)).BeginInit();
            this.grpNetwork.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numEpochs)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numHidden)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numLogEvery)).BeginInit();
            this.grpEvaluate.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            this.SuspendLayout();
            // 
            // statusStrip
            // 
            this.statusStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblStatus,
            this.progressBar});
            this.statusStrip.Location = new System.Drawing.Point(0, 666);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(1114, 26);
            this.statusStrip.TabIndex = 0;
            this.statusStrip.Text = "statusStrip1";
            // 
            // lblStatus
            // 
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(897, 20);
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
            this.tabMain.Location = new System.Drawing.Point(354, 18);
            this.tabMain.Name = "tabMain";
            this.tabMain.SelectedIndex = 0;
            this.tabMain.Size = new System.Drawing.Size(734, 650);
            this.tabMain.TabIndex = 1;
            // 
            // tabLog
            // 
            this.tabLog.Controls.Add(this.txtLog);
            this.tabLog.Location = new System.Drawing.Point(4, 25);
            this.tabLog.Name = "tabLog";
            this.tabLog.Padding = new System.Windows.Forms.Padding(3);
            this.tabLog.Size = new System.Drawing.Size(726, 621);
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
            this.txtLog.Size = new System.Drawing.Size(720, 615);
            this.txtLog.TabIndex = 0;
            this.txtLog.WordWrap = false;
            // 
            // tabLoss
            // 
            this.tabLoss.Location = new System.Drawing.Point(4, 25);
            this.tabLoss.Name = "tabLoss";
            this.tabLoss.Padding = new System.Windows.Forms.Padding(3);
            this.tabLoss.Size = new System.Drawing.Size(726, 621);
            this.tabLoss.TabIndex = 1;
            this.tabLoss.Text = "Loss curve";
            this.tabLoss.UseVisualStyleBackColor = true;
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
            this.tabEval.Size = new System.Drawing.Size(726, 621);
            this.tabEval.TabIndex = 2;
            this.tabEval.Text = "Evaluation";
            this.tabEval.UseVisualStyleBackColor = true;
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
            this.tabData.Size = new System.Drawing.Size(726, 621);
            this.tabData.TabIndex = 3;
            this.tabData.Text = "Data";
            this.tabData.UseVisualStyleBackColor = true;
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
            this.tabTuning.Location = new System.Drawing.Point(4, 25);
            this.tabTuning.Name = "tabTuning";
            this.tabTuning.Padding = new System.Windows.Forms.Padding(3);
            this.tabTuning.Size = new System.Drawing.Size(726, 621);
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
            this.grpData.Size = new System.Drawing.Size(330, 218);
            this.grpData.TabIndex = 2;
            this.grpData.TabStop = false;
            this.grpData.Text = "1. Data";
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
            // lblFile
            // 
            this.lblFile.AutoSize = true;
            this.lblFile.Location = new System.Drawing.Point(120, 27);
            this.lblFile.Name = "lblFile";
            this.lblFile.Size = new System.Drawing.Size(91, 16);
            this.lblFile.TabIndex = 1;
            this.lblFile.Text = "No file loaded";
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
            // lblTrainPct
            // 
            this.lblTrainPct.AutoSize = true;
            this.lblTrainPct.Location = new System.Drawing.Point(12, 84);
            this.lblTrainPct.Name = "lblTrainPct";
            this.lblTrainPct.Size = new System.Drawing.Size(74, 16);
            this.lblTrainPct.TabIndex = 3;
            this.lblTrainPct.Text = "Training %:\t";
            // 
            // lblSeed
            // 
            this.lblSeed.AutoSize = true;
            this.lblSeed.Location = new System.Drawing.Point(175, 84);
            this.lblSeed.Name = "lblSeed";
            this.lblSeed.Size = new System.Drawing.Size(70, 16);
            this.lblSeed.TabIndex = 4;
            this.lblSeed.Text = "Split seed:";
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
            // numSeed
            // 
            this.numSeed.Location = new System.Drawing.Point(248, 81);
            this.numSeed.Maximum = new decimal(new int[] {
            9999,
            0,
            0,
            0});
            this.numSeed.Name = "numSeed";
            this.numSeed.Size = new System.Drawing.Size(70, 22);
            this.numSeed.TabIndex = 6;
            this.numSeed.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
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
            // btnPrepare
            // 
            this.btnPrepare.Location = new System.Drawing.Point(12, 178);
            this.btnPrepare.Name = "btnPrepare";
            this.btnPrepare.Size = new System.Drawing.Size(306, 28);
            this.btnPrepare.TabIndex = 10;
            this.btnPrepare.Text = "Clean + Split + Normalize (Min-Max to [0, 1])";
            this.btnPrepare.UseVisualStyleBackColor = true;
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
            this.grpNetwork.Size = new System.Drawing.Size(330, 206);
            this.grpNetwork.TabIndex = 3;
            this.grpNetwork.TabStop = false;
            this.grpNetwork.Text = "2. Network and training";
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
            // lblHidden
            // 
            this.lblHidden.AutoSize = true;
            this.lblHidden.Location = new System.Drawing.Point(12, 50);
            this.lblHidden.Name = "lblHidden";
            this.lblHidden.Size = new System.Drawing.Size(95, 16);
            this.lblHidden.TabIndex = 1;
            this.lblHidden.Text = "Hidden nodes:";
            // 
            // numEpochs
            // 
            this.numEpochs.Increment = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.numEpochs.Location = new System.Drawing.Point(200, 75);
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
            // lblEpochs
            // 
            this.lblEpochs.AutoSize = true;
            this.lblEpochs.Location = new System.Drawing.Point(12, 78);
            this.lblEpochs.Name = "lblEpochs";
            this.lblEpochs.Size = new System.Drawing.Size(143, 16);
            this.lblEpochs.TabIndex = 3;
            this.lblEpochs.Text = "Epochs per Train click:";
            // 
            // numHidden
            // 
            this.numHidden.Location = new System.Drawing.Point(200, 47);
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
            // lblLogEvery
            // 
            this.lblLogEvery.AutoSize = true;
            this.lblLogEvery.Location = new System.Drawing.Point(12, 106);
            this.lblLogEvery.Name = "lblLogEvery";
            this.lblLogEvery.Size = new System.Drawing.Size(131, 16);
            this.lblLogEvery.TabIndex = 5;
            this.lblLogEvery.Text = "Log every N epochs:";
            // 
            // numLogEvery
            // 
            this.numLogEvery.Location = new System.Drawing.Point(200, 104);
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
            // btnCreate
            // 
            this.btnCreate.Location = new System.Drawing.Point(12, 160);
            this.btnCreate.Name = "btnCreate";
            this.btnCreate.Size = new System.Drawing.Size(96, 32);
            this.btnCreate.TabIndex = 8;
            this.btnCreate.Text = "Create NN";
            this.btnCreate.UseVisualStyleBackColor = true;
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
            // grpEvaluate
            // 
            this.grpEvaluate.Controls.Add(this.btnExport);
            this.grpEvaluate.Controls.Add(this.btnEvaluate);
            this.grpEvaluate.Location = new System.Drawing.Point(12, 448);
            this.grpEvaluate.Name = "grpEvaluate";
            this.grpEvaluate.Size = new System.Drawing.Size(330, 64);
            this.grpEvaluate.TabIndex = 4;
            this.grpEvaluate.TabStop = false;
            this.grpEvaluate.Text = "3. Evaluate";
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
            // btnExport
            // 
            this.btnExport.Location = new System.Drawing.Point(171, 22);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(147, 30);
            this.btnExport.TabIndex = 1;
            this.btnExport.Text = "Export loss log";
            this.btnExport.UseVisualStyleBackColor = true;
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
            // lblConfusionTitle
            // 
            this.lblConfusionTitle.AutoSize = true;
            this.lblConfusionTitle.Location = new System.Drawing.Point(12, 66);
            this.lblConfusionTitle.Name = "lblConfusionTitle";
            this.lblConfusionTitle.Size = new System.Drawing.Size(381, 16);
            this.lblConfusionTitle.TabIndex = 1;
            this.lblConfusionTitle.Text = "Confusion matrix on test set (rows = actual, columns = predicted)";
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
            // lblMetricsTitle
            // 
            this.lblMetricsTitle.AutoSize = true;
            this.lblMetricsTitle.Location = new System.Drawing.Point(13, 215);
            this.lblMetricsTitle.Name = "lblMetricsTitle";
            this.lblMetricsTitle.Size = new System.Drawing.Size(173, 16);
            this.lblMetricsTitle.TabIndex = 3;
            this.lblMetricsTitle.Text = "Per-class metrics on test set";
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
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1114, 692);
            this.Controls.Add(this.grpEvaluate);
            this.Controls.Add(this.grpNetwork);
            this.Controls.Add(this.grpData);
            this.Controls.Add(this.tabMain);
            this.Controls.Add(this.statusStrip);
            this.MinimumSize = new System.Drawing.Size(1132, 739);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Maternal Health Risk Classification - Backpropagation Neural Network";
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.tabMain.ResumeLayout(false);
            this.tabLog.ResumeLayout(false);
            this.tabLog.PerformLayout();
            this.tabEval.ResumeLayout(false);
            this.tabEval.PerformLayout();
            this.tabData.ResumeLayout(false);
            this.tabData.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvData)).EndInit();
            this.grpData.ResumeLayout(false);
            this.grpData.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numTrainPct)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numSeed)).EndInit();
            this.grpNetwork.ResumeLayout(false);
            this.grpNetwork.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numEpochs)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numHidden)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numLogEvery)).EndInit();
            this.grpEvaluate.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
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
    }
}

