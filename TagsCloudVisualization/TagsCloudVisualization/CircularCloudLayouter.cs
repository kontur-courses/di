using System.Drawing;

namespace TagsCloudVisualization;

public abstract class CircularCloudLayouter
{
    protected Point _center;
    protected List<Rectangle> _rectangles;

    public List<Rectangle> Rectangles
    {
        get => _rectangles;
    }

    public CircularCloudLayouter(Point center)
    {
        _center = center;
        _rectangles = new();
    }

    public abstract Rectangle PutNextRectangle(Size rectangleSize);
}