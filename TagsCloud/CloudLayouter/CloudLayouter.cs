using System.Drawing;

namespace TagsCloud.CloudLayouter;

public class CloudLayouter : ICloudLayouter
{
    private readonly IEnumerator<Point> iterator;

    public CloudLayouter(ISpiral spiral, Point center)
    {
        iterator = spiral.GetPoints(center).GetEnumerator();
        Rectangles = new List<Rectangle>();
    }

    public List<Rectangle> Rectangles { get; }

    public Rectangle PutNextRectangle(Size rectangleSize)
    {
        if (rectangleSize.Height <= 0 || rectangleSize.Width <= 0)
            throw new ArgumentException("Sides of the rectangle should not be non-positive");
        var rectangles = CreateNextRectangle(rectangleSize);
        Rectangles.Add(rectangles);
        return rectangles;
    }

    private Rectangle CreateNextRectangle(Size rectangleSize)
    {
        iterator.MoveNext();
        var rectangle = new Rectangle(iterator.Current, rectangleSize);
        while (!HasNoIntersections(rectangle))
        {
            iterator.MoveNext();
            rectangle = new Rectangle(iterator.Current, rectangleSize);
        }

        return rectangle;
    }

    private bool HasNoIntersections(Rectangle rectangles)
    {
        for (var i = Rectangles.Count - 1; i >= 0; i--)
            if (Rectangles[i].IntersectsWith(rectangles))
                return false;
        return true;
    }
}