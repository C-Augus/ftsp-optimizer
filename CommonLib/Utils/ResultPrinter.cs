using CommonLib.Entities;

namespace CommonLib.Utils
{
    public abstract class ResultPrinter
    {
        public static void ExportInstanceData(ref TSPInstance instance)
        {
            string formattedLine = string.Format($"{instance.Name, -25} | {instance.Dimension, 3} | {instance.NumberOfFamilies, 3} | {instance.Solution, -3} |{instance.Date, 23} | {instance.ElapsedTime, 20} | {instance.UpperBound} | {instance.LowerBound} | {instance.Gap}");

            using (StreamWriter writer = new (instance.LogDirectoryPath + "generalLogs" + ".log", true))
            {
                writer.WriteLine(new string('-', formattedLine.Length));
                writer.WriteLine(formattedLine);
            }
        }
    }
}