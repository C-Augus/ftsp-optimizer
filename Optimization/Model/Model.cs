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
                GRBVar[,] x = new GRBVar[instance.Nodes.Count, instance.Nodes.Count]; ;
                GRBVar[] y = new GRBVar[instance.Nodes.Count];
                GRBVar[] u = new GRBVar[instance.Nodes.Count];

                instance.Model = model;
                instance.X = x;
                instance.Y = y;
                instance.U = u;

                GurobiVariables.SetGurobiVariables(ref instance);

                GurobiObjective.SetGurobiObjective(ref instance);

                GurobiConstraints.SetGurobiConstraints(ref instance);
                
                model.Parameters.TimeLimit = 6000.00;

                model.Update();

                Stopwatch stopwatch = Stopwatch.StartNew();
                model.Optimize();
                stopwatch.Stop();

                foreach (Node nodeI in instance.Nodes)
                    foreach (Node nodeJ in instance.Nodes)
                        Console.WriteLine(x[nodeI.Id, nodeJ.Id]);

                //GRBVar.
                
                Console.WriteLine("Obj: " + model.ObjVal);
                Console.WriteLine("Stopwatch: " + stopwatch.Elapsed);

                model.Dispose();
                env.Dispose();
            }
            catch (GRBException e)
            {
                Console.WriteLine("Error code: " + e.ErrorCode + ". " + e.Message);
            }
        }   
    }

    // public static class AnotherModel { }
}
