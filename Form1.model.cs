using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace MaternalRiskPredictor
{
    /// <summary>
    /// Model part of the main window (owner: logic partner).
    /// Create / Train / Stop, loss chart, export, evaluation, classify one patient, tuning.
    /// Filled in during phases L1 to L7. The UI owner created this file empty and does not edit it.
    /// </summary>
    public partial class Form1
    {
        /// <summary>Connects the model buttons. Called once from the constructor.</summary>
        private void WireModelEvents()
        {
            // L2: btnCreate, btnTrain, btnStop, bgwTrain events
            // L3: btnExport
            // L4: btnEvaluate
            // L5: btnPredict
            // L6: btnRunTuning, bgwTuning events
        }
    }
}