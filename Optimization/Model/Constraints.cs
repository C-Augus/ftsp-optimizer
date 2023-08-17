using Gurobi;
using Optimizer.Delegates;
using Optimizer.Entities;

namespace Optimizer.Model {
    public static class GurobiConstraints 
    {
        // Delegate of method chaining, responsible for processing all methods listed on GurobiConstraints in the following order.
        public static ConstraintMethods SetGurobiConstraints =
            new ConstraintMethods(Constraint1)
            + Constraint2
            + Constraint3
            + Constraint4
            + Constraint5
            ;

        public static void Constraint1(ref TSPInstance instance)
        {
            GRBLinExpr expr = new();

            foreach (Node nodeJ in instance.Nodes)
                expr.AddTerm(1.0, instance.X[0, nodeJ.Id]);

            instance.Model.AddConstr(expr, GRB.EQUAL, 1.0, "arc_leaves_depot");
        }

        public static void Constraint2(ref TSPInstance instance)
        {
            GRBLinExpr expr = new();

            foreach (Node nodeI in instance.Nodes)
            {
                expr.Clear();

                foreach (Node nodeJ in instance.Nodes)
                    if (nodeI.Id != nodeJ.Id)
                        expr.AddTerm(1.0, instance.X[nodeI.Id, nodeJ.Id]);

                instance.Model.AddConstr(expr, GRB.EQUAL, 1.0, "arc_leaves_node_" + nodeI.Id);
            }
        }

        public static void Constraint3(ref TSPInstance instance)
        {
            GRBLinExpr expr = new();

            foreach (Node nodeI in instance.Nodes)
            {
                expr.Clear();

                foreach (Node nodeJ in instance.Nodes)
                {
                    expr.AddTerm(1.0, instance.X[nodeI.Id, nodeJ.Id]);
                    expr.AddTerm(-1.0, instance.X[nodeJ.Id, nodeI.Id]);
                }

                instance.Model.AddConstr(expr, GRB.EQUAL, 0.0, "arc_entering_depot_" + nodeI.Id);
            }
        }

        public static void Constraint4(ref TSPInstance instance)
        {
            GRBLinExpr expr = new();

            foreach (Family family in instance.Families)
            {
                expr.Clear();

                foreach (Node node in family.Nodes)
                    expr.AddTerm(1.0, instance.Y[node.Id]);

                instance.Model.AddConstr(expr, GRB.EQUAL, family.NumberOfVisits, "visits_on_family_" + family.Id);
            }
        }

        public static void Constraint5(ref TSPInstance instance)
        {
            GRBLinExpr expr = new();

            foreach (Node nodeI in instance.Nodes)
                foreach (Node nodeJ in instance.Nodes)
                    if (nodeI.Id != nodeJ.Id)
                        instance.Model.AddConstr(
                            instance.U[nodeI.Id] - instance.U[nodeJ.Id] + (instance.Nodes.Count * instance.X[nodeI.Id, nodeJ.Id]),
                            GRB.LESS_EQUAL,
                            instance.Nodes.Count - 1,
                            $"subtour_elimination_{nodeI.Id}_{nodeJ.Id}");
        }
    }
}