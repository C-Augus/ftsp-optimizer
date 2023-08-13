using Gurobi;
using Optimizer.Entities;
using Optimizer.Utils;
using System.Diagnostics;

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

                // Create new Gurobi environment
                GRBEnv env = new(true);
                env.Set("LogFile", $"{instance.Name}.log");
                env.Start();

                // Create empty model
                GRBModel model = new(env);


                // Create x binary variable matrix
                GRBVar[,] x;
                GRBVar[] y;
                /*
                GurobiVariables.SetGurobiVariables(ref model, instance.NumberOfNodes, out x, out y);

                GurobiObjective.SetGurobiObjective(ref model, ref instance, ref x);

                GurobiConstraints.SetGurobiConstraints(ref model, ref instance, instance.NumberOfNodes, ref x, ref y);
                */
                model.Parameters.TimeLimit = 6000.00;

                model.Update();

                Stopwatch stopwatch = Stopwatch.StartNew();
                model.Optimize();
                stopwatch.Stop();

                Console.WriteLine("Obj: " + model.ObjVal);

                model.Dispose();
                env.Dispose();

                //return model;
            }
            catch (GRBException e)
            {
                Console.WriteLine("Error code: " + e.ErrorCode + ". " + e.Message);
            }
        }   
    }

    // public static class AnotherModel { }
}
