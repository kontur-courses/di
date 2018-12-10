using System.Drawing;

namespace TagsCloudContainer.CircularCloudLayouters
{
    public interface ICircularCloudLayouter
    {
        Rectangle PutNextRectangle(Size size);
    }
}