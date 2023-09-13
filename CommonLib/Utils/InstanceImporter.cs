using CommonLib.Delegates;
using CommonLib.Entities;

namespace CommonLib.Utils
{
    public class InstanceImporter
    {
        public static TSPInstance ReadInstanceFromFile(string filePath)
        {
            string[] lines = StringHelper.ReadAllLines(filePath);

            TSPInstance instance = new(filePath);

            CustomCommonDelegates.ImportInstance(ref instance, ref lines);

            return instance;
        }
    }
}