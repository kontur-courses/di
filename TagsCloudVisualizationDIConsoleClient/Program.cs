using System;
using System.IO;
using System.Linq;

namespace TagsCloudVisualizationDIConsoleClient
{
    public class Program
    {
        public static void Main(string[] args)
        {

            if (args.Contains("-h") || args.Contains("--help"))
            {
                Console.WriteLine("HELP");
                return;
            }

            if (args.Length != 2)
                throw new ArgumentException("Incorrect Number Of Arguments");

            var pathToFile = args.ElementAtOrDefault(0);
            var pathToSave = args.ElementAtOrDefault(1);

            var safeDirectory = pathToSave.Remove(pathToSave.LastIndexOf('\\'));

            CheckArguments(pathToFile, safeDirectory);

            TagsCloudVisualizationDI.Program.Main(pathToFile, pathToSave);
        }

        private static void CheckArguments(string pathToFile, string pathToSafeFile)
        {
            if (!File.Exists(pathToFile))
                throw new Exception($"Giving path to file: {pathToFile} is not valid, EXC");

            if (!Directory.Exists(pathToSafeFile))
                throw new Exception($"Giving path to safefile: {pathToSafeFile} is not valid, EXC");
        }
    }
}
