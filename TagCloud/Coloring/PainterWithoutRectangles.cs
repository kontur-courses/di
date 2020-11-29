using System.Drawing;

namespace TagCloud.Coloring
{
    public class PainterWithoutRectangles: IPainter
    {
        private readonly Brush textBrush;
        public PainterWithoutRectangles(Color color)
        {
            textBrush = new SolidBrush(color);
        }
        
        public void DrawAndFillRectangle(Rectangle rectangle, Graphics graphics)
        {
        }

        public void DrawString(Rectangle rectangle, string str, string fontFamily, Graphics graphics)
        {
            graphics.DrawString(str, new Font(fontFamily,rectangle.Height/2), textBrush, rectangle);
        }
    }
}