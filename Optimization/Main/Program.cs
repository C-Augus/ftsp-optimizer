using Optimizer.Model;
using Optimizer.Utils;
// using ExtensionMethods;

namespace Optimizer.Main
{
    class Program
    {
        static async Task Main(string[] args)
        {
            string[] tspFiles = Directory.GetFiles(DirectoryImporter.SelectDirectory(), "*.tsp");

            //string[] tspFiles = Directory.GetFiles(DirectoryExtensions.ImportDirectory(), "*.tsp");

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