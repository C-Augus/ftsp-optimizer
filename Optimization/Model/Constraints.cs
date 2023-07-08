using Gurobi;
using Optimizer.Entities;

namespace Optimizer.Model {
    public static class GurobiConstraints {
        public static void SetGurobiConstraints(ref GRBModel model, ref TSPInstance instance, int n, ref GRBVar[,] x, ref GRBVar[] y)
        {
            // Constraint 1: There must be exactly one arc leaving the depot
            GRBLinExpr expr = new();
            for (int j = 0; j < n; j++)
            {
                expr.AddTerm(1.0, x[0, j]);
            }
            model.AddConstr(expr, GRB.EQUAL, 1.0, "arc_leaves_depot");
            model.Update();
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

            // Constraint 3: together with c.1, guarantee there is an arc entering the depot
            for (int i = 0; i < n; i++)
            {
                expr.Clear();
                for (int j = 0; j < n; j++)
                {
                    expr.AddTerm(1.0, x[i, j]);
                    expr.AddTerm(-1.0, x[j, i]);
                }
                model.AddConstr(expr, GRB.EQUAL, y[i], "arc_entering_depot_" + i+1);
            }

            // Constraint 4: guarantees the number of required visits per family
            int startingPoint = 0;
            for (int l = 0; l < instance.ArrayOfVisits.Length; l++)
            {
                expr.Clear();
                for (int i = startingPoint; i < startingPoint + instance.ArrayOfVisits[l]; i++)
                {
                    expr.AddTerm(1.0, y[i]);
                }
                model.AddConstr(expr, GRB.EQUAL, instance.ArrayOfVisits[l], "visits_on_family_" + l+1);
                startingPoint += instance.ArrayOfVisits[l];
            }

            // Constraint 5: MTZ elimination of subtours
            GRBVar[] u = new GRBVar[n]; // u[i] represents the position of city i in the tour

            // Add the variables u[i] to the model
            for (int i = 0; i < n; i++)
            {
                u[i] = model.AddVar(2.0, n, 0.0, GRB.INTEGER, "u_" + i);
            }

            // Add the MTZ subtour elimination constraints
            for (int i = 1; i < n; i++)
            {
                for (int j = 1; j < n; j++)
                {
                    if (i != j)
                    {
                        expr.Clear();
                        expr.AddTerm(1.0, u[i]);
                        expr.AddTerm(-1.0, u[j]);
                        expr.AddTerm(n, x[i, j]); // Assuming x[i, j] represents the decision variable for the edge between city i and j
                        model.AddConstr(expr, GRB.LESS_EQUAL, n - 1, "mtz_" + i + "_" + j);
                    }
                }
            }
        }
    }
}