using CommonLib.Entities;
using SimulatedAnnealing.Entities;

namespace SimulatedAnnealing.Utils
{
    //PrintOut.Print(instance, acceptanceProbability, currentSolution, newSolution, iteration, new Random().NextDouble(), acceptanceProbability); 
    abstract class PrintOut
    {
        public static void Print(SimulatedAnnealingInstance instance, double value, Route currentSolution, Route newSolution, int iteration, double random, double acceptanceProbability)
        {
            using (StreamWriter writer = new(instance.LogDirectoryPath + "SANumbers" + ".log", true))
            {
                writer.WriteLine("Iteration = " + iteration);

                writer.Write("\nCurrent solution: ");
                foreach (Node node in currentSolution.VisitedNodes)
                    writer.Write(node.Id + " ");

                writer.Write(" | New solution: ");
                foreach (Node node in newSolution.VisitedNodes)
                    writer.Write(node.Id + " ");

                writer.WriteLine("\n\nRandom = " + random);

                if (random > acceptanceProbability)
                {
                    writer.WriteLine("\n NOT ACCEPTED");
                }
                else
                {
                    writer.WriteLine("\n PASSED");
                }

                writer.WriteLine("");
                writer.WriteLine(value + " = " + Math.E + "**(" + RouteCostCalculator.CalculateRouteCost(currentSolution) + " - " + RouteCostCalculator.CalculateRouteCost(newSolution) + ") / " + InitialTemperature);

                writer.WriteLine("---------------------");
            }
        }
    }
}
