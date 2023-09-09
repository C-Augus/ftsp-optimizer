using CommonLib.Entities;
using Gurobi;
using CommonLib.Utils;
using Optimizer.Delegates;

namespace Optimizer.Entities {
    public class GurobiTSPInstance : TSPInstance {
        public GRBEnv Env { get; set; }
        public GRBModel Model { get; set; }
        public GRBVar[,] X { get; set; }
        public GRBVar[] Y { get; set; }
        public GRBVar[] U { get; set; }

        public GurobiTSPInstance(string filePath) : base(filePath)
        {
            //Families = new List<Family>();
            //Nodes = new List<Node>();
            //Date = DateTime.Now;

            //LogDirectoryPath = StringHelper.FindLogDirectoryPath(filePath);

            Solution = "GRB";

            Env = new GRBEnv();
            Env.Start();

            Model = new GRBModel(Env);
        }

        public void ProcessInstance(ref GurobiTSPInstance instance)
        {
            CustomGurobiDelegates.ProcessInstance(ref instance);

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