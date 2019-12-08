using System.Drawing;

namespace TagCloudContainer
{
    public class DrawingOptions
    {
        public Brush BackgroundBrush { get; }
        public Brush WordBrush { get; }
        public Font Font { get; }
        public Pen Pen { get; }

        public DrawingOptions() : this(new SolidBrush(Color.White), new SolidBrush(Color.Blue),
            new Font(FontFamily.GenericSerif, 14), new Pen(Color.Blue))
        {
        }

        public DrawingOptions(Brush backgroundBrush, Brush wordBrush, Font font, Pen pen)
        {
            BackgroundBrush = backgroundBrush;
            WordBrush = wordBrush;
            Font = font;
            Pen = pen;
        }
    }
}