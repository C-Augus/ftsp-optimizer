using Optimizer.Model;
using Optimizer.Utils;
using ExtensionMethods;

namespace Optimizer.Main
{
    class Program
    {
        static void Main(string[] args)
        {
            //string[] tspFiles = Directory.GetFiles(DirectoryImporter.SelectDirectory(), "*.tsp");

            string[] tspFiles = Directory.GetFiles(StringExtensions.ImportDirectory(), "*.tsp");

            if (tspFiles.Length == 0)
            {
                Console.WriteLine("No TSP files found in the selected folder.");
                return;
            }

            foreach (string tspFile in tspFiles)
            {
                Console.WriteLine($"Executing: {tspFile}");
                GurobiModel.ExecuteGurobiModel(tspFile);
            }
        }
    }
}