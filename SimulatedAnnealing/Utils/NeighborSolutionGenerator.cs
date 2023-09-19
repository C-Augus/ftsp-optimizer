using CommonLib.Entities;
using System.Collections.Generic;

namespace SimulatedAnnealing.Utils
{
    abstract class NeighborSolutionGenerator
    {
        public static Route SwapNodes(Route currentRoute)
        {
            Route newRoute = (Route)currentRoute.Clone();

            int index1 = new Random().Next(1, newRoute.VisitedNodes.Count - 1);
            int index2 = new Random().Next(1, newRoute.VisitedNodes.Count - 1);

            while (index1 == index2)
                index2 = new Random().Next(1, newRoute.VisitedNodes.Count - 1);

            Node temp = newRoute.VisitedNodes[index1];
            newRoute.VisitedNodes[index1] = newRoute.VisitedNodes[index2];
            newRoute.VisitedNodes[index2] = temp;

            return newRoute;
        }

        public static Route InsertionDeletion(Route currentRoute)
        {
            Route newRoute = (Route)currentRoute.Clone();

            int indexOnRoute = new Random().Next(1, newRoute.VisitedNodes.Count - 1);

            Node nodeToBeDeleted = newRoute.VisitedNodes[indexOnRoute];

            Family family = nodeToBeDeleted.Family;

            int indexOnFamily = new Random().Next(family.Nodes.Count);

            Node nodeToBeInserted = family.Nodes[indexOnFamily];

            while (newRoute.VisitedNodes.Contains(nodeToBeInserted))
                nodeToBeInserted = family.Nodes[new Random().Next(family.Nodes.Count)];

            newRoute.VisitedNodes[indexOnRoute] = nodeToBeInserted;

            return newRoute;
        }
    }
}
