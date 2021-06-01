namespace _02._SimpleNeuralNetwork
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using ConsoleTableExt;

    public class NetworkModel
    {
        private readonly List<NeuralLayer> Layers;

        public NetworkModel()
        {
            this.Layers = new List<NeuralLayer>();
        }

        // Add Layer to the network
        public void AddLayer(NeuralLayer layer)
        {
            var dendriteCount = 1;

            if (this.Layers.Count > 0)
            {
                dendriteCount = this.Layers.Last().Neurons.Count;
            }

            foreach (var element in layer.Neurons)
            {
                for (var i = 0; i < dendriteCount; i++)
                {
                    element.Dendrites.Add(new Dendrite());
                }
            }

            this.Layers.Add(layer);
        }

        // Builds the network
        public void Build()
        {
            for (var index = 0; index < this.Layers.Count - 1; index++)
            {
                var layer = this.Layers[index];
                
                var nextLayer = this.Layers[index + 1];
                CreateNetwork(layer, nextLayer);
            }
        }

        // Loop through the training data. Input the data into the first layer
        // Forward the pulse across the layer and get the output. Measure the output against the expected result.
        // Optimize the weights and then iterate to see the new result.
        public void Train(NeuralData X, NeuralData Y, int iterations, double learningRate = 0.1)
        {
            var epoch = 1;
            //Loop till the number of iterations
            while (iterations >= epoch)
            {
                //Get the input layers
                var inputLayer = this.Layers[0];
                var outputs = new List<double>();

                //Loop through the record
                for (var i = 0; i < X.Data.Length; i++)
                {
                    //Set the input data into the first layer
                    for (var j = 0; j < X.Data[i].Length; j++)
                    {
                        inputLayer.Neurons[j].OutputPulse.Value = X.Data[i][j];
                    }

                    //Fire all the neurons and collect the output
                    this.ComputeOutput();
                    outputs.Add(this.Layers.Last().Neurons.First().OutputPulse.Value);
                }

                //Check the accuracy score against Y with the actual output
                double accuracySum = 0;
                var y_counter = 0;

                outputs.ForEach((x) => {
                    if (Math.Abs(x - Y.Data[y_counter].First()) < 0.0000001)
                    {
                        accuracySum++;
                    }

                    y_counter++;
                });

                //Optimize the synaptic weights
                this.OptimizeWeights(accuracySum / y_counter, learningRate);
                Console.WriteLine("Epoch: {0}, Accuracy: {1} %", epoch, (accuracySum / y_counter) * 100);
                epoch++;
            }
        }

        public void Print()
        {
            var dt = new DataTable();
            dt.Columns.Add("Name");
            dt.Columns.Add("Neurons");
            dt.Columns.Add("Weight");

            foreach (var element in this.Layers)
            {
                var row = dt.NewRow();
                row[0] = element.Name;
                row[1] = element.Neurons.Count;
                row[2] = element.Weight;

                dt.Rows.Add(row);
            }

            var builder = ConsoleTableBuilder.From(dt);
            builder.ExportAndWrite();
        }

        private void ComputeOutput()
        {
            var first = true;
            foreach (var layer in this.Layers)
            {
                //Skip first layer as it is input
                if (first)
                {
                    first = false;
                    continue;
                }

                layer.Forward();
            }
        }

        private void OptimizeWeights(double accuracy, double learningRate = 0.1D)
        {
            //Skip if the accuracy reached 100%
            if (Math.Abs(accuracy - 1) < 0.0000001)
            {
                return;
            }

            if (accuracy > 1)
            {
                learningRate = -learningRate;
            }

            //Update the weights for all the layers
            foreach (var layer in this.Layers)
            {
                layer.Optimize(learningRate, 1);
            }
        }

        private static void CreateNetwork(NeuralLayer connectingFrom, NeuralLayer connectingTo)
        {
            foreach (var from in connectingFrom.Neurons)
            {
                from.Dendrites = new List<Dendrite>
                {
                    new Dendrite()
                };
            }

            foreach (var to in connectingTo.Neurons)
            {
                to.Dendrites = new List<Dendrite>();

                foreach (var from in connectingFrom.Neurons)
                {
                    to.Dendrites.Add(new Dendrite()
                    {
                        InputPulse = from.OutputPulse, 
                        SynapticWeight = connectingTo.Weight
                    });
                }
            }
        }
    }
}
