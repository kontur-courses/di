using System.Drawing;

namespace CloudLayout.Interfaces
{
    public interface ILayout
    {
        bool PutNextRectangle(SizeF size, out RectangleF rectangle);
    }
}