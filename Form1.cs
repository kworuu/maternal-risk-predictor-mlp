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
    /// <summary>
    /// Main window: Maternal Health Risk Classification.
    ///
    /// This class is split across three files (partial class) so two people can work
    /// without editing the same file:
    ///   Form1.cs        - constructor and shared helpers         (UI + preprocessing owner)
    ///   Form1.Data.cs   - loading, cleaning, split, scaling      (UI + preprocessing owner)
    ///   Form1.Model.cs  - network, training, evaluation, tuning  (logic owner)
    /// The controls themselves live in Form1.Designer.cs (only the UI owner opens the Designer).
    /// </summary>
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

            // Buttons are connected in code, never by double-clicking in the Designer.
            WireDataEvents();   // Form1.Data.cs
            WireModelEvents();  // Form1.Model.cs
        }

        // =====================================================================
        // Shared helpers (both partial files may call these)
        // =====================================================================

        /// <summary>Read-only grid, no empty "new row", columns fill the width.</summary>
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

        /// <summary>Adds one line to the Training log tab.</summary>
        protected void Log(string line)
        {
            txtLog.AppendText(line + Environment.NewLine);
        }

        /// <summary>Text in the status bar at the bottom left.</summary>
        protected void SetStatus(string text)
        {
            lblStatus.Text = text;
        }

        /// <summary>Simple information popup.</summary>
        protected void Warn(string message)
        {
            MessageBox.Show(this, message, "Maternal Health Risk Classification",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        /// <summary>
        /// Disables every input while a background job (training or grid search) runs,
        /// so nothing can change the data or the network underneath it. Stop is the only
        /// button left enabled.
        /// </summary>
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
