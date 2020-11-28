using System.Drawing;

namespace TagCloud
{
    public interface ICloudLayouter
    {
        public Rectangle PutNextRectangle(Size rectangleSize);
    }
}