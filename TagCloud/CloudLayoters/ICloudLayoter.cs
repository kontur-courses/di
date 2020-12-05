using System.Drawing;
using TagCloud.PointGetters;

namespace TagCloud.CloudLayoters
{
    public interface ICloudLayoter
    {
        public IPointGetter PointGetter { get; }
        public int Top { get; }
        public int Bottom { get; }
        public int Left { get; }
        public int Right { get; }
        public Rectangle PutNextRectangle(Size rectangleSize);
    }
}
