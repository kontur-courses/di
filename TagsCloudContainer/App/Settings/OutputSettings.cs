using System.IO;
using TagsCloudContainer.Infrastructure.Settings;

namespace TagsCloudContainer.App.Settings
{
    public class OutputSettings : IOutputSettingsHolder
    {
        public static readonly OutputSettings Instance = new OutputSettings();

        private OutputSettings()
        {
            OutputDirectory = Path.Combine(Directory.GetCurrentDirectory(),
                "..", "..", "..");
        }

        public string OutputDirectory { get; set; }
    }
}