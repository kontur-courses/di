using System.Drawing;

namespace TagCloud.Visualization
{
    public class RectanglePainter : ITagCloudElementPainter
    {
        public void Paint(Graphics graphics, TagCloudElement element)
        {
            graphics.DrawRectangle(new Pen(element.Color), element.Rectangle);
        }
    }
}