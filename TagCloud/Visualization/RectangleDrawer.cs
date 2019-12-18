using System.Drawing;

namespace TagCloud.Visualization
{
    public class RectangleDrawer : ITagCloudElementDrawer
    {
        public void Draw(Graphics graphics, TagCloudElement element)
        {
            graphics.DrawRectangle(new Pen(element.Color), element.Rectangle);
        }
    }
}