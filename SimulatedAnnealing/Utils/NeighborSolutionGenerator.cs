using CommonLib.Entities;

namespace SimulatedAnnealing.Utils
{
    abstract class NeighborSolutionGenerator
    {
        public static Route SwapNodes(Route route)
        {
            int index1 = new Random().Next(route.VisitedNodes.Count);
            int index2 = new Random().Next(route.VisitedNodes.Count);

            while (index1 == index2)
                index2 = new Random().Next(route.VisitedNodes.Count);

            Node temp = route.VisitedNodes[index1];
            route.VisitedNodes[index1] = route.VisitedNodes[index2];
            route.VisitedNodes[index2] = temp;

            return route;
        }

        public static Route InsertionDeletion(Route route)
        {
            int indexOnRoute = new Random().Next(route.VisitedNodes.Count);

            Node nodeToBeDeleted = route.VisitedNodes[indexOnRoute];

            Family family = nodeToBeDeleted.Family;

            int indexOnFamily = new Random().Next(family.Nodes.Count);

            Node nodeToBeInserted = family.Nodes[indexOnFamily];

            while (nodeToBeDeleted == nodeToBeInserted)
                nodeToBeInserted = family.Nodes[new Random().Next(family.Nodes.Count)];

            route.VisitedNodes[indexOnRoute] = nodeToBeInserted;

            return route;
        }
    }
}
