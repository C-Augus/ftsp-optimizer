using CommonLib.Entities;

namespace SimulatedAnnealing.Entities
{
    public class SimulatedAnnealingInstance : TSPInstance
    {
        public double InitialTemperature { get; set; }
        public double CoolingRate { get; set; }
        public int MaxIterations { get; set; }
        public double PenaltyFactor { get; set; }
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

            double InitialTemperature = 1000.0;
            double CoolingRate = 0.995;
            int MaxIterations = 10000;
            double PenaltyFactor = 1000.0;

            Solution = "SA";
        }

        public double CalculateAcceptanceProbability(Route currentRoute, Route newRoute)
        {
            return Math.Pow(Math.E, (RouteCostCalculator.CalculateRouteCost(currentRoute) - RouteCostCalculator.CalculateRouteCost(newRoute)) / InitialTemperature);
        }

        public override void PostProcessData()
        {
            base.PostProcessData();
        }
    }
}
