using System.Drawing;
using TagsCloudVisualization.Interfaces;

namespace TagsCloudVisualization
{
    internal class DrawerOption : IDrawerOption
    {
        public DrawerOption(SolidBrush textBrush, Color backgroundColor, Font textOption)
        {
            TextBrush = textBrush;
            BackgroundColor = backgroundColor;
            TextOption = textOption;
        }

        public SolidBrush TextBrush { get; }
        public Color BackgroundColor { get; }
        public Font TextOption { get; }
    }
}