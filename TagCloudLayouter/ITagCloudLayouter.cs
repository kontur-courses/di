using System.Drawing;

namespace CircularCloudLayouter;

public interface ITagCloudLayouter
{
    Rectangle PutNextRectangle(Size rectSize);
}