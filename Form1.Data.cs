using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace MaternalRiskPredictor
{
    /// <summary>
    /// Data part of the main window (owner: UI + preprocessing).
    /// P1: load the CSV and show it in the Data tab with problem rows flagged.
    /// P2: clean, split and scale.   P3: Cleaned / Scaled views and the hand-off fields.
    /// </summary>
    public partial class Form1
    {
        // ---- data ----
        private List<Patient> rawData;     // every row exactly as loaded from the file
        private bool[] rawIsDuplicate;     // true = exact copy of an earlier row

        private static readonly Color DuplicateBack = Color.FromArgb(236, 236, 236);
        private static readonly Color DuplicateText = Color.Gray;
        private static readonly Color ImpossibleBack = Color.FromArgb(250, 205, 205);

        /// <summary>Connects the data buttons. Called once from the constructor.</summary>
        private void WireDataEvents()
        {
            btnLoad.Click += new EventHandler(btnLoad_Click);
            // P2: btnPrepare.Click
            // P3: rdoRaw / rdoCleaned / rdoScaled .CheckedChanged
        }

        // =====================================================================
        // P1: Load
        // =====================================================================

        private void btnLoad_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog dlg = new OpenFileDialog())
            {
                dlg.Filter = "CSV files (*.csv)|*.csv|All files (*.*)|*.*";
                dlg.Title = "Open the Maternal Health Risk dataset";
                dlg.InitialDirectory = Application.StartupPath;           // folder of the .exe
                dlg.FileName = "Maternal_Health_Risk_Data_Set.csv";        // pre-selected
                if (dlg.ShowDialog() != DialogResult.OK)
                    return;

                try
                {
                    int skipped;
                    List<Patient> loaded = MaternalData.LoadCsv(dlg.FileName, out skipped);
                    if (loaded.Count == 0)
                    {
                        Warn("No valid rows were found. Expected 6 numbers and a risk level on each line.");
                        return;
                    }

                    rawData = loaded;
                    rawIsDuplicate = MaternalData.FindDuplicates(rawData);

                    string fileName = Path.GetFileName(dlg.FileName);
                    int[] counts = MaternalData.CountPerClass(rawData);
                    lblFile.Text = fileName;
                    lblDataInfo.Text = string.Format("{0:N0} rows: {1} low, {2} mid, {3} high risk",
                        rawData.Count, counts[0], counts[1], counts[2]);

                    Log(string.Format("Loaded {0}: {1} rows, {2} skipped (invalid lines).",
                        fileName, rawData.Count, skipped));
                    Log(string.Format("Class counts: Low {0}, Mid {1}, High {2}", counts[0], counts[1], counts[2]));
                    LogDataCheck();
                    Log("");

                    // a new file invalidates any earlier cleaning
                    rdoRaw.Checked = true;
                    rdoCleaned.Enabled = false;
                    rdoScaled.Enabled = false;

                    ShowRawView();
                    tabMain.SelectedTab = tabData;
                    SetStatus("Data loaded. Next: Clean + Split + Normalize.");
                }
                catch (Exception ex)
                {
                    Warn("Could not read the file:\n" + ex.Message);
                }
            }
        }

        /// <summary>Reports the data-quality problems found in the raw file.</summary>
        private void LogDataCheck()
        {
            int duplicates = 0;
            foreach (bool d in rawIsDuplicate)
                if (d) duplicates++;

            List<string> impossibleRows = new List<string>();
            foreach (Patient p in rawData)
                if (MaternalData.IsImpossible(p))
                    impossibleRows.Add(p.RowNumber.ToString());

            Log("Data check:");
            Log(string.Format("  Duplicate rows (exact copies of an earlier row): {0} of {1} ({2:F1}%)",
                duplicates, rawData.Count, 100.0 * duplicates / rawData.Count));
            Log(string.Format("  Unique rows: {0}", rawData.Count - duplicates));
            Log(string.Format("  Impossible heart rate (< {0} bpm): {1} rows (row {2})",
                MaternalData.MinPossibleHeartRate, impossibleRows.Count, string.Join(", ", impossibleRows.ToArray())));
            Log(string.Format("  Identical vitals with different risk labels: {0} sets (cannot be fixed by cleaning)",
                MaternalData.CountContradictions(rawData)));
        }

        /// <summary>
        /// Data tab, Raw view: every row as loaded. Duplicate rows are grey and
        /// impossible values are red, so the reader sees what cleaning will remove.
        /// </summary>
        private void ShowRawView()
        {
            dgvData.SuspendLayout();
            dgvData.Rows.Clear();
            dgvData.Columns.Clear();

            dgvData.Columns.Add("colRow", "#");
            for (int c = 0; c < MaternalData.NumFeatures; c++)
                dgvData.Columns.Add("colF" + c, MaternalData.ShortNames[c]);
            dgvData.Columns.Add("colRisk", "Risk level");
            dgvData.Columns.Add("colStatus", "Status");

            for (int i = 0; i < rawData.Count; i++)
            {
                Patient p = rawData[i];
                bool impossible = MaternalData.IsImpossible(p);

                object[] cells = new object[MaternalData.NumFeatures + 3];
                cells[0] = p.RowNumber;
                for (int c = 0; c < MaternalData.NumFeatures; c++)
                    cells[c + 1] = p.Features[c];
                cells[MaternalData.NumFeatures + 1] = MaternalData.ClassNames[p.Label];

                string status = "";
                if (rawIsDuplicate[i]) status = "duplicate - will be removed";
                else if (impossible) status = "impossible value - will be removed";
                cells[MaternalData.NumFeatures + 2] = status;

                int row = dgvData.Rows.Add(cells);

                if (rawIsDuplicate[i])
                {
                    dgvData.Rows[row].DefaultCellStyle.BackColor = DuplicateBack;
                    dgvData.Rows[row].DefaultCellStyle.ForeColor = DuplicateText;
                }
                if (impossible)
                    dgvData.Rows[row].Cells[MaternalData.HeartRateIndex + 1].Style.BackColor = ImpossibleBack;
            }
            dgvData.ResumeLayout();
        }
    }
}