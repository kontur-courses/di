using System.Drawing;

namespace TagCloud.Visualization
{
    public class WordDrawer : ITagCloudElementDrawer
    {
        public void Draw(Graphics graphics, TagCloudElement element)
        {
            graphics.DrawString(element.WordValue,
                element.Font,
                new SolidBrush(element.Color),
                element.Rectangle);
        }
    }
}