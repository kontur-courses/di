using System.Drawing;

namespace TagCloudContainer
{
    public class DrawingOptions
    {
        public Brush BackgroundBrush = new SolidBrush(Color.White);
        public Brush WordBrush = new SolidBrush(Color.Blue);
        public Font Font = new Font(FontFamily.GenericSerif, 14);
        public Pen Pen = new Pen(Color.Blue);

        public DrawingOptions()
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