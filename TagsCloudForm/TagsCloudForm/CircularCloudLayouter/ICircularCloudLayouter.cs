using System.Drawing;

namespace TagsCloudForm.CircularCloudLayouter
{
    public interface ICircularCloudLayouter
    {
        Rectangle PutNextRectangle(Size rectangleSize);
    }
}
