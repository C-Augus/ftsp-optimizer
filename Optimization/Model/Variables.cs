using Gurobi;
using Optimizer.Entities;

namespace Optimizer.Model {
    public static class GurobiVariables
    {
        // Creates an empty binary matrix of variables (x) and an empty binary array (y). <- deprecated
        // Then, it fills them with proper decision variables to send them back to the model's call.
        public static void SetGurobiVariables(ref TSPInstance instance)
        {
            foreach (Node nodeI in instance.Nodes)
            {
                foreach (Node nodeJ in instance.Nodes)
                    instance.X[nodeI.Id, nodeJ.Id] = instance.Model.AddVar(0.0, GRB.INFINITY, 0.0, GRB.BINARY, $"x_{nodeI.Id}_{nodeJ.Id}");

                instance.Y[nodeI.Id] = instance.Model.AddVar(0.0, GRB.INFINITY, 0.0, GRB.BINARY, $"y_{nodeI.Id}");

                instance.U[nodeI.Id] = instance.Model.AddVar(0.0, instance.Nodes.Count, 0.0, GRB.CONTINUOUS, $"u_{nodeI.Id}");

                instance.Model.Update();
            }
        }
    }
}