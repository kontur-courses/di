using System.Collections.Immutable;
using System.Drawing;
using QuadTrees;
using QuadTrees.Wrappers;
using TagCloud.Abstractions;

namespace TagCloud;

public class BaseCloudLayouter : ICloudLayouter
{
    private readonly IEnumerator<Point> pointEnumerator;
    private readonly QuadTreeRect<QuadTreeRectWrapper> rectanglesQuadTree;

    public BaseCloudLayouter(Point center, IPointGenerator pointGenerator)
    {
        Center = center;
        pointEnumerator = pointGenerator.Generate(center).GetEnumerator();
        rectanglesQuadTree = new QuadTreeRect<QuadTreeRectWrapper>(
            int.MinValue / 2 + center.X, int.MinValue / 2 + center.Y,
            int.MaxValue, int.MaxValue);
    }

    public Point Center { get; }

    public ImmutableArray<Rectangle> Rectangles => rectanglesQuadTree
        .GetAllObjects()
        .Select(rw => rw.Rect).ToImmutableArray();

    public Rectangle PutNextRectangle(Size rectangleSize)
    {
        if (!rectangleSize.IsPositive())
            throw new ArgumentException($"Width and height of the rectangle must be positive, but {rectangleSize}");

        var rectangle = CreateNewRectangle(rectangleSize);
        while (rectanglesQuadTree.GetObjects(rectangle).Any())
            rectangle = CreateNewRectangle(rectangleSize);

        rectanglesQuadTree.Add(new QuadTreeRectWrapper(rectangle));

        return rectangle;
    }

    private Rectangle CreateNewRectangle(Size size)
    {
        if (!pointEnumerator.MoveNext())
            throw new InvalidOperationException(
                "You are trying to put a new rectangle, but the points sequence has ended");

        var point = new Point(pointEnumerator.Current.X - size.Width / 2,
            pointEnumerator.Current.Y - size.Height / 2);
        return new Rectangle(point, size);
    }
}