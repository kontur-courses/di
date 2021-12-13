using System.Drawing;

namespace Visualization
{
    public class VisualizerSettings
    {
        public Size ImageSize { get; set; }
        public Font TextFont { get; set; }
        public Color TextColor { get; set; }
        public Color BackgroundColor { get; set; }

        public VisualizerSettings(Size imageSize, Font textFont, Color textColor, Color backgroundColor)
        {
            this.ImageSize = imageSize;
            this.TextFont = textFont;
            this.TextColor = textColor;
            this.BackgroundColor = backgroundColor;
        }
    }
}