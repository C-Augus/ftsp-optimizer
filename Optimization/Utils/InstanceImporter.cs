using Optimizer.Delegates;
using Optimizer.Entities;

namespace Optimizer.Utils
{
    public class InstanceImporter
    {
        public static TSPInstance ReadInstanceFromFile(string filePath)
        {
            string[] lines = StringHelper.ReadAllLines(filePath);

            TSPInstance instance = new(filePath);

            CustomGurobiDelegates.ImportInstance(ref instance, ref lines);

            return instance;
        }
    }
}