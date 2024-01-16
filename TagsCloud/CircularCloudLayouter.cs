using System.Drawing;

namespace TagsCloud;

public class CircularCloudLayouter : ICircularCloudLayouter
{
    public List<Rectangle> Rectangles { get; }

    private readonly ISpiral spiral;

    public CircularCloudLayouter(ISpiral spiral)
    {
        this.spiral = spiral;
        Rectangles = new List<Rectangle>();
    }

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
        var point = spiral.GetPoint();
        var rectangles = new Rectangle(point, rectangleSize);
        while (!HasNoIntersections(rectangles))
        {
            point = spiral.GetPoint();
            rectangles = new Rectangle(point, rectangleSize);
        }

        return rectangles;
    }

    private bool HasNoIntersections(Rectangle rectangles)
    {
        for (var i = Rectangles.Count - 1; i >= 0; i--)
            if (Rectangles[i].IntersectsWith(rectangles))
                return false;
        return true;
    }
}