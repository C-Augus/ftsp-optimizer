using Gurobi;
using Optimizer.Entities;
using CommonLib.Entities;

namespace Optimizer.Model {
    public static class GurobiVariables
    {
        // Creates an empty binary matrix of variables (X), an empty binary array (Y) and a empty continuous array (U).
        // Then, it fills them with their respective decision variables.
        public static void SetGurobiVariables(ref GurobiTSPInstance instance)
        {
            instance.X = new GRBVar[instance.Nodes.Count, instance.Nodes.Count]; ;
            instance.Y = new GRBVar[instance.Nodes.Count];
            instance.U = new GRBVar[instance.Nodes.Count];

            foreach (Node nodeI in instance.Nodes)
            {
                foreach (Node nodeJ in instance.Nodes)
                    instance.X[nodeI.Id, nodeJ.Id] = instance.Model.AddVar(0.0, GRB.INFINITY, 0.0, GRB.BINARY, $"x_{nodeI.Id}_{nodeJ.Id}");

                instance.Y[nodeI.Id] = instance.Model.AddVar(0.0, GRB.INFINITY, 0.0, GRB.BINARY, $"y_{nodeI.Id}");

                instance.U[nodeI.Id] = instance.Model.AddVar(0.0, instance.Nodes.Count, 0.0, GRB.CONTINUOUS, $"u_{nodeI.Id}");

            }

            instance.Model.Update();
        }
    }
}