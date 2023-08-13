using Gtk;
using Optimizer.Entities;

namespace Optimizer.Utils
{
    public abstract class InstanceHelper
    {
        // Delegate of method chaining, responsible for processing all methods listed on InstanceHelper in the following order.
        public delegate void ProcessInstance(ref TSPInstance instance, ref string[] lines);
        public static ProcessInstance Process =
            new ProcessInstance(FindFileHeaderValues)
            + FindFamilies
            + FindNodes
            + LinkNodesToFamilies
            + FindFamiliesAndVisits;

        // FindFileHeaderValues is responsible for searching the selected values based on their tags inside a given
        // file. As each of them is found, it will assign the value to the instance object's respective property.
        public static void FindFileHeaderValues(ref TSPInstance instance, ref string[] lines)
        {
            for (int line = 0; line < StringHelper.FindNodesSection(ref lines); line++)
            {
                switch (lines[line])
                {
                    case string when lines[line].StartsWith("NAME"):
                        instance.Name = StringHelper.ParseValue(lines[line]);
                        break;
                    case string when lines[line].StartsWith("TYPE"):
                        instance.Type = StringHelper.ParseValue(lines[line]);
                        break;
                    case string when lines[line].StartsWith("COMMENT"):
                        instance.Comment = StringHelper.ParseValue(lines[line]);
                        break;
                    case string when lines[line].StartsWith("DIMENSION"):
                        instance.Dimension = int.Parse(StringHelper.ParseValue(lines[line]));
                        break;
                    case string when lines[line].StartsWith("EDGE_WEIGHT_TYPE"):
                        instance.EdgeWeightType = StringHelper.ParseValue(lines[line]);
                        break;
                    default:
                        break;
                }
            }
        }

        // FindFamilies is responsible for retrieving the familes and their visits required from a given file. As new 
        // families are identified, it will directly assign a new Family object to the instance's object list of Families.
        public static void FindFamilies(ref TSPInstance instance, ref string[] lines)
        {
            int familySectionLine = StringHelper.FindFamiliesSection(ref lines);

            for (int line = familySectionLine + 2; line < lines.Length - 1; line++)
            {
                string[] familyStringLine = lines[line].Split(' ');
                instance.Families.Add(new Family(line - (familySectionLine + 2), int.Parse(familyStringLine[1])));
            }
        }

        // FindNodes is responsible for retrieving the nodes from a given file. As new nodes are
        // identified, it will directly assign a new Node object to the instance's object list of Nodes.
        public static void FindNodes(ref TSPInstance instance, ref string[] lines)
        {
            int nodeCoordSectionLine = StringHelper.FindNodesSection(ref lines);
            int familySectionLine = StringHelper.FindFamiliesSection(ref lines);

            for (int line = nodeCoordSectionLine + 1; line < familySectionLine; line++)
            {
                string[] coordinateStringLine = lines[line].Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                instance.Nodes.Add(new Entities.Node(float.Parse(coordinateStringLine[1]), float.Parse(coordinateStringLine[2])));
            }
        }

        // LinkNodesToFamilies is responsible for assigning families to nodes and vice-versa,
        // as their classes features a list of Nodes and a Family Family property respectively.
        // Once currentNode hits its family's number of nodes, then the next family should be triggered.
        public static void LinkNodesToFamilies(ref TSPInstance instance, ref string[] lines)
        {
            int currentFamilyLine = 0;
            int currentNode = 0;

            foreach(Family family in instance.Families)
            {
                for (; currentNode < (family.NumberOfNodes + currentFamilyLine); currentNode++)
                {
                    instance.Nodes[currentNode].Family = family;
                    family.Nodes.Add(instance.Nodes[currentNode]);
                }

                currentFamilyLine += family.NumberOfNodes;
            }
        }

        //FindFamiliesAndVisits is responsible for retrieving two numbers from a given file, which are the
        //number of families and the number of visits, and assign them respectively to the instance object.
        public static void FindFamiliesAndVisits(ref TSPInstance instance, ref string[] lines)
        {
            int firstLineFromFamiliesSection = StringHelper.FindFamiliesSection(ref lines) + 1;

            string[] auxiliaryString = lines[firstLineFromFamiliesSection].Split(' ');

            instance.NumberOfFamilies = int.Parse(auxiliaryString[0]);
            instance.NumberOfVisits = int.Parse(auxiliaryString[1]);
        }
    }
}
