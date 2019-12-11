using System.Drawing;

namespace TagCloudContainer
{
    public class DrawingOptions
    {
        private DrawingOptions(DrawingOptions parent) :
            this(parent.BackgroundBrush, parent.WordBrush, parent.Pen, parent.Font, parent.ImageSize)
        {
        }

        public DrawingOptions() :
            this(new SolidBrush(Color.White), new SolidBrush(Color.Orange), new Pen(Color.Blue),
                new Font(FontFamily.GenericMonospace, 14), Size.Empty)
        {
        }

        private DrawingOptions(Brush backgroundBrush, Brush wordBrush, Pen pen, Font font, Size imageSize)
        {
            BackgroundBrush = backgroundBrush;
            WordBrush = wordBrush;
            Pen = pen;
            Font = font;
            ImageSize = imageSize;
        }

        public Brush BackgroundBrush { get; set; }
        public Brush WordBrush { get; set; }
        public Pen Pen { get; set; }
        public Font Font { get; set; }
        public Size ImageSize { get; set; }
    }
}