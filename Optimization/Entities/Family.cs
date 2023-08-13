namespace Optimizer.Entities
{
    public class Family
    {
        public int Id { get; set; }
        public int NumberOfNodes { get; set; }
        public int NumberOfVisits { get; set; }
        public List<Node> Nodes { get; set; } = new List<Node>();

        public Family() { }

        public Family(int id, int numberOfNodes, int numberOfVisits)
        {
            Id = id;
            NumberOfNodes = numberOfNodes;
            NumberOfVisits = numberOfVisits;
        }
    }
}
