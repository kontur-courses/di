using System.Drawing;

namespace TagsCloud.App
{
    public interface IRectanglesLayouter
    {
        string Name { get; }
        int MaxX { get; }
        int MinX { get; }
        int MaxY { get; }
        int MinY { get; }
        int Width { get; }
        int Height { get; }
        Rectangle PutNextRectangle(Size rectangleSize);
        void Clear();
    }
}
