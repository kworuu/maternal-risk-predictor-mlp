using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

namespace MaternalRiskPredictor
{

    // One patient record: 6 vital signs and the risk level (0 = low, 1 = mid, 2 = high).

    public class Patient
    {
        public double[] Features; // Age, SystolicBP, DiastolicBP, BS, BodyTemp, HeartRate
        public int Label;         // 0 = low risk, 1 = mid risk, 2 = high risk
        public int RowNumber;     // line number in the CSV (1 = first patient), for display

        public Patient(double[] features, int label, int rowNumber)
        {
            Features = features;
            Label = label;
            RowNumber = rowNumber;
        }

        // Two rows get the same key only if all 6 values and the label match.
        public string RowKey()
        {
            return FeatureKey() + "|" + Label;
        }

        public string FeatureKey()
        {
            string[] parts = new string[Features.Length];
            for (int i = 0; i < Features.Length; i++)
                parts[i] = Features[i].ToString("R", CultureInfo.InvariantCulture);
            return string.Join(",", parts);
        }
    }

  
    // Describes the UCI Maternal Health Risk dataset and loads it from CSV.
    // Source: Ahmed, M. (2020). Maternal Health Risk [Dataset]. UCI Machine Learning Repository.
    // https://doi.org/10.24432/C5DP5D (CC BY 4.0)

    public static class MaternalData
    {
        public const int NumFeatures = 6;
        public const int NumClasses = 3;

        public static readonly string[] FeatureNames =
            { "Age", "SystolicBP", "DiastolicBP", "BS", "BodyTemp", "HeartRate" };

        // short names for grid headers
        public static readonly string[] ShortNames =
            { "Age", "Systolic", "Diastolic", "BS", "Temp", "HR" };

        public static readonly string[] ClassNames = { "Low risk", "Mid risk", "High risk" };

        public const int HeartRateIndex = 5;
        public const double MinPossibleHeartRate = 40.0; // below this is not a living resting heart rate

        // Expects a header row, then 6 numbers and a label ("low risk", "mid risk", "high risk")
        // per line. Lines that can't be parsed are skipped and counted.

        public static List<Patient> LoadCsv(string path, out int skippedRows)
        {
            List<Patient> patients = new List<Patient>();
            skippedRows = 0;

           
            string[] lines = File.ReadAllLines(path);
            int rowNumber = 0;

            for (int i = 0; i < lines.Length; i++)
            {
                string line = lines[i].Trim();
                if (line.Length == 0)
                    continue; // blank line, e.g. at the end of the file

                string[] parts = line.Split(',');
                double[] features = new double[NumFeatures];
                bool ok = (parts.Length == NumFeatures + 1);

                for (int c = 0; ok && c < NumFeatures; c++)
                {
                    string cell = parts[c].Trim().Trim('"');
                    ok = double.TryParse(cell, NumberStyles.Float, CultureInfo.InvariantCulture, out features[c]);
                }

                int label = ok ? ParseLabel(parts[NumFeatures]) : -1;
                if (!ok || label < 0)
                {
                    if (i > 0) skippedRows++; // line 0 is the header, not an error
                    continue;
                }

                rowNumber++;
                patients.Add(new Patient(features, label, rowNumber));
            }
            return patients;
        }

        // low risk" -> 0, "mid risk" -> 1, "high risk" -> 2, anything else -> -1.
        public static int ParseLabel(string text)
        {
            string s = text.Trim().Trim('"').ToLowerInvariant();
            if (s.StartsWith("low")) return 0;
            if (s.StartsWith("mid")) return 1;
            if (s.StartsWith("high")) return 2;
            return -1;
        }

        // Returns { low count, mid count, high count }.
        public static int[] CountPerClass(List<Patient> data)
        {
            int[] counts = new int[NumClasses];
            foreach (Patient p in data)
                counts[p.Label]++;
            return counts;
        }

     
        // For each row: true if it is an exact copy (all values and label) of an EARLIER row.
        // The first occurrence is kept; later copies are the duplicates.
       
        public static bool[] FindDuplicates(List<Patient> data)
        {
            bool[] isDuplicate = new bool[data.Count];
            Dictionary<string, bool> seen = new Dictionary<string, bool>();
            for (int i = 0; i < data.Count; i++)
            {
                string key = data[i].RowKey();
                if (seen.ContainsKey(key))
                    isDuplicate[i] = true;
                else
                    seen.Add(key, true);
            }
            return isDuplicate;
        }

        // A resting heart rate under 40 bpm isn't realistic, so we treat it as a data entry error.
        public static bool IsImpossible(Patient p)
        {
            return p.Features[HeartRateIndex] < MinPossibleHeartRate;
        }

   
        // Number of distinct sets of 6 vital signs that appear with MORE THAN ONE risk label.
        // These contradictions cannot be fixed by cleaning; they limit the possible accuracy.
      
        public static int CountContradictions(List<Patient> data)
        {
            Dictionary<string, int> firstLabel = new Dictionary<string, int>();
            Dictionary<string, bool> contradicted = new Dictionary<string, bool>();
            foreach (Patient p in data)
            {
                string key = p.FeatureKey();
                int label;
                if (!firstLabel.TryGetValue(key, out label))
                    firstLabel.Add(key, p.Label);
                else if (label != p.Label && !contradicted.ContainsKey(key))
                    contradicted.Add(key, true);
            }
            return contradicted.Count;
        }
    }
}
