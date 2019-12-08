using System.Drawing;

namespace TagCloudContainer
{
    public class DrawingOptions
    {
        public Brush BackgroundBrush { get; private set; }
        public Brush WordBrush { get; private set; }
        public Pen Pen { get; private set; }
        public Font Font { get; private set; }

        private DrawingOptions(DrawingOptions parent) :
            this(parent.BackgroundBrush, parent.WordBrush, parent.Pen, parent.Font)
        {
        }

        public DrawingOptions() :
            this(new SolidBrush(Color.White), new SolidBrush(Color.Orange), new Pen(Color.Blue),
                new Font(FontFamily.GenericMonospace, 14))
        {
        }

        private DrawingOptions(Brush backgroundBrush, Brush wordBrush, Pen pen, Font font)
        {
            BackgroundBrush = backgroundBrush;
            WordBrush = wordBrush;
            Pen = pen;
            Font = font;
        }

        public DrawingOptions WithBackgroundBrush(Brush brush)
        {
            return new DrawingOptions(this) {BackgroundBrush = brush};
        }

        public DrawingOptions WithFont(Font font)
        {
            return new DrawingOptions(this) {Font = font};
        }

        public DrawingOptions WithWordBrush(Brush brush)
        {
            return new DrawingOptions(this) {WordBrush = brush};
        }

        public DrawingOptions WithPen(Pen pen)
        {
            return new DrawingOptions(this) {Pen = pen};
        }
    }
}