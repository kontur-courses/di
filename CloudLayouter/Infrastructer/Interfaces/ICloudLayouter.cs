using System.Drawing;

namespace CloudLayouter.Infrastructer.Interfaces
{
    public interface ICloudLayouter
    {
        Rectangle PutNextRectangle(Size size);
    }
}