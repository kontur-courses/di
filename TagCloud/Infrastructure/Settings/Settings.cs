using System.Drawing;

namespace TagCloud.Infrastructure.Settings
{
    public class Settings : IFileSettingsProvider, ITagCloudSettingsProvider, IExcludeTypesSettingsProvider
    {
        public string Path { get; set; }
        public Point Center { get; set; }
        public string[] ExcludedTypes { get; set; }
    }
}