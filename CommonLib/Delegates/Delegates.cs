using CommonLib.Entities;
using IH = CommonLib.Utils.InstanceHelper;

namespace CommonLib.Delegates
{
    public delegate void InstanceHelperMethods(ref TSPInstance instance, ref string[] lines);
    
    // Class resposible for holding the delegates of method chaining.
    public abstract class CustomDelegates
    {
        // Processes all methods listed on Utils/InstanceHelper in the following order.
        public static InstanceHelperMethods ImportInstance =
            new InstanceHelperMethods(IH.FindFileHeaderValues)
            + IH.FindFamilies
            + IH.FindNodes
            + IH.LinkNodesToFamilies
            + IH.FindFamiliesAndVisits
            ;
    }
}
