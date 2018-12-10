using System.Drawing;

namespace TagCloud.Core.Settings
{
    public class PaintingSettings : ISettings
    {
        public Color BackgroundColor { get; set; }
        public Brush TagBrush { get; set; }
    }
}