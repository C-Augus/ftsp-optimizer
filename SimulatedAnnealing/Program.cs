using CommonLib.Entities;
using CommonLib.Utils;
using SimulatedAnnealing;
using SimulatedAnnealing.Entities;

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
                    SimulatedAnnealingInstance SAInstance = new(tspFile, baseInstance);
                    SimulatedAnnealingLoop.ProcessSA(ref SAInstance);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
        }
    }
}