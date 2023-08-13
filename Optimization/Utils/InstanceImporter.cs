using Optimizer.Entities;
using static Optimizer.Utils.InstanceHelper;

namespace Optimizer.Utils
{
    public class InstanceImporter
    {
        public static TSPInstance ReadInstanceFromFile(string filePath)
        {
            string[] lines = File.ReadAllLines(filePath);

            TSPInstance instance = new(DateTime.Now);

            Process(ref instance, ref lines);

            return instance;
        }
    }
}