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

            File.WriteAllText(instance.LogDirectoryPath + "SANumbers" + ".log", string.Empty);

            for (int iteration = 0; iteration < instance.MaxIterations; iteration++)
            {
                // Generate a neighboring solution
                Route newSolution = NeighborSolutionGenerator.SwapNodes(currentSolution);
                newSolution = NeighborSolutionGenerator.InsertionDeletion(newSolution);

                if (new Random().NextDouble() <= instance.CalculateAcceptanceProbability(currentSolution, newSolution))
                    currentSolution = newSolution;

                // Update temperature
                instance.InitialTemperature *= instance.CoolingRate;
            }

            Console.WriteLine("The best solution found is: " + RouteCostCalculator.CalculateRouteCost(currentSolution));
        }
    }
}
