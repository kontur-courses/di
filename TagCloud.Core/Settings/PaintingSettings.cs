using System.Drawing;

namespace TagCloud.Core.Settings
{
    public class PaintingSettings : ISettings
    {
        public Color BackgroundColor { get; set; } = Color.White;
        public Brush TagBrush { get; set; } = Brushes.Navy;
    }
}