using System.Drawing;

namespace TagsCloudVisualization
{
    public class CloudTag : ICloudTag
    {
        public Rectangle Rectangle { get; private set; }//TODO fix naming
        public string Text { get; private set; }

        public CloudTag(Rectangle rectangle, string text)
        {
            Rectangle = rectangle;
            Text = text;
        }
    }
}