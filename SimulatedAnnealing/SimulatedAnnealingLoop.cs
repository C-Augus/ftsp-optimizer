using CommonLib.Entities;
using SimulatedAnnealing.Entities;
using SimulatedAnnealing.Utils;
using System;

namespace SimulatedAnnealing
{
    class SimulatedAnnealingLoop
    {
        public void ProcessSA(SimulatedAnnealingInstance instance)
        {
            // Initialize the current solution
            Route currentSolution = InitialRouteGenerator.InitializeSolution(instance);
            double currentCost = RouteCostCalculator.CalculateRouteCost(currentSolution);

            for (int iteration = 0; iteration < instance.MaxIterations; iteration++)
            {
                // Generate a neighboring solution
                Route newSolution = NeighborSolutionGenerator.SwapNodes(currentSolution);
                newSolution = NeighborSolutionGenerator.InsertionDeletion(newSolution);

               // Calculate the objective value for the new solution
               double newCost = RouteCostCalculator.CalculateRouteCost(newSolution);

                // Calculate acceptance probability and decide whether to accept the new solution
                double acceptanceProbability = instance.CalculateAcceptanceProbability(currentSolution, newSolution);

                if (new Random().NextDouble() < acceptanceProbability)
                {
                    currentSolution = newSolution;
                    currentCost = newCost;
                }

                // Update temperature
                instance.InitialTemperature *= instance.CoolingRate;
            }
        }
    }
}
