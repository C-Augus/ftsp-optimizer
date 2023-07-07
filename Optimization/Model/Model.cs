using Gurobi;
using Optimizer.Entities;
using Optimizer.Utils;

namespace Optimizer.Model
{
    public static class GurobiModel
    {
        public static GRBModel GetGurobiModel()
        {
            GRBEnv env = new GRBEnv(true);
            env.Set("LogFile", "ftsp.log");
            env.Start();

            // Create empty model
            GRBModel model = new GRBModel(env);

            TSPInstance instance = Instance.ReadInstFromFile();

            // Create x binary variable matrix
            GRBVar[,] x = new GRBVar[instance.NumberOfNodes, instance.NumberOfNodes];

            // Feeding matrix x
            for (int i = 0; i < instance.NumberOfNodes; i++)
            {
                for (int j = 0; j < instance.NumberOfNodes; j++)
                {
                    x[i, j] = model.AddVar(0.0, GRB.INFINITY, 0.0, GRB.BINARY, $"x{i + 1}{j + 1}");
                }

            }

            SetGurobiObjective(ref model, ref instance, ref x);

            return model;
        }

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

            model.SetObjective(objective, GRB.MAXIMIZE);
        }

        public static void SetGurobiConstraints()
        {

        }
    }

    // public static class AnotherModel { }
}
