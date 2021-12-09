using System;
using System.Linq;

namespace TagsCloudVisualizationDIConsoleClient
{
    public class Program
    {
        static void Main(string[] args)
        {
            if (args[0] == "-h" || args[0] == "--help")
            {
                Console.WriteLine("HELP");
                return;
            }

            if (args.Length < 2)
                throw new Exception("Not enough arguments");
            if (args.Length > 3)
                throw new Exception("Too many arguments");

            var pathToFile = args[0];


            var pathToSave = args[1];

            string pathToMystem = null;
            if (args.Length == 3)
                pathToMystem = args[2];


            TagsCloudVisualizationDI.Program.Main(pathToFile, pathToSave, pathToMystem);
        }
    }
}
