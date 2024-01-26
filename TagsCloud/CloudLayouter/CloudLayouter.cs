using System.Drawing;

namespace TagsCloud.CloudLayouter;

public class CloudLayouter : ICloudLayouter
{
    private readonly ISpiral spiral;

    public CloudLayouter(ISpiral spiral)
    {
        this.spiral = spiral;
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
        var point = spiral.GetPoint();
        var rectangle = new Rectangle(point, rectangleSize);
        while (!HasNoIntersections(rectangle))
        {
            point = spiral.GetPoint();
            rectangle = new Rectangle(point, rectangleSize);
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