using System.Drawing;

namespace TagsCloudVisualization.TagsCloudDrawer
{
    public interface IDrawable
    {
        void Draw(Graphics graphics);
        Rectangle Bounds { get; }
        IDrawable Shift(Size vector);
    }
}