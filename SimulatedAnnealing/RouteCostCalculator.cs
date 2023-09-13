using CommonLib.Entities;
using CommonLib.Utils;
using SimulatedAnnealing.Entities;

namespace SimulatedAnnealing
{
    public abstract class RouteCostCalculator
    {
        public static double CalculateRouteCost(Route route, SimulatedAnnealingInstance instance)
        {

            double totalDistance = 0.0;

            // Calculate distance between locations in the order they are visited
            for (int i = 0; i < route.VisitedNodes.Count - 1; i++)
            {
                Node from = route.VisitedNodes[i];
                Node to = route.VisitedNodes[i + 1];
                totalDistance += DistanceCalculator.DistanceBetween(from, to);
            }

            //// Add distance from the last visited location back to the depot
            //totalDistance += DistanceCalculator.DistanceBetween(route.VisitedNodes.Last(), depot);

            // Calculate the penalty for not meeting minimum visit requirements
            double penalty = 0.0;
            foreach (var family in instance.Families)
            {
                int actualVisits = route.VisitedNodes.Count(node => family.Nodes.Contains(node));
                int visitsShortage = Math.Max(0, family.NumberOfVisits - actualVisits);
                penalty += visitsShortage * instance.PenaltyFactor;
            }

            return totalDistance + penalty;
        }
    }
}
