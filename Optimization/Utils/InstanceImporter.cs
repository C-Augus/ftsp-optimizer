using Optimizer.Entities;
using static Optimizer.Utils.InstanceHelper;

namespace Optimizer.Utils
{
    public class InstanceImporter
    {
        public static TSPInstance ReadInstanceFromFile(string filePath)
        {
            string[] lines = StringHelper.ReadAllLines(filePath);

            TSPInstance instance = new(DateTime.Now);

            ProcessInstance(ref instance, ref lines);

            return instance;
        }
    }
}