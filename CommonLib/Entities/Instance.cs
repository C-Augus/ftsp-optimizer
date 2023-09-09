using CommonLib.Utils;

namespace CommonLib.Entities {
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
        public Route FinalNodesOrder { get; set; }

        public TSPInstance(string filePath)
        {
            LogDirectoryPath = StringHelper.FindLogDirectoryPath(filePath);
            Families = new List<Family>();
            Nodes = new List<Node>();
            Date = DateTime.Now;
        }

        public virtual void  PostProcessData()
        {

        }

        public void ExportData()
        {
            string formattedLine = string.Format($"{Name,-25} | {Dimension,3} | {NumberOfFamilies,3} | {Solution,-3} |{Date,23} | {ElapsedTime,20} | {UpperBound} | {LowerBound} | {Gap}");

            using (StreamWriter writer = new(LogDirectoryPath + "generalLogs" + ".log", true))
            {
                writer.WriteLine(new string('-', formattedLine.Length));
                writer.WriteLine(formattedLine);
            }
        }
    }
}