using System.Drawing;
using TagsCloudVisualization.CloudLayouter.PointGenerator;

namespace TagsCloudVisualization.CloudLayouter;

public class CircularCloudLayouter : ICloudLayouter
{
    private readonly List<Rectangle> rectangles = new();
    private readonly IPointGenerator pointGenerator;

    public CircularCloudLayouter(IPointGenerator pointGenerator)
    {
        this.pointGenerator = pointGenerator;
    }

    public Rectangle PutNextRectangle(Size rectangleSize)
    {
        if (rectangleSize.Height <= 0)
            throw new ArgumentException("Heigth should be positive");
        if (rectangleSize.Width <= 0)
            throw new ArgumentException("Width should be positive");

        while (true)
        {
            var point = pointGenerator.Next();
            var rectangle = new Rectangle(point, rectangleSize);
            if (!rectangle.IsIntersectWith(rectangles))
            {
                rectangles.Add(rectangle);
                return rectangle;
            }
        }
    }
}