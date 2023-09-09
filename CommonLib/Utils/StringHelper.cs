﻿using CommonLib.Entities;

namespace CommonLib.Utils
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

        public static string[] ReadAllLines(string filePath)
        {
            int nonEmptyLines = 0;
            int currentLine = 0;

            string[] originalLines = File.ReadAllLines(filePath);

            foreach (string line in originalLines) if (!string.IsNullOrWhiteSpace(line)) nonEmptyLines++;

            string[] newLines = new string[nonEmptyLines];

            foreach (string line in originalLines)
                if (!string.IsNullOrEmpty(line))
                {
                    newLines[currentLine] = line;
                    currentLine++;
                }

            return newLines;
        }

        public static string FindLogDirectoryPath(string filePath)
        {
            return filePath.Substring(0, filePath.IndexOf("Instances")) + "Logs/";
        }
    }
}
