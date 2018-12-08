using System.Drawing;

namespace Extensions
{
    public static class GraphicsExtensions
    {
        public static void DrawFramedRectangle(this Graphics graphics, Rectangle rect, int frameSize = 1,
            Brush fillBrush = null, Brush frameBrush = null)
        {
            fillBrush = fillBrush ?? Brushes.White;
            frameBrush = frameBrush ?? Brushes.Black;
            graphics.FillRectangle(frameBrush, rect);
            graphics.FillRectangle(fillBrush, 
                new Rectangle(rect.X+frameSize,rect.Y+frameSize,
                    rect.Width-2*frameSize,rect.Height-2*frameSize));            
        } 
    }
}