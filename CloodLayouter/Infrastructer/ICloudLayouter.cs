using System.Drawing;

namespace CloudLayouter.Infrastructer
{
    public interface ICloudLayouter
    {
        Rectangle PutNextRectangle(Size size);
    }
}