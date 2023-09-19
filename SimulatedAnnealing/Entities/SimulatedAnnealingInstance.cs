using CommonLib.Entities;

namespace SimulatedAnnealing.Entities
{
    public class SimulatedAnnealingInstance : TSPInstance
    {
        public double InitialTemperature { get; set; }
        public double CoolingRate { get; set; }
        public int MaxIterations { get; set; }
        public SimulatedAnnealingInstance(string filePath, TSPInstance baseInstance) : base(filePath)
        {
            Name = baseInstance.Name;
            Type = baseInstance.Type;
            Dimension = baseInstance.Dimension;
            Date = baseInstance.Date;
            LogDirectoryPath = baseInstance.LogDirectoryPath;
            NumberOfFamilies = baseInstance.NumberOfFamilies;
            NumberOfVisits = baseInstance.NumberOfVisits;
            Families = baseInstance.Families;
            Nodes = baseInstance.Nodes;

            InitialTemperature = 1000.0;
            CoolingRate = 0.995;
            MaxIterations = 10000;

            Solution = "SA";
        }

        public double CalculateAcceptanceProbability(Route currentRoute, Route newSolution)
        {
            return Math.Pow(Math.E, -(RouteCostCalculator.CalculateRouteCost(newSolution) - RouteCostCalculator.CalculateRouteCost(currentRoute)) / InitialTemperature);
        }

        public override void PostProcessData()
        {
            base.PostProcessData();
        }
    }
}
