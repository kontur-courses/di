using System.Drawing;
using TagsCloudContainer.Settings;

namespace TagsCloudContainer.CloudLayouters;

public class CircularCloudLayouter : ICloudLayouter
{
    private readonly List<Rectangle> rectangles;
    private Point center;
    private double spiralStep;
    private double angle;
    private const double DefaultAngleStep = Math.PI / 10;
    private const double DefaultSpiralStep = 1;
    private const double FullCircle = Math.PI * 2;
    private const int SpiralStepThreshold = 10;

    public CircularCloudLayouter(ImageSettings imageSettings)
    {
        this.center = new Point(imageSettings.ImageSize.Width / 2, imageSettings.ImageSize.Height / 2);
        rectangles = new List<Rectangle>();
        spiralStep = 1;
    }

    public IReadOnlyList<Rectangle> AddedRectangles => rectangles;

    public Rectangle PutNextRectangle(Size rectangleSize)
    {
        if (rectangleSize.Width == 0 || rectangleSize.Height == 0)
            throw new ArgumentException($"{nameof(rectangleSize)} should be with positive width and height");
        var location = GetPosition(rectangleSize);
        var rectangle = new Rectangle(location, rectangleSize);
        rectangles.Add(rectangle);
        return rectangle;
    }

    private Point GetPosition(Size rectangleSize)
    {
        if (rectangles.Count == 0)
        {
            center.Offset(new Point(rectangleSize / -2));
            return center;
        }

        return FindApproximatePosition(rectangleSize);
    }

    private Point FindApproximatePosition(Size rectangleSize)
    {
        var currentAngle = angle;
        while (true)
        {
            var candidateLocation = new Point(center.X + (int)(spiralStep * Math.Cos(currentAngle)),
                center.Y + (int)(spiralStep * Math.Sin(currentAngle)));
            var candidateRectangle = new Rectangle(candidateLocation, rectangleSize);

            if (!IntersectsWithAny(candidateRectangle))
            {
                rectangles.Add(candidateRectangle);
                angle = currentAngle;
                return candidateRectangle.Location;
            }

            currentAngle = CalculateAngle(currentAngle);
        }
    }

    private bool IntersectsWithAny(Rectangle candidateRectangle)
    {
        return rectangles
            .Any(candidateRectangle.IntersectsWith);
    }

    private double CalculateAngle(double currentAngle)
    {
        currentAngle += GetAngleStep();
        if (currentAngle > FullCircle)
        {
            currentAngle %= FullCircle;
            UpdateSpiral();
        }

        return currentAngle;
    }

    private void UpdateSpiral()
    {
        spiralStep += DefaultSpiralStep;
    }

    private double GetAngleStep()
    {
        var angleStep = DefaultAngleStep;
        var stepCount = (int)spiralStep / SpiralStepThreshold;
        if (stepCount > 0)
            angleStep /= stepCount;

        return angleStep;
    }
}