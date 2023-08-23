using Optimizer.Entities;
using Optimizer.Utils;
using Optimizer.Model;
using GC = Optimizer.Model.GurobiConstraints;
using IH = Optimizer.Utils.InstanceHelper;

namespace Optimizer.Delegates
{
    public delegate void InstanceHelperMethods(ref TSPInstance instance, ref string[] lines);

    public delegate void GurobiModelingMethods(ref TSPInstance instance);
    
    // Class resposible for holding the delegates of method chaining.
    public abstract class CustomGurobiDelegates
    {
        // Processes all methods listed on Utils/InstanceHelper in the following order.
        public static InstanceHelperMethods ImportInstance =
            new InstanceHelperMethods(IH.FindFileHeaderValues)
            + IH.FindFamilies
            + IH.FindNodes
            + IH.LinkNodesToFamilies
            + IH.FindFamiliesAndVisits
            ;

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
