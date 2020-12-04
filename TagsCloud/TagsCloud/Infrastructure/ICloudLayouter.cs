using System.Drawing;

namespace TagsCloud
{
    public interface ICloudLayouter
    {
        void UpdateCenterPoint(ImageSettings settings);
        void ClearLayouter();
        Rectangle PutNextRectangle(Size rectangleSize);
    }
}