using System;
using Gurobi;
using Optimization.Entities;
using Optimization.Utils;

namespace Optimization{
  class Program {
    static void Main(string[] args) {
      TSPInstance instance = Instance.ReadInstFromFile();

      try 
      {
        // Create an empty environment, set options and start
        GRBEnv env = new GRBEnv(true);
        env.Set("LogFile", "ftsp.log");
        env.Start();

        // Create empty model
        GRBModel model = new GRBModel(env);

        GRBVar[,] x = new GRBVar[instance.NumberOfNodes, instance.NumberOfNodes];

        for (int i = 0; i < instance.NumberOfNodes; i++) {
          for (int j = 0; j < instance.NumberOfNodes; j++) {
            x[i, j] = model.AddVar(0.0, GRB.INFINITY, 0.0, GRB.BINARY, $"x{i+1}{j+1}");
          }
        }

        model.Update();

        GRBLinExpr objective = new();

        for (int i = 0; i < instance.NumberOfNodes; i++) {
          for (int j = 0; j < instance.NumberOfNodes; j++) {
            objective.AddTerm(instance.DistancesMatrix[i,j], x[i,j]);
          }
        }

        model.SetObjective(objective, GRB.MAXIMIZE);

        model.Optimize();

        Console.WriteLine("Obj: " + model.ObjVal);

        model.Dispose();
        env.Dispose();
      }
      catch (GRBException e)
      {
        Console.WriteLine("Error code: " + e.ErrorCode + ". " + e.Message);
      }
    }
  }
}

/*

class mip1_cs
{
  static void Main()
  {

    
    try {

      // Create an empty environment, set options and start
      GRBEnv env = new GRBEnv(true);
      env.Set("LogFile", "ftsp.log");
      env.Start();

      // Create empty model
      GRBModel model = new GRBModel(env);

      // Create variables
      // GRBVar x = model.AddVar(0.0, 1.0, 0.0, GRB.BINARY, "x");
      // GRBVar y = model.AddVar(0.0, 1.0, 0.0, GRB.BINARY, "y");
      // GRBVar z = model.AddVar(0.0, 1.0, 0.0, GRB.BINARY, "z");
      GRBVar[,] x = new GRBVar[i,j];
      GRBVar[,] c = new GRBVar[i,j];

      // TENTATIVA
      x[0,0] = model.AddVar(0.0, 1.0, 0.0, GRB.BINARY, "x"+i+j);

      // Set objective: maximize x + y + 2 z
      model.SetObjective(x + y + 2 * z, GRB.MAXIMIZE);

      // Add constraint: x + 2 y + 3 z <= 4
      obj = sum();
      model.AddConstr(x + 2 * y + 3 * z <= 4.0, "c0");

      // Add constraint: x + y >= 1
      model.AddConstr(x + y >= 1.0, "c1");

      // Optimize model
      model.Optimize();

      Console.WriteLine(x.VarName + " " + x.X);
      Console.WriteLine(y.VarName + " " + y.X);
      Console.WriteLine(z.VarName + " " + z.X);

      Console.WriteLine("Obj: " + model.ObjVal);

      // Dispose of model and env
      model.Dispose();
      env.Dispose();

    } catch (GRBException e) {
      Console.WriteLine("Error code: " + e.ErrorCode + ". " + e.Message);
    }
  }
}

*/