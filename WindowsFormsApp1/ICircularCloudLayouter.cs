using System.Drawing;

namespace WindowsFormsApp1
{
    public interface ICircularCloudLayouter
    {
        Rectangle PutNextRectangle(Size size);
    }
}