using Optimizer.Model;
using Optimizer.Utils;

namespace Optimizer.Main
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] tspFiles = Directory.GetFiles(DirectorySelect.SelectDirectory(), "*.tsp");

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