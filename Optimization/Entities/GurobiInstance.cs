using CommonLib.Entities;
using Gurobi;
using Optimizer.Delegates;

namespace Optimizer.Entities {
    public class GurobiTSPInstance : TSPInstance {
        public GRBEnv Env { get; set; }
        public GRBModel Model { get; set; }
        public GRBVar[,] X { get; set; }
        public GRBVar[] Y { get; set; }
        public GRBVar[] U { get; set; }

        public GurobiTSPInstance(string filePath, TSPInstance baseInstance) : base(filePath)
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

            Solution = "GRB";

            Env = new GRBEnv();
            Env.Start();

            Model = new GRBModel(Env);
        }

        public void ProcessInstance(ref GurobiTSPInstance gurobiInstance)
        {
            CustomGurobiDelegates.ProcessInstance(ref gurobiInstance);

            Model.Optimize();
        }

        public override void PostProcessData()
        {
            ElapsedTime = Model.Runtime;
            ObjVal = Model.ObjVal;
            LowerBound = Model.MinBound;
            UpperBound = Model.MaxBound;
            Gap = Model.MIPGap;

            ExportData();
        }

        public void Dispose()
        {
            Model.Dispose();
            Env.Dispose();
        }
    }
}