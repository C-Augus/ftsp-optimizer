using CommonLib.Entities;
using CommonLib.Utils;

namespace SimulatedAnnealing
{
    public abstract class RouteCostCalculator
    {
        public static double CalculateRouteCost(Route route)
        {
            double totalDistance = 0.0;

            // Calculate distance between locations in the order they are visited
            for (int i = 0; i < route.VisitedNodes.Count - 1; i++)
            {
                Node from = route.VisitedNodes[i];
                Node to = route.VisitedNodes[i + 1];
                totalDistance += DistanceCalculator.DistanceBetween(from, to);
            }

            return totalDistance;
        }
    }
}
