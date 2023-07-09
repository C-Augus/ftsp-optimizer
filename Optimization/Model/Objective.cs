using Gurobi;
using Optimizer.Entities;

namespace Optimizer.Model {
    public static class GurobiObjective  {
        public static void SetGurobiObjective(ref GRBModel model, ref TSPInstance instance, ref GRBVar[,] decisionVarible)
        {
            // Setting the objective function
            model.Update();

            GRBLinExpr objective = new();

            for (int i = 0; i < instance.NumberOfNodes; i++)
            {
                for (int j = 0; j < instance.NumberOfNodes; j++)
                {
                    objective.AddTerm(instance.DistancesMatrix[i, j], decisionVarible[i, j]);
                }
            }

            model.SetObjective(objective, GRB.MINIMIZE);
        }
    }
}