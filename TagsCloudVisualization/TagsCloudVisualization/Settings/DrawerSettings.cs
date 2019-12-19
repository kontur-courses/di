using System.Drawing;
using TagsCloudVisualization.Providers.Sizable;

namespace TagsCloudVisualization.Settings
{
    public class DrawerSettings
    {
        public DrawerSettings(SolidBrush textBrush, Color backgroundColor, Font textFont, int height, int width,
            SizeSelectorType sizeSelector)
        {
            TextBrush = textBrush;
            BackgroundColor = backgroundColor;
            TextFont = textFont;
            Height = height;
            Width = width;
            SizeSelector = sizeSelector;
        }

        public SolidBrush TextBrush { get; }
        public Color BackgroundColor { get; }
        public Font TextFont { get; }
        public SizeSelectorType SizeSelector { get; }

        public int Height { get; }
        public int Width { get; }
    }
}