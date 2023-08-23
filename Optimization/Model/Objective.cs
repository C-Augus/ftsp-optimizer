using Gurobi;
using Optimizer.Entities;
using DC = Optimizer.Utils.DistanceCalculator;

namespace Optimizer.Model
{
    public static class GurobiObjective
    {
        // Defines the model's objective by adding iteratively: Distance between two nodes * Decision variable.
        // Once done, sets the model objective.
        public static void SetGurobiObjective(ref TSPInstance instance)
        {
            GRBLinExpr objective = new();

            foreach (Node nodeI in instance.Nodes)
                foreach (Node nodeJ in instance.Nodes)
                    objective.AddTerm(DC.DistanceBetween(nodeI, nodeJ), instance.X[nodeI.Id, nodeJ.Id]);

            instance.Model.SetObjective(objective, GRB.MINIMIZE);

            instance.Model.Update();
        }
    }
}