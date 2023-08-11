using Optimizer.Entities;

namespace Optimizer.Utils
{
    public class Instance 
    {
        public static TSPInstance ReadInstanceFromFile(string filePath)
        {
            string sourceFilePath = filePath;
            string targetFilePath = filePath + "_out";

            string[] lines = File.ReadAllLines(sourceFilePath);

            //Feeding numberOfNodes(|N|), numberOfFamilies(V) and numberOfVisits(L)
            string[] header = lines[0].Split(' ');

            int numberOfNodes = int.Parse(header[0]);
            ushort numberOfFamilies = ushort.Parse(header[1]);
            ushort numberOfVisits = ushort.Parse(header[2]);

            //Auxiliar array of the number of nodes of each family
            string[] strFamilies = lines[1].Split(' ');
            int[] intFamilies = new int[numberOfFamilies];

            //Auxiliar array of the number of visits of each family
            string[] strVisits = lines[2].Split(' ');
            int[] intVisits = new int[numberOfFamilies];

            //Feeding both arrays
            for (int i = 0; i < numberOfFamilies; i++)
            {
                intFamilies[i] = int.Parse(strFamilies[i]);
                intVisits[i] = int.Parse(strVisits[i]);
            }


            TSPInstance instance = new
            (
                numberOfNodes, 
                numberOfFamilies, 
                numberOfVisits,
                intFamilies,
                intVisits, 
                numberOfNodes
            );

            for (int i = 3; i < numberOfNodes + 3; i++)
            {
                string[] line = lines[i].Split(' ');
                for (int j = 0; j < numberOfNodes; j++)
                {
                    instance.DistancesMatrix[i - 3, j] = int.Parse(line[j]);
                }
            }

            return instance;

            // using (streamwriter sw = file.appendtext(targetfilepath))
            // {
            //     sw.writeline(instance.numberofnodes.tostring() + ' ' + instance.numberoffamilies.tostring() + ' ' + instance.numberofvisits.tostring());
            //     string arraystring = string.join(" ", instance.arrayoffamilies);
            //     sw.writeline(arraystring);
            //     arraystring = string.join(" ", instance.arrayofvisits);
            //     sw.writeline(arraystring);
            // }

            // using (StreamWriter sw = File.AppendText(targetFilePath))
            // {
            //     for (int i = 0; i < numberOfNodes; i++)
            //     {
            //         string ArrayString = string.Join(" ", GetRow(instance.DistancesMatrix, i, numberOfNodes));
            //         sw.WriteLine(ArrayString);
            //     }
            // }
        }

        // static int[] GetRow(int[,] matrix, int row, int columns)
        // {
        //     int[] rowArray = new int[columns];

        //     for (int i = 0; i < columns; i++)
        //     {
        //         rowArray[i] = matrix[row, i];
        //     }

        //     return rowArray;
        // }
    }
}