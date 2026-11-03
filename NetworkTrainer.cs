using System;
using System.Collections.Generic;
using Backprop;

namespace MaternalRiskPredictor
{
    /// <summary>
    /// Wraps the professor's NeuralNet engine (BackNeural.cs) for the maternal risk problem.
    /// The engine is NOT modified: this class only calls setInputs, setDesiredOutput,
    /// learn, run and getOuputData - the same methods used in the XOR exercise.
    ///
    /// Network shape: 6 inputs (vital signs) -> H hidden neurons -> 3 outputs (one per risk level).
    /// Targets are one-hot:  Low = 1 0 0,  Mid = 0 1 0,  High = 0 0 1.
    /// The predicted risk level is the output neuron with the highest value.
    /// </summary>
    public class NetworkTrainer
    {
        private NeuralNet net;
        private Random rnd;        // only shuffles the training order each epoch
        private int hiddenNodes;
        private int epochsTrained;

        public NetworkTrainer(int hiddenNodes, int shuffleSeed)
        {
            this.hiddenNodes = hiddenNodes;
            this.net = new NeuralNet(MaternalData.NumFeatures, hiddenNodes, MaternalData.NumClasses);
            this.rnd = new Random(shuffleSeed);
            this.epochsTrained = 0;
        }

        public int HiddenNodes { get { return hiddenNodes; } }
        public int EpochsTrained { get { return epochsTrained; } }

        /// <summary>
        /// One epoch = one learn() call for every training patient, in a new random order.
        /// learn() runs the forward pass, measures the error and updates every weight (backpropagation).
        /// </summary>
        public void TrainOneEpoch(List<Patient> train)
        {
            int[] order = new int[train.Count];
            for (int i = 0; i < order.Length; i++)
                order[i] = i;
            Preprocessing.Shuffle(order, rnd);

            for (int i = 0; i < order.Length; i++)
            {
                Patient p = train[order[i]];
                SetInputs(p.Features);
                for (int k = 0; k < MaternalData.NumClasses; k++)
                    net.setDesiredOutput(k, (k == p.Label) ? 1.0 : 0.0);
                net.learn();
            }
            epochsTrained++;
        }

        /// <summary>Forward pass only (no learning). Returns the 3 output values, each 0..1.</summary>
        public double[] Predict(double[] scaledFeatures)
        {
            SetInputs(scaledFeatures);
            net.run();
            double[] outputs = new double[MaternalData.NumClasses];
            for (int k = 0; k < outputs.Length; k++)
                outputs[k] = net.getOuputData(k);
            return outputs;
        }

        /// <summary>0 = low, 1 = mid, 2 = high: the output neuron with the highest value.</summary>
        public int PredictClass(double[] scaledFeatures)
        {
            return ArgMax(Predict(scaledFeatures));
        }

        /// <summary>
        /// Mean squared error: average of (target - output)^2 over every patient
        /// and every output neuron. This is the "loss" shown in the training log.
        /// </summary>
        public double ComputeMse(List<Patient> data)
        {
            if (data.Count == 0) return 0.0;
            double sumSq = 0.0;
            foreach (Patient p in data)
            {
                double[] y = Predict(p.Features);
                for (int k = 0; k < y.Length; k++)
                {
                    double target = (k == p.Label) ? 1.0 : 0.0;
                    double err = target - y[k];
                    sumSq += err * err;
                }
            }
            return sumSq / (data.Count * MaternalData.NumClasses);
        }

        /// <summary>Fraction of patients whose predicted risk level is correct (0..1).</summary>
        public double ComputeAccuracy(List<Patient> data)
        {
            if (data.Count == 0) return 0.0;
            int correct = 0;
            foreach (Patient p in data)
                if (PredictClass(p.Features) == p.Label) correct++;
            return (double)correct / data.Count;
        }

        private void SetInputs(double[] features)
        {
            for (int i = 0; i < features.Length; i++)
                net.setInputs(i, features[i]);
        }

        public static int ArgMax(double[] values)
        {
            int best = 0;
            for (int i = 1; i < values.Length; i++)
                if (values[i] > values[best]) best = i;
            return best;
        }
    }
}
