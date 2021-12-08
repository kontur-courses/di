using System.Drawing;

namespace CloudTagContainer
{
    public class VisualizerSettings
    {
        public Size ImageSize { get; set; }
        public Font TextFont { get; set; }
        public Color TextColor { get; set; }
        public Color StrokeColor { get; set; }

        public VisualizerSettings(Size imageSize, Font textFont, Color textColor, Color strokeColor)
        {
            this.ImageSize = imageSize;
            this.TextFont = textFont;
            this.TextColor = textColor;
            this.StrokeColor = strokeColor;
        }
    }
}