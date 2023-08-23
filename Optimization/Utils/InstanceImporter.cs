using Optimizer.Entities;
using Optimizer.Delegates;

namespace Optimizer.Utils
{
    public class InstanceImporter
    {
        public static TSPInstance ReadInstanceFromFile(string filePath)
        {
            string[] lines = StringHelper.ReadAllLines(filePath);

            TSPInstance instance = new(DateTime.Now);

            CustomGurobiDelegates.ImportInstance(ref instance, ref lines);

            return instance;
        }
    }
}