using System.Drawing;

namespace CloodLayouter.Infrastructer
{
    public interface ICloudLayouter
    {
        Rectangle PutNextRectangle(Size size);
    }
}