using System.Drawing;

namespace TagsCloud
{
    public interface ICloudLayouter
    {
        void ClearLayouter();
        Rectangle PutNextRectangle(Size rectangleSize);
    }
}