using Optimizer.Entities;

namespace Optimizer.Utils
{
    public abstract class StringHelper
    {
        public static int FindNodesSection(ref string[] lines)
        {
            for (int line = 0; line < lines.Length; line++)
                if (lines[line] == "NODE_COORD_SECTION") return line;
            return -1;
        }

        public static int FindFamiliesSection(ref string[] lines)
        {
            for (int line = lines.Length - 1; line > 0; line--)
                if (lines[line] == "FAMILY_SECTION") return line;
            return -1;
        }

        public static string ParseValue(string line)
        {
            return line.Substring(line.IndexOf(":") + 2).Trim();
        }

        public static string ReplaceSpacesWithUnderscores(string line)
        {
            return line.Replace(" ", "_");
        }
    }
}
