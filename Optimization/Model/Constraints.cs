using Gurobi;
using Optimizer.Entities;
using CommonLib.Entities;

namespace Optimizer.Model
{
    public static class GurobiConstraints
    {
        // Constraint 1: There must be exactly one arc leaving the depot.
        public static void Constraint1(ref GurobiTSPInstance instance)
        {
            GRBLinExpr expr = new();

            foreach (Node nodeJ in instance.Nodes)
                expr.AddTerm(1.0, instance.X[0, nodeJ.Id]);

            expr.Remove(0); // The set here doesn't include subset {0}

            instance.Model.AddConstr(expr, GRB.EQUAL, 1.0, "arc_leaves_depot");

            instance.Model.Update();
        }

        // Constraint 2: If a node I is visited, there must be an arc leaving node I.
        public static void Constraint2(ref GurobiTSPInstance instance)
        {
            GRBLinExpr expr = new();

            foreach (Node nodeI in instance.Nodes)
            {
                expr.Clear();

                foreach (Node nodeJ in instance.Nodes)
                    if (nodeI.Id != nodeJ.Id)
                        expr.AddTerm(1.0, instance.X[nodeI.Id, nodeJ.Id]);

                instance.Model.AddConstr(expr, GRB.EQUAL, instance.Y[nodeI.Id], "arc_leaves_node_" + nodeI.Id);
            }

            expr.Remove(0); // The set here doesn't include subset {0}

            instance.Model.Update();
        }

        // Constraint 3: Together with Constraint1, guarantee there is an arc entering the depot.
        public static void Constraint3(ref GurobiTSPInstance instance)
        {
            GRBLinExpr expr = new();

            foreach (Node nodeI in instance.Nodes)
            {
                expr.Clear();

                foreach (Node nodeJ in instance.Nodes)
                {
                    expr.AddTerm(1.0, instance.X[nodeJ.Id, nodeI.Id]);
                    expr.AddTerm(-1.0, instance.X[nodeI.Id, nodeJ.Id]);
                }

                instance.Model.AddConstr(expr, GRB.EQUAL, 0.0, "arc_entering_depot_" + nodeI.Id);
            }

            instance.Model.Update();
        }

        // Constraint 4: guarantees the number of required visits per family.
        public static void Constraint4(ref GurobiTSPInstance instance)
        {
            GRBLinExpr expr = new();

            foreach (Family family in instance.Families)
            {
                expr.Clear();

                foreach (Node node in family.Nodes)
                    expr.AddTerm(1.0, instance.Y[node.Id]);

                instance.Model.AddConstr(expr, GRB.EQUAL, family.NumberOfVisits, "visits_on_family_" + family.Id);
            }

            instance.Model.Update();
        }

        // Constraint 5: MTZ elimination of subtours.
        public static void Constraint5(ref GurobiTSPInstance instance)
        {
            for (int nodeI = 1; nodeI < instance.Nodes.Count; nodeI++)
                for (int nodeJ = 1; nodeJ < instance.Nodes.Count; nodeJ++)
                    if (nodeI != nodeJ)
                        instance.Model.AddConstr(
                            instance.U[nodeI] + ((instance.Nodes.Count + 1) * instance.X[nodeI, nodeJ]) - (instance.Nodes.Count + 1) + 1,
                            GRB.LESS_EQUAL,
                            instance.U[nodeJ],
                            $"subtour_elimination_{nodeI}_{nodeJ}"
                        );

            instance.Model.Update();
        }
    }
}