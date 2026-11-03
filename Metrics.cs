using System;
using System.Collections.Generic;

namespace MaternalRiskPredictor
{
   
    // 3x3 confusion matrix plus the scores computed from it (L4).
    // Counts[actual, predicted]: rows = the real class, columns = the network's answer.
    // The diagonal (actual == predicted) holds the correct answers.

    public class ConfusionMatrix
    {
        public readonly int[,] Counts;
        public readonly int NumClasses;
        public int Total { get; private set; }

        public ConfusionMatrix(int numClasses)
        {
            NumClasses = numClasses;
            Counts = new int[numClasses, numClasses];
        }

        // Runs the network on every row of data and tallies actual vs predicted
        public static ConfusionMatrix Build(NetworkTrainer trainer, List<Patient> data)
        {
            ConfusionMatrix cm = new ConfusionMatrix(MaternalData.NumClasses);
            foreach (Patient p in data)
                cm.Add(p.Label, trainer.PredictClass(p.Features));
            return cm;
        }

        public void Add(int actual, int predicted)
        {
            Counts[actual, predicted]++;
            Total++;
        }

        // How many rows really belong to class k (the row total)
        public int Support(int k)
        {
            int sum = 0;
            for (int j = 0; j < NumClasses; j++) sum += Counts[k, j];
            return sum;
        }

        // How many times the network answered class k (the column total)
        public int PredictedCount(int k)
        {
            int sum = 0;
            for (int i = 0; i < NumClasses; i++) sum += Counts[i, k];
            return sum;
        }

        // Correct answers / all answers (sum of the diagonal / total)
        public double Accuracy()
        {
            if (Total == 0) return 0;
            int correct = 0;
            for (int k = 0; k < NumClasses; k++) correct += Counts[k, k];
            return (double)correct / Total;
        }

        public int Correct()
        {
            int correct = 0;
            for (int k = 0; k < NumClasses; k++) correct += Counts[k, k];
            return correct;
        }

        // Precision of k: of the rows the network CALLED class k, how many really were k?
        public double Precision(int k)
        {
            int predicted = PredictedCount(k);
            return predicted == 0 ? 0 : (double)Counts[k, k] / predicted;
        }

        // Recall of k: of the rows that REALLY are class k, how many did the network find?
        public double Recall(int k)
        {
            int actual = Support(k);
            return actual == 0 ? 0 : (double)Counts[k, k] / actual;
        }

        // F1 of k: harmonic mean of precision and recall (high only if both are high)
        public double F1(int k)
        {
            double p = Precision(k), r = Recall(k);
            return (p + r) == 0 ? 0 : 2 * p * r / (p + r);
        }

        // Macro average: plain mean over the 3 classes, so the small classes count as much as the big one
        public double MacroPrecision() { return Mean(Precision); }
        public double MacroRecall() { return Mean(Recall); }
        public double MacroF1() { return Mean(F1); }

        private double Mean(Func<int, double> score)
        {
            double sum = 0;
            for (int k = 0; k < NumClasses; k++) sum += score(k);
            return sum / NumClasses;
        }
    }

    public static class Metrics
    {

        // Majority-class baseline: a "model" that ignores the vitals and always answers the
        // class that is most common in the TRAINING set. The network must beat this to be useful.

        public static double MajorityBaseline(List<Patient> train, List<Patient> test, out int majorityClass)
        {
            int[] counts = MaternalData.CountPerClass(train);
            majorityClass = 0;
            for (int k = 1; k < counts.Length; k++)
                if (counts[k] > counts[majorityClass]) majorityClass = k;

            if (test.Count == 0) return 0;
            int hits = 0;
            foreach (Patient p in test)
                if (p.Label == majorityClass) hits++;
            return (double)hits / test.Count;
        }
    }
}
