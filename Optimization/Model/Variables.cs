using Gurobi;
using Optimizer;

namespace Optimizer.Model {
    public static class GurobiVariables{
        public static void SetGurobiVariables(ref GRBModel model, int n, out GRBVar[,] x, out GRBVar[] y)
        {
            // Create binary matrix x and binary array y
            x = new GRBVar[n, n];
            y = new GRBVar[n];

            // Feeding matrix x and array y
            for (int i = 0; i < n; i++)
            {
                y[i] = model.AddVar(0.0, GRB.INFINITY, 0.0, GRB.BINARY, $"y{i + 1}");
                z[i] = model.AddVar()

                for (int j = 0; j < n; j++)
                {
                    x[i, j] = model.AddVar(0.0, GRB.INFINITY, 0.0, GRB.BINARY, $"x{i + 1}{j + 1}");
                }
            }
        }
    }
}