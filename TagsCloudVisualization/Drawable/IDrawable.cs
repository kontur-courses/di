using System.Drawing;

namespace TagsCloudVisualization.Drawable
{
    public interface IDrawable
    {
        Rectangle Bounds { get; }
        void Draw(Graphics graphics);
    }
}