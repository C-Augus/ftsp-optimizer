using Optimizer.Entities;
using Optimizer.Utils;
using Optimizer.Model;
using CommonLib.Delegates;
using GC = Optimizer.Model.GurobiConstraints;

namespace Optimizer.Delegates
{
    public delegate void GurobiModelingMethods(ref GurobiTSPInstance instance);
    
    // Class responsible for holding the delegates of method chaining.
    public abstract class CustomGurobiDelegates : CustomCommonDelegates
    {
        // Processes all methods listed on Model/GurobiConstraints in the following order.
        public static GurobiModelingMethods SetGurobiConstraints =
            new GurobiModelingMethods(GC.SetConstraint1)
            + GC.SetConstraint2
            + GC.SetConstraint3
            + GC.SetConstraint4
            + GC.SetConstraint5
            ;

        // Processes the methods listed in the following order.
        public static GurobiModelingMethods ProcessInstance =
            new GurobiModelingMethods(GurobiVariables.SetGurobiVariables)
            + GurobiObjective.SetGurobiObjective
            + SetGurobiConstraints
            + ParameterSetter.SetParameters
            ;
    }
}
