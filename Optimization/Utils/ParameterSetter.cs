//using ExtensionMethods;
using Optimizer.Entities;

namespace Optimizer.Utils
{
    public abstract class ParameterSetter
    {
        public static void SetParameters(ref TSPInstance instance)
        {
            instance.Model.Parameters.TimeLimit = 6000.00;

            instance.Model.Parameters.LogFile = instance.LogDirectoryPath + instance.Name + ".log";
        }
    }
}
