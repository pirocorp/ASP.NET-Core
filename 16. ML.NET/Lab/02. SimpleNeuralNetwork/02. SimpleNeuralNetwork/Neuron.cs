namespace _02._SimpleNeuralNetwork
{
    using System.Collections.Generic;

    // The workflow of a neuron is it receives the input values,
    // do the weighted sum of all the input signals from all dendrites
    // and pass them to the activation function which in this case is
    // Step activation function.
    // The output of the Activation is assigned to OutputPulse which will
    // travel through the Axon of the neuron.
    public class Neuron
    {
        public Neuron()
        {
            this.Dendrites = new List<Dendrite>();
            this.OutputPulse = new Pulse();
        }

        public List<Dendrite> Dendrites { get; set; }

        public Pulse OutputPulse { get; set; }

        public void Fire()
        {
            this.OutputPulse.Value = this.Sum();

            this.OutputPulse.Value = Activation(this.OutputPulse.Value);
        }

        public void UpdateWeights(double new_weights)
        {
            foreach (var terminal in this.Dendrites)
            {
                terminal.SynapticWeight = new_weights;
            }
        }

        private double Sum()
        {
            var computeValue = 0.0D;

            foreach (var d in this.Dendrites)
            {
                computeValue += d.InputPulse.Value * d.SynapticWeight;
            }

            return computeValue;
        }

        private static double Activation(double input)
        {
            double threshold = 1;
            return input <= threshold ? 0 : threshold;
        }
    }
}