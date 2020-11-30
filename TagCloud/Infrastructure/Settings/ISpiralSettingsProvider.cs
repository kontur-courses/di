using System.Drawing;

namespace TagCloud.Infrastructure.Settings
{
    public interface ISpiralSettingsProvider
    {
        public Point Center { get; }
        public int Increment { get; }
    }
}