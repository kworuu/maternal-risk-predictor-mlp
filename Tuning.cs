using System;
using System.Collections.Generic;
using System.Globalization;

namespace MaternalRiskPredictor
{

    // Scores of one setting (hidden nodes x epochs) over several repeated runs (L6).
    // Every run starts from new random weights and a new shuffle order, so the scores
    // differ a little from run to run; the mean and standard deviation summarise that.

    public class TuningResult
    {
        public int Hidden;
        public int Epochs;
        public List<double> TestAccuracy = new List<double>();
        public List<double> TrainAccuracy = new List<double>();
        public List<double> TestMse = new List<double>();
        public List<double> MacroF1 = new List<double>();

        public int Runs { get { return TestAccuracy.Count; } }
    }


    public class TuningJob
    {
        public List<int> HiddenList;
        public List<int> EpochList;   // sorted ascending; the last one is how far each run trains
        public int Repeats;
        public int Seed;
    }

    public static class Tuning
    {
        // Turns "2, 4, 6, 8" into a sorted list without duplicates.
        public static bool TryParseList(string text, int min, int max, out List<int> values, out string error)
        {
            values = new List<int>();
            error = null;
            string[] parts = text.Split(new[] { ',', ';', ' ' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string part in parts)
            {
                int v;
                if (!int.TryParse(part.Trim(), NumberStyles.Integer, CultureInfo.InvariantCulture, out v) || v < min || v > max)
                {
                    error = string.Format("\"{0}\" is not a whole number from {1} to {2}.", part.Trim(), min, max);
                    return false;
                }
                if (!values.Contains(v)) values.Add(v);
            }
            if (values.Count == 0)
            {
                error = "The list is empty.";
                return false;
            }
            values.Sort();
            return true;
        }

        public static double Mean(List<double> xs)
        {
            if (xs.Count == 0) return 0;
            double sum = 0;
            foreach (double x in xs) sum += x;
            return sum / xs.Count;
        }

        // Sample standard deviation (divides by n - 1); 0 with fewer than 2 runs.
        public static double Std(List<double> xs)
        {
            if (xs.Count < 2) return 0;
            double mean = Mean(xs), sum = 0;
            foreach (double x in xs) sum += (x - mean) * (x - mean);
            return Math.Sqrt(sum / (xs.Count - 1));
        }
    }
}
