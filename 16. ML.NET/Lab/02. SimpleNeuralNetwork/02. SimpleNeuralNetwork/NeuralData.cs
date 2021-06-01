namespace _02._SimpleNeuralNetwork
{
    public class NeuralData
    {
        private int counter = 0;

        public NeuralData(int rows)
        {
            this.Data = new double[rows][];
        }

        public double[][] Data { get; set; }

        public void Add(params double[] rec)
        {
            this.Data[this.counter] = rec;
            this.counter++;
        }
    }
}
