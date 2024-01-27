using System.Drawing;
using TagsCloudVisualization.PointCreators;

namespace TagsCloudVisualization.CloudLayouters;

public class CircularCloudLayouter : ICloudLayouter
{
    private readonly List<Rectangle> rectanglesInLayout;
    private readonly IEnumerator<Point> pointsOnSpiral;

    public CircularCloudLayouter(IPointCreator pointCreator)
    {
        rectanglesInLayout = new();
        pointsOnSpiral = pointCreator.GetPoints().GetEnumerator();
    }

    public IList<Rectangle> Rectangles { get => rectanglesInLayout; }

    public Rectangle PutNextRectangle(Size rectangleSize)
    {
        if (rectangleSize.Width <= 0 || rectangleSize.Height <= 0)
        {
            throw new ArgumentException("Rectangle width and height must be positive");
        }

        var currentRectangle = CreateNewRectangle(rectangleSize);

        rectanglesInLayout.Add(currentRectangle);

        return currentRectangle;
    }

    private Rectangle CreateNewRectangle(Size rectangleSize)
    {
        Rectangle rectangle;

        do
        {
            pointsOnSpiral.MoveNext();
            var rectangleLocation = GetLeftUpperCornerFromRectangleCenter(pointsOnSpiral.Current, rectangleSize);
            rectangle = new Rectangle(rectangleLocation, rectangleSize);
        }
        while (rectanglesInLayout.Any(rect => rect.IntersectsWith(rectangle)));

        return rectangle;
    }

    private Point GetLeftUpperCornerFromRectangleCenter(Point rectangleCenter, Size rectangleSize)
    {
        return rectangleCenter - rectangleSize / 2;
    }
}