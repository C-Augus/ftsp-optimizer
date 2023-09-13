namespace CommonLib.Entities
{
    public class Family
    {
        public int Id { get; set; }
        public int NumberOfNodes { get; set; }
        public int NumberOfVisits { get; set; }
        public List<Node> Nodes { get; set; }

        public Family(int id, int numberOfNodes, int numberOfVisits)
        {
            Id = id;
            NumberOfNodes = numberOfNodes;
            NumberOfVisits = numberOfVisits;
            Nodes = new List<Node>();
        }
    }
}