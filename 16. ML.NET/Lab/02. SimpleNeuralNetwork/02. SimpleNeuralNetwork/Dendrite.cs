namespace _02._SimpleNeuralNetwork
{
    // A simple projection of a neuron that receives the pulse input and feeds into the neuron itself for computation. 
    public class Dendrite
    {
        public Dendrite()
        {
            this.InputPulse = new Pulse();
        }

        // The input signal.
        public Pulse InputPulse { get; set; }

        // The connection strength between the dendrite and synapsis of two neurons.
        public double SynapticWeight { get; set; }

        // Just a bool used during training to adjust the SynapticWeight value.
        public bool Learnable { get; set; } = true;
    }
}