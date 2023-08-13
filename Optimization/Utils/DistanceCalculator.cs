using Optimizer.Entities;

namespace Optimizer.Utils
{
    public abstract class DistanceCalculator
    {
        // DistanceBetween calculates the distance between nodeI and nodeJ based 
        // on their coordinates (x, y) through Euclidian Distance formula.
        // = sqrt( (nodeI.x - nodeJ.x)^2 + (nodeI.y - nodeJ.y)^2 )
        public static double DistanceBetween(Node nodeI, Node nodeJ)
        {
            return Math.Sqrt(Math.Pow((nodeI.X - nodeJ.X), 2) + Math.Pow((nodeI.Y - nodeJ.Y), 2));
        }
    }
}
