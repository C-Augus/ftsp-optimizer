using Gurobi;
using Optimizer.Entities;

namespace Optimizer.Delegates
{
    public delegate void ProcessInstanceMethods(ref TSPInstance instance, ref string[] lines);

    public delegate void ConstraintMethods(ref TSPInstance instance);
}
