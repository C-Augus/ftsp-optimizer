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
            + Constraint5;

        // Constraint 1: There must be exactly one arc leaving the depot.
        //public static void Constraint1(ref GRBModel model, ref TSPInstance instance, int numberOfNodes, ref GRBVar[,] x, ref GRBVar[] y)
        //{
        //    GRBLinExpr expr = new();
        //    for (int nodeJ = 0; nodeJ < numberOfNodes; nodeJ++)
        //        expr.AddTerm(1.0, x[0, nodeJ]);
        //    model.AddConstr(expr, GRB.EQUAL, 1.0, "arc_leaves_depot");
        //}

        public static void Constraint1(ref TSPInstance instance)
        {
            GRBLinExpr expr = new();

            foreach (Node nodeJ in instance.Nodes)
                expr.AddTerm(1.0, instance.X[0, nodeJ.Id]);

            instance.Model.AddConstr(expr, GRB.EQUAL, 1.0, "arc_leaves_depot");
        }

        // Constraint 2: If a nodeI is visited, there must be an arc leaving nodeI.
        //public static void Constraint2(ref GRBModel model, ref TSPInstance instance, int numberOfNodes, ref GRBVar[,] x, ref GRBVar[] y)
        //{
        //    GRBLinExpr expr = new();

        //    for (int nodeI = 0; nodeI < numberOfNodes; nodeI++)
        //    {
        //        expr.Clear();
        //        for (int nodeJ = 0; nodeJ < numberOfNodes; nodeJ++)
        //            if (nodeI != nodeJ)
        //                expr.AddTerm(1.0, x[nodeI, nodeJ]);

        //        model.AddConstr(expr, GRB.EQUAL, y[nodeI], "arc_leaves_node_" + nodeI + 1);
        //    }
        //}

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

        // Constraint 3: Together with Constraint1, guarantees that there is an arc entering the depot.
        //public static void Constraint3(ref GRBModel model, ref TSPInstance instance, int numberOfNodes, ref GRBVar[,] x, ref GRBVar[] y)
        //{
        //    GRBLinExpr expr = new();

        //    for (int nodeI = 0; nodeI < numberOfNodes; nodeI++)
        //    {
        //        expr.Clear();
        //        for (int nodeJ = 0; nodeJ < numberOfNodes; nodeJ++)
        //        {
        //            expr.AddTerm(1.0, x[nodeI, nodeJ]);
        //            expr.AddTerm(-1.0, x[nodeJ, nodeI]);
        //        }
        //        model.AddConstr(expr, GRB.EQUAL, 0.0, "arc_entering_depot_" + nodeI + 1);
        //    }
        //}

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

        //// Constraint 4: Guarantees that the number of required visits per family.
        //public static void Constraint4(ref GRBModel model, ref TSPInstance instance, int numberOfNodes, ref GRBVar[,] x, ref GRBVar[] y)
        //{
        //    GRBLinExpr expr = new();

        //    foreach(Family family in instance.Families)
        //    { 
        //        foreach(Node node in family.Nodes)
        //            expr.AddTerm(1.0, y[node.Id]);

        //        model.AddConstr(expr, GRB.EQUAL, family.NumberOfVisits, "visits_on_family_" + family.Id + 1);
        //    }
        //}

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

        // Constraint 5: MTZ elimination of subtours.
        //public static void Constraint5(ref GRBModel model, ref TSPInstance instance, int numberOfNodes, ref GRBVar[,] x, ref GRBVar[] y)
        //{
        //    GRBLinExpr expr = new();

        //    GRBVar[] u = new GRBVar[numberOfNodes]; // u[i] represents the position of city i in the tour

        //    //// Add the variables u[i] to the model
        //    //for (int i = 0; i < numberOfNodes; i++)
        //    //    u[i] = model.AddVar(2.0, numberOfNodes, 0.0, GRB.INTEGER, "u_" + i);

        //    // Add the MTZ subtour elimination constraints
        //    for (int i = 1; i < numberOfNodes; i++)
        //        for (int j = 1; j < numberOfNodes; j++)
        //            if (i != j)
        //            {
        //                expr.Clear();
        //                expr.AddTerm(1.0, u[i]);
        //                expr.AddTerm(-1.0, u[j]);
        //                expr.AddTerm(numberOfNodes, x[i, j]);
        //                model.AddConstr(expr, GRB.LESS_EQUAL, numberOfNodes - 1, "mtz_" + i + "_" + j);
        //            }
        //}

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