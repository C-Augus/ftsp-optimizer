using CommonLib.Entities;
using SimulatedAnnealing.Entities;

namespace SimulatedAnnealing
{
    public abstract class InitialRouteGenerator
    {
        public static Route InitializeSolution(SimulatedAnnealingInstance instance)
        {
            List<Node> initialTour = new();

            initialTour.Add(instance.Nodes[0]);

            foreach (var family in instance.Families)
                for (int i = 0; i < family.NumberOfVisits; i++)
                    initialTour.Add(family.Nodes[i]);

            initialTour.Add(instance.Nodes[0]);

            return new Route { VisitedNodes = initialTour };
        }

    }
}
