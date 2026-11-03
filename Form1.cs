using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MaternalRiskPredictor
{
    /* 
       Form1 is split into partial files:
           Form1.cs       - constructor and helpers used by the other two files
           Form1.Data.cs  - loading, cleaning, splitting and scaling the dataset
           Form1.model.cs - creating, training, evaluating and tuning the network
    */
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            SetupGrid(dgvData);
            SetupGrid(dgvConfusion);
            SetupGrid(dgvMetrics);
            SetupGrid(dgvTuning);

            Log("Maternal health risk classifier using the provided backpropagation engine (NeuralNet, INeuron, HNeuron, ONeuron).");
            Log("Steps: Load CSV -> Clean + Split + Normalize -> Create NN -> Train NN -> Evaluate.");
            Log("");

          
            WireDataEvents();   // Form1.Data.cs
            WireModelEvents();  // Form1.Model.cs
        }

        protected static void SetupGrid(DataGridView grid)
        {
            grid.ReadOnly = true;
            grid.AllowUserToAddRows = false;
            grid.AllowUserToDeleteRows = false;
            grid.AllowUserToResizeRows = false;
            grid.RowHeadersVisible = false;
            grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            grid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            grid.BackgroundColor = Color.White;
        }

        protected void Log(string line)
        {
            txtLog.AppendText(line + Environment.NewLine);
        }
        protected void SetStatus(string text)
        {
            lblStatus.Text = text;
        }

        protected void Warn(string message)
        {
            MessageBox.Show(this, message, "Maternal Health Risk Classification",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        // Locks the inputs while training or tuning runs in the background, so the data
        // and the network can't change underneath it. Only Stop stays enabled. 
        protected void SetBusy(bool busy)
        {
            grpData.Enabled = !busy;
            btnCreate.Enabled = !busy;
            btnTrain.Enabled = !busy;
            numHidden.Enabled = !busy;
            numEpochs.Enabled = !busy;
            numLogEvery.Enabled = !busy;
            grpEvaluate.Enabled = !busy;
            grpPredict.Enabled = !busy;
            btnRunTuning.Enabled = !busy;
            btnStop.Enabled = busy;
            if (!busy) progressBar.Value = 0;
        }
    }
}
