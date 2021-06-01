namespace _02._SimpleNeuralNetwork
{
	using System;

    public class SimpleNeuralNet
    {
        public static void Main()
        {
            for (var i = 0; i < 1_000_000; i++)
            {
                CreateNeuralNetwork();
            }
        }

        private static void CreateNeuralNetwork()
        {
            var rnd = new Random();

            var model = new NetworkModel();
            model.AddLayer(new NeuralLayer(2, rnd.NextDouble(), "INPUT"));
            model.AddLayer(new NeuralLayer(1, rnd.NextDouble(), "OUTPUT"));

            model.Build();
            Console.WriteLine("----Before Training------------");
            model.Print();

            Console.WriteLine();

            var X = new NeuralData(4);
            X.Add(0, 0);
            X.Add(0, 1);
            X.Add(1, 0);
            X.Add(1, 1);

            var Y = new NeuralData(4);
            Y.Add(0);
            Y.Add(1);
            Y.Add(1);
            Y.Add(1);

            model.Train(X, Y, iterations: 11  , learningRate: 0.1);
            Console.WriteLine();
            Console.WriteLine("----After Training------------");
            model.Print();

            Console.WriteLine("Press any key to create next network");
            Console.ReadKey();
        }
    }
}
