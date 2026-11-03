using System;
using System.Collections.Generic;
using System.Windows.Forms;

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

        /// <summary>Connects the model buttons. Called once from the constructor.</summary>
        private void WireModelEvents()
        {
            btnCreate.Click += new EventHandler(btnCreate_Click);
            // L2: btnTrain, btnStop, bgwTrain events
            // L3: btnExport
            // L4: btnEvaluate
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

            Log(string.Format("Created a {0}-{1}-{2} network with random weights.",
                MaternalData.NumFeatures, hidden, MaternalData.NumClasses));
            Log(string.Format("  Before training: train MSE {0:F5}, test accuracy {1:P1} (random guessing)",
                trainer.ComputeMse(trainSet), trainer.ComputeAccuracy(testSet)));
            Log("");
            tabMain.SelectedTab = tabLog;
            SetStatus("Network created. Next: Train NN.");
        }
    }
}
