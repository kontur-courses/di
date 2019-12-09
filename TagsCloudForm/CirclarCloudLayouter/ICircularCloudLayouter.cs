using System.Drawing;

namespace CircularCloudLayouter
{
    public interface ICircularCloudLayouter
    {
        Rectangle PutNextRectangle(Size rectangleSize);

        void SetCompression(int xCompression, int yCompression);
    }
}
