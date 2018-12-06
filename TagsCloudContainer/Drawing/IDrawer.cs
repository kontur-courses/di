using System.Drawing;

namespace TagsCloudContainer.Drawing
{
    public interface IDrawer
    {
        int Width { get; }
        int Height { get; }
        void Draw(Graphics graphics);
    }
}
