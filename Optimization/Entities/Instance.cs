namespace Optimizer.Entities {
    public class TSPInstance {
        public string Name { get; set; }
        public string Comment { get; set; }
        public string Type { get; set; }
        public ushort Dimension { get; set; }
        public string EdgeWeightType { get; set; }
        public ushort NumberOfFamilies { get; set; }
        public ushort NumberOfVisits { get; set; }
        public List<Coordinate> Coordinates { get; set; }
        public string Solution { get; set; }
        public DateTime Date { get; set; }
        public float ElapsedTime { get; set; }
        public float UpperBound { get; set; }
        public float LowerBound { get; set; }
        public float Gap { get; set; }
        public string NodesOrder { get; set; }


        //Deprecated
        public int NumberOfNodes { get; }
        public int NumberOfFamilies { get; }
        public int NumberOfVisits { get; }
        public int[] ArrayOfFamilies { get; }
        public int[] ArrayOfVisits { get; }
        public int[,] DistancesMatrix { get; }

        public TSPInstance(int numberOfNodes, int numberOfFamilies, int numberOfVisits, int[] arrayOfFamilies, int[] arrayOfVisits, int size) {
            NumberOfNodes = numberOfNodes;
            NumberOfFamilies = numberOfFamilies;
            NumberOfVisits = numberOfVisits;
            ArrayOfFamilies = arrayOfFamilies;
            ArrayOfVisits = arrayOfVisits;
            DistancesMatrix = new int[size, size];
        }
    }
}