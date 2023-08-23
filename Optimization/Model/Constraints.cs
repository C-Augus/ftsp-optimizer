using Gurobi;
using Optimizer.Entities;

namespace Optimizer.Model {
    public static class GurobiConstraints 
    {
        public static void Constraint1(ref TSPInstance instance)
        {
            GRBLinExpr expr = new();

            foreach (Node nodeJ in instance.Nodes)
                if (nodeJ.Id != 0)
                    expr.AddTerm(1.0, instance.X[0, nodeJ.Id]);

            instance.Model.AddConstr(expr, GRB.EQUAL, 1.0, "arc_leaves_depot");

            instance.Model.Update();
        }

        public static void Constraint2(ref TSPInstance instance)
        {
            GRBLinExpr expr = new();

            foreach (Node nodeI in instance.Nodes)
            {
                if (nodeI.Id != 0)
                {
                    expr.Clear();

                    foreach (Node nodeJ in instance.Nodes)
                        if (nodeI.Id != nodeJ.Id)
                            expr.AddTerm(1.0, instance.X[nodeI.Id, nodeJ.Id]);

                    instance.Model.AddConstr(expr, GRB.EQUAL, instance.Y[nodeI.Id], "arc_leaves_node_" + nodeI.Id);
                }
            }

            instance.Model.Update();
        }

        public static void Constraint3(ref TSPInstance instance)
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

            instance.Model.Update();
        }

        public static void Constraint5(ref TSPInstance instance)
        {
            GRBLinExpr expr = new();

            foreach (Node nodeI in instance.Nodes)
            {
                foreach (Node nodeJ in instance.Nodes)
                {
                    if ((nodeI.Id != nodeJ.Id) && (nodeI.Id != 0) && (nodeJ.Id != 0))
                    {
                        instance.Model.AddConstr(instance.U[nodeI.Id] + ((instance.Nodes.Count + 1) * instance.X[nodeI.Id, nodeJ.Id]) - (instance.Nodes.Count + 1) + 1, GRB.LESS_EQUAL, instance.U[nodeJ.Id], $"subtour_elimination_{nodeI.Id}_{nodeJ.Id}");
                    }
                }
            }

            instance.Model.Update();
        }
    }
}