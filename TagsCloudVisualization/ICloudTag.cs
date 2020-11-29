using System.Drawing;

namespace TagsCloudVisualization
{
    public interface ICloudTag
    {
        public Rectangle Size { get;}
        public string Text { get; }
    }
}