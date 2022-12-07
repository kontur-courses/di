using System.Drawing;

namespace TagsCloudLayouter;

public class CircularCloudLayouter : ICloudLayouter
{
    public readonly Point Center;
    public double Density { get; }
    public double AngleStep { get; }
    public IReadOnlyList<Rectangle> Rectangles => rectangles;

    private readonly List<Rectangle> rectangles = new();
    private PolarPoint CurrentPosition { get; set; }
    private List<(PolarPoint, PolarPoint)> unusedRanges = new();

    public CircularCloudLayouter(Point center, double density = 0.01, double angleStep = 0.01)
    {
        Center = center;
        Density = density;
        AngleStep = angleStep;
    }

    public CircularCloudLayouter(SizeProperties sizeProperties) : this(sizeProperties.ImageCenter)
    {
    }

    public Rectangle PutNextRectangle(Size rectangleSize)
    {
        if (rectangleSize.Width <= 0 || rectangleSize.Height <= 0)
            throw new ArgumentException("Size can't be negative");
        foreach (var polarPoint in GetAllFreeRanges())
        {
            if (TryAddRectangle(polarPoint, rectangleSize, out var rectangle))
                return rectangle;
        }

        throw new Exception("There is no place for the rectangle");
    }

    public IEnumerable<Rectangle> PutRectangles(IEnumerable<Size> rectangleSizes) =>
        rectangleSizes.Select(PutNextRectangle);

    private bool TryAddRectangle(PolarPoint pointInPolar, Size rectangleSize, out Rectangle outRectangle)
    {
        var rectangleCenter = (Point)pointInPolar;
        var position = new Point(Center.X + rectangleCenter.X - rectangleSize.Width / 2,
            Center.Y + rectangleCenter.Y - rectangleSize.Height / 2);

        var rectangle = new Rectangle(position, rectangleSize);
        if (HasOverlapWith(rectangle))
        {
            outRectangle = new Rectangle();
            return false;
        }

        rectangles.Add(rectangle);
        outRectangle = rectangle;
        CurrentPosition = pointInPolar;
        RemoveOverlappedFromUnused();
        return true;
    }

    private IEnumerable<PolarPoint> GetAllFreeRanges()
    {
        return new List<IEnumerable<PolarPoint>>
        {
            unusedRanges.SelectMany(range =>
                GenerateArchimedeanSpiralRadius(range.Item1, range.Item2, 0, Density, AngleStep)),
            GenerateArchimedeanSpiralRadius(CurrentPosition, null, 0, Density, AngleStep)
        }.SelectMany(x => x);
    }

    private void RemoveOverlappedFromUnused()
    {
        var newRanges = new List<ValueTuple<PolarPoint, PolarPoint>>();
        foreach (var range in unusedRanges)
            RemoveOverlappedForRange(range, newRanges);
        unusedRanges = newRanges;
    }

    private void RemoveOverlappedForRange(ValueTuple<PolarPoint, PolarPoint> range,
        ICollection<ValueTuple<PolarPoint, PolarPoint>> ranges)
    {
        var isLastOverlapped = true;
        PolarPoint start = default, end = default;
        foreach (var polarPoint in GenerateArchimedeanSpiralRadius(range.Item1, range.Item2, 0, Density, AngleStep))
        {
            var point = (Point)polarPoint;
            var isOverlapped = HasOverlapWith(new Rectangle(point.X - 1, point.Y - 1, 3, 3));
            if (isOverlapped)
            {
                if (!isLastOverlapped)
                    ranges.Add((start, end));
            }
            else
            {
                if (isLastOverlapped)
                    start = polarPoint;
                else
                    end = polarPoint;
            }

            isLastOverlapped = isOverlapped;
        }
    }

    public bool HasOverlapWith(Rectangle rectangle)
    {
        foreach (var existingRectangle in rectangles)
        {
            if (existingRectangle.IntersectsWith(rectangle))
                return true;
        }

        return false;
    }

    private static IEnumerable<PolarPoint> GenerateArchimedeanSpiralRadius(PolarPoint start, PolarPoint? end,
        double offset, double density, double angleStep)
    {
        /** Archimedean Spiral  
         * Formula: r = a + b * θ,
         * a – move start by OX axis, 
         * b – curve density, 
         * θ – angle in polar system, 
         * r – radius in polar system
         */
        end ??= new PolarPoint(int.MaxValue / 2, 0);

        var nextAngle = start.Angle;
        var nextRadius = start.Radius;
        while (nextRadius <= end.Value.Radius)
        {
            yield return new PolarPoint(nextRadius, nextAngle);
            nextRadius = offset + density * nextAngle;
            nextAngle += angleStep;
        }
    }

    public void Clear()
    {
        rectangles.Clear();
        unusedRanges.Clear();
        CurrentPosition = new PolarPoint();
    }
}