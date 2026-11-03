using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace MaternalRiskPredictor
{
    /// <summary>
    /// Data part of the main window (owner: UI + preprocessing).
    /// Loading the CSV, cleaning, the stratified split, min-max scaling, and the Data tab views.
    /// Filled in during phases P1 to P3.
    /// </summary>
    public partial class Form1
    {
        /// <summary>Connects the data buttons. Called once from the constructor.</summary>
        private void WireDataEvents()
        {
            // P1: btnLoad.Click
            // P2: btnPrepare.Click
            // P3: rdoRaw / rdoCleaned / rdoScaled .CheckedChanged
        }
    }
}