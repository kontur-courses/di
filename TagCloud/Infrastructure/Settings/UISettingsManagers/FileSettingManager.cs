using System;
using System.IO;

namespace TagCloud.Infrastructure.Settings.UISettingsManagers
{
    public class FileSettingManager : ISettingsManager
    {
        private readonly Func<IFileSettingsProvider> fileSettingsProvider;

        public FileSettingManager(Func<IFileSettingsProvider> fileSettingsProvider)
        {
            this.fileSettingsProvider = fileSettingsProvider;
        }

        public string Title => "Input file";
        public string Help => "Type path to file to analyze";

        public bool TrySet(string value)
        {
            if (!File.Exists(value))
            {
                Console.WriteLine($"{value} not found");
                return false;
            }

            fileSettingsProvider().Path = value;
            return true;
        }

        public string Get()
        {
            return fileSettingsProvider().Path;
        }
    }
}