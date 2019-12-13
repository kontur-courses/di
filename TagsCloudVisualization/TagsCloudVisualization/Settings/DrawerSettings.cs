using System.Drawing;

namespace TagsCloudVisualization.Settings
{
    public class DrawerSettings
    {
        public DrawerSettings(SolidBrush textBrush, Color backgroundColor, Font textFont)
        {
            TextBrush = textBrush;
            BackgroundColor = backgroundColor;
            TextFont = textFont;
            Heigth = 10000;
            Width = 10000;
        }

        public SolidBrush TextBrush { get; }
        public Color BackgroundColor { get; }
        public Font TextFont { get; }

        public int Heigth { get; }
        public int Width { get; }
    }
}