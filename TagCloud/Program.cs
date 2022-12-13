using System;
using System.Text;
using System.Collections.Generic;
using System.IO;
using Autofac;
using TagCloud.AppConfig;
using TagCloud.App;
using System.Linq;

namespace TagCloud
{
    public class Program
    {
        static void Main(string[] args)
        {
            var inputfile = GetSolutionDirectory().FullName + "\\TestText.txt";
            var outputfile = GetSolutionSubDirectory("TagCloudImages").FullName + "\\WordCloud.png";

            var argsssss = $"-i {inputfile} -o {outputfile} -h 600 -w 600 -b White -f Arial -l 5 -p 40 -k random -z elipse".Split(' ');

            var appConfig = new ConsoleAppConfigProvider(argsssss).GetAppConfig();
            var container = ContainerConfig.Configure(appConfig);
            var app = container.Resolve<IApp>();
            app.Run(appConfig);
        }

        private static DirectoryInfo GetSolutionSubDirectory(string subDirectoryName)
        {
            var solutionDirectory = GetSolutionDirectory();

            return GetSubDirectory(solutionDirectory, subDirectoryName);
        }

        private static DirectoryInfo GetSolutionDirectory()
        {
            var currentDirectory = new DirectoryInfo(Environment.CurrentDirectory);

            while (currentDirectory != null && !currentDirectory.GetFiles("*.sln").Any())
            {
                currentDirectory = currentDirectory.Parent;
            }

            return currentDirectory;
        }

        private static DirectoryInfo GetSubDirectory(DirectoryInfo parentDirectory, string subDirectoryName)
        {
            var subDirectoryFullName = Path.Combine(parentDirectory.FullName, subDirectoryName);

            if (!Directory.Exists(subDirectoryFullName))
                return Directory.CreateDirectory(subDirectoryFullName);

            return new DirectoryInfo(subDirectoryFullName);
        }

        private static void CreateTestText()
        {
            var lines = File.ReadAllLines("TestWords.txt");

            Dictionary<string, int> dict = new Dictionary<string, int>();

            foreach (var line in lines)
            {
                var tokens = line.Split(new[] { '\t' }, StringSplitOptions.RemoveEmptyEntries);

                var chars = tokens[0].ToCharArray();

                chars[0] = char.ToUpper(chars[0]);

                var word = new string(chars);

                var frequency = Convert.ToInt32(Math.Round(double.Parse(tokens[1]) / 10));

                dict.Add(word, frequency);
            }

            var sb = new StringBuilder();

            foreach (var pair in dict)
            {
                for (int i = 0; i < pair.Value; i++)
                {
                    sb.AppendLine(pair.Key);
                }
            }

            File.WriteAllText("TestText.txt", sb.ToString());
        }
    }
}

