using System.Drawing;

namespace TagCloud.layouter
{
    public interface ICloudLayouter
    {
        public RectangleF PutNextRectangle(Size rectangleSize);
    }
}