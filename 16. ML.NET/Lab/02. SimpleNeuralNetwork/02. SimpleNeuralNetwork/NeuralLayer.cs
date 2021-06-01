namespace _02._SimpleNeuralNetwork
{
    using System;
    using System.Collections.Generic;

    // We stack a bunch of neuron in the form of layer
    public class NeuralLayer
    {
        private readonly List<Neuron> neurons;

        public NeuralLayer(int count, double initialWeight, string name = "")
        {
            this.neurons = new List<Neuron>();

            for (var i = 0; i < count; i++)
            {
                this.neurons.Add(new Neuron());
            }

            this.Weight = initialWeight;

            this.Name = name;
        }

        public IReadOnlyList<Neuron> Neurons => this.neurons.AsReadOnly();

        public string Name { get; }

        public double Weight { get; set; }

        public void Optimize(double learningRate, double delta)
        {
            this.Weight += learningRate * delta;

            foreach (var neuron in this.Neurons)
            {
                neuron.UpdateWeights(this.Weight);
            }
        }

        // Take cares of firing all the neuron in the layer and forward the input pulse to the next layer.
        public void Forward()
        {
            foreach (var neuron in this.Neurons)
            {
                neuron.Fire();
            }
        }

        public void Log()
        {
            Console.WriteLine("{0}, Weight: {1}", this.Name, this.Weight);
        }
    }
}
