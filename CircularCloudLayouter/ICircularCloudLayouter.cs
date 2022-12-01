using System.Drawing;

namespace CircularCloudLayouter;

public interface ICircularCloudLayouter
{
    Rectangle PutNextRectangle(Size rectSize);
}