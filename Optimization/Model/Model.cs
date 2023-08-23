using Gurobi;
using CGD = Optimizer.Delegates.CustomGurobiDelegates;
using Optimizer.Entities;
using Optimizer.Utils;

namespace Optimizer.Model
{
    public static class GurobiModel
    {
        public static void ExecuteGurobiModel(string filePath)
        {
            try
            {
                TSPInstance instance = InstanceImporter.ReadInstanceFromFile(filePath);
                instance.Solution = "Gurobi Mathematical Model Solver";

                // Creates new Gurobi environment and starts it.
                GRBEnv env = new(true);
                env.Start();

                // Creates an empty model
                instance.Model = new GRBModel(env);

                CGD.ProcessInstance(ref instance);

                instance.Model.Optimize();

                instance.Model.Dispose();
                env.Dispose();
            }
            catch (GRBException e)
            {
                Console.WriteLine("Error code: " + e.ErrorCode + ". " + e.Message);
            }
        }   
    }
}