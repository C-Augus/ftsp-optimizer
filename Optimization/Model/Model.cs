using Gurobi;
using Optimizer.Entities;
using Optimizer.Utils;
using System.Reflection;

namespace Optimizer.Model
{
    public static class GurobiModel
    {
        public static /*GRBModel*/ void ExecuteGurobiModel()
        {
            try
            {
                // Create new Gurobi environment
                GRBEnv env = new(true);
                env.Set("LogFile", "ftsp.log");
                env.Start();

                // Create empty model
                GRBModel model = new(env);

                TSPInstance instance = Instance.ReadInstanceFromFile();

                // Create x binary variable matrix
                GRBVar[,] x;
                GRBVar[] y;

                SetGurobiVariables(ref model, instance.NumberOfNodes, out x, out y);

                SetGurobiObjective(ref model, ref instance, ref x);

                model.Optimize();

                Console.WriteLine("Obj: " + model.ObjVal);

                model.Dispose();
                env.Dispose();

                //return model;
            }
            catch (GRBException e)
            {
                Console.WriteLine("Error code: " + e.ErrorCode + ". " + e.Message);
            }
        }

        public static void SetGurobiVariables(ref GRBModel model, int n, out GRBVar[,] x, out GRBVar[] y)
        {
            // Create binary matrix x and binary array y
            x = new GRBVar[n, n];
            y = new GRBVar[n];

            // Feeding matrix x and array y
            for (int i = 0; i < n; i++)
            {
                y[i] = model.AddVar(0.0, GRB.INFINITY, 0.0, GRB.BINARY, $"y{i + 1}");

                for (int j = 0; j < n; j++)
                {
                    x[i, j] = model.AddVar(0.0, GRB.INFINITY, 0.0, GRB.BINARY, $"x{i + 1}{j + 1}");
                }
            }

            // return x;
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

        public static void SetGurobiConstraints(ref GRBModel model, ref TSPInstance instance, int n, ref GRBVar[,] x, ref GRBVar[] y)
        {
            // Constraint 1: There must be exactly one arc leaving the depot
            GRBLinExpr expr = new();
            for (int j = 0; j < n; j++)
            {
                expr.AddTerm(1.0, x[0, j]);
            }
            model.AddConstr(expr, GRB.EQUAL, 1.0, "arc_leaves_depot");
            expr.Clear();

            // Constraint 2: if a node i is visited, there must be an arc leaving i
            for (int i = 0; i < n; i++)
            {
                expr.Clear();
                for (int j = 0; j < n; j++)
                {
                    expr.AddTerm(1.0, x[i, j]);
                }
                model.AddConstr(expr, GRB.EQUAL, y[i], "arc_leaves_node_" + i+1);
            }
            expr.Clear();



        }
    }

    // public static class AnotherModel { }
}
