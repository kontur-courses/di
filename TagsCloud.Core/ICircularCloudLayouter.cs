using System.Drawing;

namespace TagsCloud.Core
{
    public interface ICircularCloudLayouter
    {
        Rectangle PutNextRectangle(Size rectangleSize);
    }
}