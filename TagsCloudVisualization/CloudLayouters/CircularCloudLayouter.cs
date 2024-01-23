using System.Drawing;
using TagsCloudVisualization.PointCreators;
using TagsCloudVisualization.Settings;

namespace TagsCloudVisualization.CloudLayouters;

public class CircularCloudLayouter : ICloudLayouter
{
    private readonly Point center;
    private readonly List<Rectangle> rectanglesInLayout;
    private readonly IEnumerator<Point> pointsOnSpiral;

    public CircularCloudLayouter(ImageSettings imageSettings, IPointCreator pointCreator)
    {
        center = new Point(imageSettings.Width / 2, imageSettings.Height / 2);
        rectanglesInLayout = new();
        pointsOnSpiral = pointCreator.GetPoints().GetEnumerator();
    }

    public Point CloudCenter { get => center; }
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
