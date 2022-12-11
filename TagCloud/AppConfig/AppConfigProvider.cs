using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TagCloud.ImageProcessing;

namespace TagCloud.AppConfig
{
    public class AppConfigProvider : IAppConfigProvider
    {
        private readonly IEnumerable<string> args;

        public AppConfigProvider(IEnumerable<string> args)
        {
            this.args = args;
        }

        public IAppConfig GetAppConfig()
        {
            return new AppConfig(GetSolutionDirectory().FullName + "\\TestText.txt",
                                GetSolutionSubDirectory("TagCloudImages").FullName + "\\WordCloud.png", 
                                 new ImageSettings());
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
    }
}
