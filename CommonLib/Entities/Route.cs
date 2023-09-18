using System.IO;

namespace CommonLib.Entities
{
    public class Route : ICloneable
    {
        public List<Node> VisitedNodes { get; set; }

        public object Clone()
        {
            List<Node> newVisitedNodes = new List<Node>();

            foreach (Node node in VisitedNodes)
                newVisitedNodes.Add(node);

            return new Route { VisitedNodes = newVisitedNodes };
        }
    }
}
