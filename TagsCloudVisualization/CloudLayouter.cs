using System.Drawing;

namespace TagsCloudVisualization;

public class CloudLayouter
{
    private readonly IPointGenerator pointGenerator;
    private readonly List<Rectangle> createdRectangles = new();

    public IEnumerable<Rectangle> CreatedRectangles => createdRectangles.AsEnumerable();

    public CloudLayouter(IPointGenerator pointGenerator)
    {
        this.pointGenerator = pointGenerator;
    }

    public Rectangle PutNextRectangle(Size rectangleSize)
    {
        if (rectangleSize.Width < 0 || rectangleSize.Height < 0)
            throw new ArgumentException("Rectangle can't have negative width or height");

        while (true)
        {
            var nextPoint = pointGenerator.GetNextPoint();

            var rectangleLocation = new Point(nextPoint.X - rectangleSize.Width / 2,
                nextPoint.Y - rectangleSize.Height / 2);

            var newRectangle = new Rectangle(rectangleLocation, rectangleSize);
            if (createdRectangles.Any(rectangle => rectangle.IntersectsWith(newRectangle))) continue;

            createdRectangles.Add(newRectangle);
            return newRectangle;
        }
    }
}