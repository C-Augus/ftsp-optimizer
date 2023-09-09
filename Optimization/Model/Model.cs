using CommonLib.Utils;
using Gurobi;
using Optimizer.Entities;
using Optimizer.Delegates;

namespace Optimizer.Model
{
    public static class GurobiModel
    {
        public static void ExecuteGurobiModel(string filePath)
        {
            try
            {
                GurobiTSPInstance instance = (GurobiTSPInstance)InstanceImporter.ReadInstanceFromFile(filePath);

                // Creates new Gurobi environment and starts it.
                //GRBEnv env = new(true);
                //env.Start();

                //// Creates an empty model
                //instance.Model = new GRBModel(env);

                //CustomGurobiDelegates.ProcessInstance(ref instance);

                //instance.Model.Optimize();

                //InstanceHelper.PostProcessData(ref instance);
                //ResultPrinter.ExportInstanceData(ref instance);

                //instance.PostProcessData();
                //instance.ExportData();

                //instance.Model.Dispose();
                //instance.Env.Dispose();
            }
            catch (GRBException e)
            {
                Console.WriteLine("Error code: " + e.ErrorCode + ". " + e.Message);
            }
        }   
    }
}