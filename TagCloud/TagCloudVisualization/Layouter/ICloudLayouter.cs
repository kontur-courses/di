using System.Drawing;

namespace TagCloud.Interfaces
{
    public interface ICloudLayouter
    {
        void Clear();
        Rectangle PutNextRectangle(Size rectangleSize);
    }
}