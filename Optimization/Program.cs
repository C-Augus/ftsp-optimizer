using CommonLib.Utils;
using CommonLib.Entities;
using Optimizer.Entities;
using Gurobi;

namespace GurobiOptimizer
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] tspFiles = Directory.GetFiles(DirectoryImporter.SelectDirectory(), "*.tsp");

            if (tspFiles.Length == 0)
            {
                Console.WriteLine("No TSP files found in the selected folder.");
                return;
            }

            foreach (string tspFile in tspFiles)
            {
                Console.WriteLine($"Executing: {tspFile}");

                try
                {
                    TSPInstance baseInstance = InstanceImporter.ReadInstanceFromFile(tspFile);
                    GurobiTSPInstance gurobiInstance = new(tspFile, baseInstance);
                    gurobiInstance.ProcessInstance(ref gurobiInstance);
                }
                catch (GRBException e)
                {
                    Console.WriteLine("Error code: " + e.ErrorCode + ". " + e.Message);
                }
            }
        }
    }
}