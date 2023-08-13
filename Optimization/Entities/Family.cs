namespace Optimizer.Entities
{
    public class Family
    {
        public int Id { get; set; }
        public int NumberOfVisits { get; set; }
        public List<Node> Nodes { get; set; } = new List<Node>();

        public Family(int id, int numberOfVisits)
        {
            Id = id;
            NumberOfVisits = numberOfVisits;
        }
    }
}