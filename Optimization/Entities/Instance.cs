using Gurobi;
using Optimizer.Utils;

namespace Optimizer.Entities {
    public class TSPInstance {
        public string? Name { get; set; }
        public string? Type { get; set; }
        public int? Dimension { get; set; }
        public DateTime Date { get; set; }
        public string? Solution { get; set; }
        public string LogDirectoryPath { get; set; }
        public double ElapsedTime { get; set; }
        public double ObjVal { get; set; }
        public double UpperBound { get; set; }
        public double LowerBound { get; set; }
        public double Gap { get; set; }
        public int? NumberOfFamilies { get; set; }
        public int? NumberOfVisits { get; set; }
        public List<Family> Families { get; set; }
        public List<Node> Nodes { get; set; }
        public string? NodesOrder { get; set; }
        public GRBModel Model { get; set; }
        public GRBVar[,] X { get; set; }
        public GRBVar[] Y { get; set; }
        public GRBVar[] U { get; set; }

        public TSPInstance(string filePath)
        {
            LogDirectoryPath = StringHelper.FindLogDirectoryPath(filePath);
            Families = new List<Family>();
            Nodes = new List<Node>();
            Date = DateTime.Now;
        }
    }
}