using System.Drawing;

namespace TagsCloudContainer.CloudLayouters
{
    public interface ICloudLayouter
    {
        Rectangle PutNextRectangle(Size size);
    }
}