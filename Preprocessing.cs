using System;
using System.Collections.Generic;

namespace MaternalRiskPredictor
{
    /// <summary>
    /// Cleaning and splitting. Every method returns NEW lists, so the raw data is never changed.
    /// Correct order (see Form1.Data.cs):
    ///   1. RemoveDuplicates   - on the whole file, BEFORE splitting, so no patient can end up
    ///                           in both the training and the test set (that would be data leakage)
    ///   2. RemoveImpossible   - heart rate below 40 bpm
    ///   3. Split              - stratified 80/20 with a fixed seed
    ///   4. MinMaxScaler.Fit   - on the TRAINING set only, then Transform both sets
    /// </summary>
    public static class Preprocessing
    {
        /// <summary>Keeps the first occurrence of each row and drops later exact copies.</summary>
        public static List<Patient> RemoveDuplicates(List<Patient> data, out int removed)
        {
            bool[] isDuplicate = MaternalData.FindDuplicates(data);
            List<Patient> result = new List<Patient>();
            for (int i = 0; i < data.Count; i++)
                if (!isDuplicate[i]) result.Add(data[i]);
            removed = data.Count - result.Count;
            return result;
        }

        /// <summary>Drops rows with a physiologically impossible value (heart rate below 40 bpm).</summary>
        public static List<Patient> RemoveImpossible(List<Patient> data, out int removed)
        {
            List<Patient> result = new List<Patient>();
            foreach (Patient p in data)
                if (!MaternalData.IsImpossible(p)) result.Add(p);
            removed = data.Count - result.Count;
            return result;
        }

        /// <summary>
        /// Shuffles with a fixed seed and splits into training and test sets.
        /// Stratified = each risk level is split separately, so both sets keep the same
        /// low / mid / high proportions as the whole dataset.
        /// </summary>
        public static void Split(List<Patient> data, double trainFraction, int seed, bool stratified,
            out List<Patient> train, out List<Patient> test)
        {
            Random rnd = new Random(seed);
            train = new List<Patient>();
            test = new List<Patient>();

            if (stratified)
            {
                for (int c = 0; c < MaternalData.NumClasses; c++)
                {
                    List<Patient> group = new List<Patient>();
                    foreach (Patient p in data)
                        if (p.Label == c) group.Add(p);

                    Shuffle(group, rnd);
                    int numTrain = (int)Math.Round(group.Count * trainFraction);
                    for (int i = 0; i < group.Count; i++)
                    {
                        if (i < numTrain) train.Add(group[i]);
                        else test.Add(group[i]);
                    }
                }
                // mix the classes so the rows are not ordered low, mid, high
                Shuffle(train, rnd);
                Shuffle(test, rnd);
            }
            else
            {
                List<Patient> copy = new List<Patient>(data);
                Shuffle(copy, rnd);
                int numTrain = (int)Math.Round(copy.Count * trainFraction);
                for (int i = 0; i < copy.Count; i++)
                {
                    if (i < numTrain) train.Add(copy[i]);
                    else test.Add(copy[i]);
                }
            }
        }

        /// <summary>
        /// How many test rows are exact copies of a training row.
        /// Should be 0 after duplicate removal; above 0 means the test set leaks training data.
        /// </summary>
        public static int CountLeakedRows(List<Patient> train, List<Patient> test)
        {
            Dictionary<string, bool> trainKeys = new Dictionary<string, bool>();
            foreach (Patient p in train)
            {
                string key = p.RowKey();
                if (!trainKeys.ContainsKey(key)) trainKeys.Add(key, true);
            }
            int leaked = 0;
            foreach (Patient p in test)
                if (trainKeys.ContainsKey(p.RowKey())) leaked++;
            return leaked;
        }

        /// <summary>Fisher-Yates shuffle.</summary>
        public static void Shuffle<T>(IList<T> list, Random rnd)
        {
            for (int i = 0; i < list.Count; i++)
            {
                int r = rnd.Next(i, list.Count);
                T tmp = list[r];
                list[r] = list[i];
                list[i] = tmp;
            }
        }
    }

    /// <summary>
    /// Min-max scaling to [0, 1]:  x' = (x - min) / (max - min)
    /// Min and max are learned from the TRAINING set only and then reused for the test set
    /// and for new patients (Classify one patient). Test values can fall slightly outside
    /// [0, 1]; that is expected and correct.
    /// </summary>
    public class MinMaxScaler
    {
        public double[] Min;
        public double[] Max;

        public void Fit(List<Patient> trainData)
        {
            Min = new double[MaternalData.NumFeatures];
            Max = new double[MaternalData.NumFeatures];
            for (int f = 0; f < MaternalData.NumFeatures; f++)
            {
                Min[f] = double.MaxValue;
                Max[f] = double.MinValue;
            }
            foreach (Patient p in trainData)
            {
                for (int f = 0; f < MaternalData.NumFeatures; f++)
                {
                    if (p.Features[f] < Min[f]) Min[f] = p.Features[f];
                    if (p.Features[f] > Max[f]) Max[f] = p.Features[f];
                }
            }
        }

        /// <summary>Scales one patient's 6 raw values.</summary>
        public double[] Transform(double[] raw)
        {
            double[] result = new double[raw.Length];
            for (int f = 0; f < raw.Length; f++)
            {
                double range = Max[f] - Min[f];
                result[f] = (range == 0.0) ? 0.0 : (raw[f] - Min[f]) / range;
            }
            return result;
        }

        /// <summary>Scales a whole list; returns new Patient objects (the input is unchanged).</summary>
        public List<Patient> Transform(List<Patient> data)
        {
            List<Patient> result = new List<Patient>();
            foreach (Patient p in data)
                result.Add(new Patient(Transform(p.Features), p.Label, p.RowNumber));
            return result;
        }
    }
}
