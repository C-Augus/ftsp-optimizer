using Gurobi;
using Optimizer.Entities;
using CommonLib.Entities;
using DC = CommonLib.Utils.DistanceCalculator;

namespace Optimizer.Model
{
    public static class GurobiObjective
    {
        // Defines the model's objective by adding iteratively: Distance between two nodes * Decision variable.
        // Once done, sets the model objective.
        public static void SetGurobiObjective(ref GurobiTSPInstance instance)
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