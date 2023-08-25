﻿using Optimizer.Entities;

namespace Optimizer.Utils
{
    abstract class ResultPrinter
    {
        public static void ExportInstanceData(ref TSPInstance instance)
        {
            string formattedLine = string.Format($"{instance.Name,-25} | {instance.Dimension,9} | {instance.NumberOfFamilies, 3} | {instance.Solution, -8} |{instance.Date, 31} | {instance.ElapsedTime} | {instance.UpperBound} | {instance.LowerBound} | {instance.Gap}");

            using (StreamWriter writer = new (instance.LogDirectoryPath + "generalLogs" + ".log", true))
            {
                writer.WriteLine(new string('-', formattedLine.Length));
                writer.WriteLine(formattedLine);
            }
        }
    }
}