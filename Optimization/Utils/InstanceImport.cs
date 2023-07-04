using Optimization.Entities;

namespace Optimization.Utils
{
    public class Instance 
    {
        public static TSPInstance ReadInstFromFile(/*string filePath*/)
        {
            // string sourceFilePath = @"C:\code\ftsp-optimizer\Optimization\Public\Instances\d198.sftsp_198_29_1.txt";
            // string targetFilePath = @"C:\code\ftsp-optimizer\Optimization\Public\Instances\d198.sftsp_198_29_1_out.txt";
            string sourceFilePath = @"/home/ciro/Documents/ftsp-optimizer/Optimization/Public/Instances/d198.sftsp_198_29_1.txt";
            string targetFilePath = @"/home/ciro/Documents/ftsp-optimizer/Optimization/Public/Instances/d198.sftsp_198_29_1.txt_out.txt";
            string[] lines = File.ReadAllLines(sourceFilePath);

            string sourceFolderPath = Path.GetDirectoryName(sourceFilePath);
            //string targetFolderPath = sourceFolderPath + @"\out";
            //string targetFilePath = targetFolderPath + @"\summary.csv";

            //Directory.CreateDirectory(targetFolderPath);

            //Feeding numberOfNodes(|N|), numberOfFamilies(V) and numberOfVisits(L)
            string[] header = lines[0].Split(' ');

            int numberOfNodes = int.Parse(header[0]);
            int numberOfFamilies = int.Parse(header[1]);
            int numberOfVisits = int.Parse(header[2]);

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

            using (StreamWriter sw = File.AppendText(targetFilePath))
            {
                sw.WriteLine(instance.NumberOfNodes.ToString() + ' ' + instance.NumberOfFamilies.ToString() + ' ' + instance.NumberOfVisits.ToString());
                string ArrayString = string.Join(" ", instance.ArrayOfFamilies);
                sw.WriteLine(ArrayString);
                ArrayString = string.Join(" ", instance.ArrayOfVisits);
                sw.WriteLine(ArrayString);
            }

            for (int i = 3; i < numberOfNodes + 3; i++)
            {
                string[] line = lines[i].Split(' ');
                for (int j = 0; j < numberOfNodes; j++)
                {
                    instance.DistancesMatrix[i - 3, j] = int.Parse(line[j]);
                }
            }

            using (StreamWriter sw = File.AppendText(targetFilePath))
            {
                for (int i = 0; i < numberOfNodes; i++)
                {
                    string ArrayString = string.Join(" ", GetRow(instance.DistancesMatrix, i, numberOfNodes));
                    sw.WriteLine(ArrayString);
                }
            }

            return instance;
        }

        static int[] GetRow(int[,] matrix, int row, int columns)
        {
            int[] rowArray = new int[columns];

            for (int i = 0; i < columns; i++)
            {
                rowArray[i] = matrix[row, i];
            }

            return rowArray;
        }
    }
}