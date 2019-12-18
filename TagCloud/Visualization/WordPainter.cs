using System.Drawing;

namespace TagCloud.Visualization
{
    public class WordPainter : ITagCloudElementPainter
    {
        public void Paint(Graphics graphics, TagCloudElement element)
        {
            graphics.DrawString(element.WordValue,
                element.Font,
                new SolidBrush(element.Color),
                element.Rectangle);
        }
    }
}