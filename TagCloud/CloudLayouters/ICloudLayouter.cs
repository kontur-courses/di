using System.Drawing;

namespace TagCloud.CloudLayouters
{
    public interface ICloudLayouter
    {
        public delegate ICloudLayouter Factory();
        public Point Center { get; }
        public Rectangle PutNextRectangle(Size rectangleSize);
    }
}
