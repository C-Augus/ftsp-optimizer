using Optimizer.Entities;
using Optimizer.Utils;
using Optimizer.Model;
using CommonLib.Delegates;
using GC = Optimizer.Model.GurobiConstraints;

namespace Optimizer.Delegates
{
    public delegate void GurobiModelingMethods(ref GurobiTSPInstance instance);
    
    // Class resposible for holding the delegates of method chaining.
    public abstract class CustomGurobiDelegates : CustomDelegates
    {
        // Processes all methods listed on Model/GurobiConstraints in the following order.
        public static GurobiModelingMethods SetGurobiConstraints =
            new GurobiModelingMethods(GC.Constraint1)
            + GC.Constraint2
            + GC.Constraint3
            + GC.Constraint4
            + GC.Constraint5
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
