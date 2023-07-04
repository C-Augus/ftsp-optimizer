namespace Optimization.Entities {
    public class TSPInstance {
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