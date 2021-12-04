using System.Drawing;

namespace TagsCloudDrawer
{
    public interface IDrawable
    {
        void Draw(Graphics graphics);
        Rectangle Bounds { get; }
        IDrawable Shift(Size vector);
    }
}