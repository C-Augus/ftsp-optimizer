//using Optimizer.Model;
using CommonLib.Utils;
using Optimizer.Entities;
using Gurobi;

namespace GurobiOptimizer
{
    class Program
    {
        static async Task Main(string[] args)
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
                    GurobiTSPInstance instance = (GurobiTSPInstance)InstanceImporter.ReadInstanceFromFile(tspFile);
                    instance.ProcessInstance(ref instance);
                }
                catch (GRBException e)
                {
                    Console.WriteLine("Error code: " + e.ErrorCode + ". " + e.Message);
                }
                //GurobiModel.ExecuteGurobiModel(tspFile);
            }
        }
    }
}