using System.Drawing;

namespace TagsCloudVisualization;

public interface IDrawImage
{
    Rectangle Bounds { get; }
    void Draw(Graphics graphics);
    IDrawImage Offset(Size size);
}