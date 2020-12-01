using System.Drawing;

namespace TagsCloudVisualization
{
    public class CloudTag : ICloudTag
    {
        public Rectangle Size { get; private set; }//TODO fix naming
        public string Text { get; private set; }

        public CloudTag(Rectangle size, string text)
        {
            Size = size;
            Text = text;
        }
    }
}