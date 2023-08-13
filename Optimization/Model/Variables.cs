using Gurobi;
using Optimizer.Entities;

namespace Optimizer.Model {
    public static class GurobiVariables
    {
        // Creates an empty binary matrix of variables (x) and an empty binary array (y).
        // Then, it fills them with proper decision variables to send them back to the model's call.
        //public static void SetGurobiVariables(ref GRBModel model, int numberOfNodes, out GRBVar[,] x, out GRBVar[] y)
        //{
        //    x = new GRBVar[numberOfNodes, numberOfNodes];
        //    y = new GRBVar[numberOfNodes];

        //    for (int i = 0; i < numberOfNodes; i++)
        //    {
        //        y[i] = model.AddVar(0.0, GRB.INFINITY, 0.0, GRB.BINARY, $"y{i + 1}");

        //        for (int j = 0; j < numberOfNodes; j++)
        //            x[i, j] = model.AddVar(0.0, GRB.INFINITY, 0.0, GRB.BINARY, $"x{i + 1}{j + 1}");
        //    }
        //}

        public static void SetGurobiVariables(ref TSPInstance instance)
        {
            //instance.X = new GRBVar[instance.Nodes.Count, instance.Nodes.Count];
            //instance.Y = new GRBVar[instance.Nodes.Count];

            foreach (Node nodeI in instance.Nodes)
            {
                foreach (Node nodeJ in instance.Nodes)
                    instance.X[nodeI.Id, nodeJ.Id] = instance.Model.AddVar(0.0, GRB.INFINITY, 0.0, GRB.BINARY, $"x_{nodeI.Id}_{nodeJ.Id}");

                instance.Y[nodeI.Id] = instance.Model.AddVar(0.0, GRB.INFINITY, 0.0, GRB.BINARY, $"y_{nodeI.Id}");

                instance.U[nodeI.Id] = instance.Model.AddVar(0.0, instance.Nodes.Count, 0.0, GRB.INTEGER, $"u_{nodeI.Id}");
            }
        }
    }
}