using System.Drawing;

namespace TagsCloudVisualization;

public class RectangleLayouter : IRectangleLayouter
{
    private readonly IPointGenerator pointGenerator;
    private readonly List<Rectangle> createdRectangles = new();

    public RectangleLayouter(TagLayoutSettings tagLayoutSettings, IEnumerable<IPointGenerator> pointGenerators)
    {
        pointGenerator = pointGenerators.First(generator => generator.Name == tagLayoutSettings.Algorithm);
    }

    public Rectangle PutNextRectangle(Size rectangleSize)
    {
        if (rectangleSize.Height < 0 || rectangleSize.Width < 0)
            throw new ArgumentException("Rectangle cant have less than 0 width or height");
        
        while (true)
        {
            var nextPoint = pointGenerator.GetNextPoint();

            var rectangleLocation = new Point(nextPoint.X - rectangleSize.Width / 2,
                nextPoint.Y - rectangleSize.Height / 2);

            var newRectangle = new Rectangle(rectangleLocation, rectangleSize);
            if (createdRectangles.Any(rectangle => rectangle.IntersectsWith(newRectangle)))
                continue;

            createdRectangles.Add(newRectangle);
            return newRectangle;
        }
    }

    public Rectangle PutNextRectangle(SizeF rectangleSize)
    {
        var intWidth = (int)Math.Round(rectangleSize.Width);
        var intHeight = (int)Math.Round(rectangleSize.Height);
        var intSize = new Size(intWidth, intHeight);

        return PutNextRectangle(intSize);
    }
}