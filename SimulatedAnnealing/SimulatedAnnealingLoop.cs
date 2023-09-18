using CommonLib.Entities;
using SimulatedAnnealing.Entities;
using SimulatedAnnealing.Utils;

namespace SimulatedAnnealing
{
    public abstract class SimulatedAnnealingLoop
    {
        public static void ProcessSA(ref SimulatedAnnealingInstance instance)
        {
            // Initialize the current solution
            Route currentSolution = InitialRouteGenerator.InitializeSolution(instance);

            for (int iteration = 0; iteration < instance.MaxIterations; iteration++)
            {
                // Generate a neighboring solution
                Route newSolution = NeighborSolutionGenerator.SwapNodes(currentSolution);
                newSolution = NeighborSolutionGenerator.InsertionDeletion(newSolution);

                //Console.Write("Current solution: ");
                //foreach (Node node in currentSolution.VisitedNodes)
                //    Console.Write(node.Id + " ");
                //Console.Write(" | New solution: ");
                //foreach (Node node in  newSolution.VisitedNodes)
                //    Console.Write(node.Id + " ");

                //Console.WriteLine("\n");

                // Calculate acceptance probability and decide whether to accept the new solution
                double acceptanceProbability = instance.CalculateAcceptanceProbability(currentSolution, newSolution);

                Console.WriteLine(acceptanceProbability);

                if (new Random().NextDouble() <= acceptanceProbability)
                    currentSolution = newSolution;

                // Update temperature
                instance.InitialTemperature *= instance.CoolingRate;
            }

            Console.WriteLine("The best solution found is: " + RouteCostCalculator.CalculateRouteCost(currentSolution));
        }
    }
}
