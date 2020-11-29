using System.Drawing;

namespace TagCloud.Infrastructure.Settings
{
    public class Settings : IFileSettingsProvider, ITagCloudSettingsProvider
    {
        public string Path { get; private set; }
        public Point Center { get; private set; }
    }
}