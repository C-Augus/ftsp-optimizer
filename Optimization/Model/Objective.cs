using Gurobi;
using Optimizer.Entities;
using Optimizer.Utils;

namespace Optimizer.Model
{
    public static class GurobiObjective
    {
        // Defines the model's objective by adding iteratively: Distance between two nodes * Decision variable.
        // Once done, sets the model objective.
        //public static void SetGurobiObjective(ref GRBModel model, ref TSPInstance instance, ref GRBVar[,] decisionVarible)
        //{
        //    model.Update();

        //    GRBLinExpr objective = new();

        //    for (int i = 0; i < instance.Nodes.Count; i++)
        //        for (int j = 0; j < instance.Nodes.Count; j++)
        //            objective.AddTerm(DistanceCalculator.DistanceBetween(instance.Nodes[i], instance.Nodes[j]), decisionVarible[i, j]);

        //    model.SetObjective(objective, GRB.MINIMIZE);
        //}

        public static void SetGurobiObjective(ref TSPInstance instance)
        {
            instance.Model.Update();

            GRBLinExpr objective = new();

            foreach (Node nodeI in instance.Nodes)
                foreach (Node nodeJ in instance.Nodes)
                    objective.AddTerm(DistanceCalculator.DistanceBetween(nodeI, nodeJ), instance.X[nodeI.Id, nodeJ.Id]);

            instance.Model.SetObjective(objective, GRB.MINIMIZE);
        }
    }
}