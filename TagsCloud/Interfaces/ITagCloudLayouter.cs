using System.Drawing;

namespace TagsCloud.Interfaces
{
    public interface ITagCloudLayouter
    {
        Rectangle PutNextRectangle(Size size);
    }
}
