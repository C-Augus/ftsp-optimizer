using Gurobi;
using Optimizer.Entities;

namespace Optimizer.Delegates
{
    public delegate void ProcessInstanceMethods(ref TSPInstance instance, ref string[] lines);

    //public delegate void ConstraintMethods(ref GRBModel model, ref TSPInstance instance, int numberOfNodes, ref GRBVar[,] x, ref GRBVar[] y);

    public delegate void ConstraintMethods(ref TSPInstance instance);
}
