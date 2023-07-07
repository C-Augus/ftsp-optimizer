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
                GurobiModel.ExecuteGurobiModel();
            }
            catch (GRBException e)
            {
                Console.WriteLine("Error code: " + e.ErrorCode + ". " + e.Message);
            }
        }
    }
}