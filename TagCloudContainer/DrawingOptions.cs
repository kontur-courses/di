using System.Drawing;

namespace TagCloudContainer
{
    public class DrawingOptions
    {
        public Brush BackgroundBrush { get; private set; }
        public Font Font { get; private set; }

        private DrawingOptions(DrawingOptions parent) :
            this(parent.BackgroundBrush, parent.Font)
        {
        }

        public DrawingOptions() :
            this(new SolidBrush(Color.White), new Font(FontFamily.GenericSerif, 14))
        {
        }

        private DrawingOptions(Brush backgroundBrush, Font font)
        {
            BackgroundBrush = backgroundBrush;
            Font = font;
        }

        public DrawingOptions WithBackgoundBrush(Brush brush)
        {
            return new DrawingOptions(this) {BackgroundBrush = brush};
        }

        public DrawingOptions WithFont(Font font)
        {
            return new DrawingOptions(this) {Font = font};
        }
    }
}