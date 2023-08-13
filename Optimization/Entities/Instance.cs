namespace Optimizer.Entities {
    public class TSPInstance {
        public string? Name { get; set; }
        public string? Comment { get; set; }
        public string? Type { get; set; }
        public int? Dimension { get; set; }
        public string? EdgeWeightType { get; set; }
        public int? NumberOfFamilies { get; set; }
        public int? NumberOfVisits { get; set; }
        public List<Family>? Families { get; set; } = new List<Family>();
        public List<Node>? Nodes { get; set; } = new List<Node>();
        public string? Solution { get; set; }
        public DateTime? Date { get; set; }
        public float? ElapsedTime { get; set; }
        public float? UpperBound { get; set; }
        public float? LowerBound { get; set; }
        public float? Gap { get; set; }
        public string? NodesOrder { get; set; }

        public TSPInstance(DateTime dateTime)
        {
            Date = dateTime;
        }
    }
}