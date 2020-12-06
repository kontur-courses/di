using System.Drawing;

namespace TagCloud.Infrastructure.Settings
{
    public interface ISpiralSettingsProvider
    {
        public Point Center { get; set; }
        public int Increment { get; set; }
    }
}