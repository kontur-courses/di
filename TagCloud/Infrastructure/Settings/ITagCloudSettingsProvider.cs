using System.Drawing;

namespace TagCloud.Infrastructure.Settings
{
    public interface ITagCloudSettingsProvider
    {
        public Point Center { get; }
    }
}