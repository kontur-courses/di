using System.Drawing;

namespace TagsCloudVisualization.Drawer;

public interface IDrawImage
{
    Rectangle Bounds { get; }
    void Draw(Graphics graphics);
    IDrawImage Offset(Size size);
}