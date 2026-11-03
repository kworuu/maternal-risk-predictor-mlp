using System;
using System.Collections.Generic;

namespace MaternalRiskPredictor
{
    // Order used in Form1.Data.cs:
    //   1. RemoveDuplicates on the whole file, before splitting, so the same patient can't
    //      end up in both the training and the test set (data leakage)
    //   2. RemoveImpossible
    //   3. Split (stratified 80/20, fixed seed)
    //   4. MinMaxScaler.Fit on the training set only, then Transform both sets
    // Every method returns new lists; the raw data is never modified.
    public static class Preprocessing
    {
       
        public static List<Patient> RemoveDuplicates(List<Patient> data, out int removed)
        {
            bool[] isDuplicate = MaternalData.FindDuplicates(data);
            List<Patient> result = new List<Patient>();
            for (int i = 0; i < data.Count; i++)
                if (!isDuplicate[i]) result.Add(data[i]);
            removed = data.Count - result.Count;
            return result;
        }

        public static List<Patient> RemoveImpossible(List<Patient> data, out int removed)
        {
            List<Patient> result = new List<Patient>();
            foreach (Patient p in data)
                if (!MaternalData.IsImpossible(p)) result.Add(p);
            removed = data.Count - result.Count;
            return result;
        }

        // Stratified: each risk level is shuffled and split on its own, so the training and
        // test sets keep the same low / mid / high proportions as the full dataset.

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

        // Test rows that are exact copies of a training row. Should be 0 once duplicates
        // are removed; anything above 0 means the test score is partly memorisation.
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

        // Fisher-Yates shuffle
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

    // Scales each feature to [0, 1] with x' = (x - min) / (max - min).
    // Min and max come from the training set only and are reused for the test set and for
    // single-patient predictions, so test values can land slightly outside [0, 1]. That's expected.
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

        public List<Patient> Transform(List<Patient> data)
        {
            List<Patient> result = new List<Patient>();
            foreach (Patient p in data)
                result.Add(new Patient(Transform(p.Features), p.Label, p.RowNumber));
            return result;
        }
    }
}
