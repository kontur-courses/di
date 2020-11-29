using System.Drawing;

namespace TagCloud.Infrastructure.Settings
{
    public class Settings : IFileSettingsProvider, ITagCloudSettingsProvider
    {
        public string Path { get; set; }
        public Point Center { get; set; }
    }
}