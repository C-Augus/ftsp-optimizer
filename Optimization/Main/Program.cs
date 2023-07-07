using Gurobi;
using Optimizer.Model;

namespace Optimizer.main
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                // Create an empty environment, set options and start
                GRBEnv env = new GRBEnv(true);
                env.Set("LogFile", "ftsp.log");
                env.Start();

                // Create empty model
                GRBModel model = GurobiModel.GetGurobiModel();

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