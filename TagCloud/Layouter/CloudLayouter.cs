using System.Drawing;
using TagCloud.PointGenerator;

namespace TagCloud.Layouter;

public class CloudLayouter : ILayouter
{
    private readonly IPointGenerator pointGenerator;
    public IList<Rectangle> Rectangles { get; }

    public CloudLayouter(IPointGenerator pointGenerator)
    {
        Rectangles = new List<Rectangle>();
        this.pointGenerator = pointGenerator;
    }

    public Rectangle PutNextRectangle(Size rectangleSize)
    {
        if (rectangleSize.Height <= 0 || rectangleSize.Width <= 0)
        {
            throw new ArgumentException("Rectangle size parameters should be positive");
        }

        var rectangle = new Rectangle(pointGenerator.GetNextPoint(), rectangleSize);
        while (!CanPlaceRectangle(rectangle))
        {
            rectangle = new Rectangle(pointGenerator.GetNextPoint() - rectangleSize / 2, rectangleSize);
        }

        Rectangles.Add(rectangle);

        return rectangle;
    }

    private bool CanPlaceRectangle(Rectangle newRectangle)
    {
        return !Rectangles.Any(rectangle => rectangle.IntersectsWith(newRectangle));
    }
}