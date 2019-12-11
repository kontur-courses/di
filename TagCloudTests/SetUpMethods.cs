using System;
using System.IO;

namespace TagCloudTests
{
    public static class SetUpMethods
    {
        public static void CreateFile(string path)
        {
            var f = File.Create($"{Directory.GetParent(Environment.CurrentDirectory).FullName}\\ForParsingTests.txt");
            f.Close();
        }

        public static void WriteLinesInFile(string path, params string[] lines)
        {
            using (var sw = new StreamWriter(path))
            {
                foreach (var line in lines)
                    sw.WriteLine(line);
            }
        }

        public static string GetPathToWordsToRead()
        {
            return $"{Directory.GetParent(Environment.CurrentDirectory).Parent.FullName}\\ForParsingTests.txt";
        }

        public static string GetPathToBoringWords()
        {
            return $"{Directory.GetParent(Environment.CurrentDirectory).Parent.FullName}\\BoringWords.txt";
        }
    }
}