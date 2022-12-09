using System.Drawing;

namespace TagCloud.LayoutAlgorithm.CircularLayoutAlgorithm;

public class CircularLayoutAlgorithm : ILayoutAlgorithm
{
    private readonly Point center;
    private readonly FermatSpiral fermatSpiral;
    private readonly List<Rectangle> rectangles = new();  

    public CircularLayoutAlgorithm(Point center)
    {
        this.center = center;
        fermatSpiral = new FermatSpiral(center);
    }
    
    public Rectangle PutNextRectangle(Size rectangleSize)
    {
        if (rectangleSize.Width < 0 || rectangleSize.Height < 0)
            throw new ArgumentException($"Rectangle size should not be negative. Received Width: {rectangleSize.Width} and Height: {rectangleSize.Height}");

        Rectangle result;

        do
        {
            var rectangleCenter = fermatSpiral.GetNextPoint();
            var anchorPoint = GetAnchorPoint(rectangleCenter, rectangleSize);
            result = new Rectangle(anchorPoint, rectangleSize);
        } while (rectangles.Any(r => r.IntersectsWith(result)));
        
        if (rectangles.Count > 0)
            result = ShiftRectangleToCenter(result);
        rectangles.Add(result);

        return result;
    }

    private Rectangle ShiftRectangleToCenter(Rectangle rectangle)
    {
        var deltaX = rectangle.X > center.X ? -1 : 1;
        var deltaY = rectangle.Y > center.Y ? -1 : 1;

        rectangle = ShiftRectangleToCenterWithDelta(rectangle, new Point(deltaX, 0));
        rectangle = ShiftRectangleToCenterWithDelta(rectangle, new Point(0, deltaY));

        return rectangle;
    }

    private Rectangle ShiftRectangleToCenterWithDelta(Rectangle rectangle, Point delta)
    {
        var previousAnchor = new Point(rectangle.X, rectangle.Y);
        while (!rectangles.Any(r => r.IntersectsWith(rectangle)) && rectangle.X != center.X && rectangle.Y != center.Y)
        {
            previousAnchor = new Point(rectangle.X, rectangle.Y);
            var anchor = new Point(rectangle.X + delta.X, rectangle.Y + delta.Y);
            rectangle = new Rectangle(anchor, rectangle.Size);
        }
        rectangle = new Rectangle(previousAnchor, rectangle.Size);

        return rectangle;
    }

    private Point GetAnchorPoint(Point rectangleCenter, Size rectangleSize)
    {
        return new Point(rectangleCenter.X - rectangleSize.Width / 2, rectangleCenter.Y - rectangleSize.Height / 2);
    }
}