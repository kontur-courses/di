using System.Collections.Immutable;
using System.Drawing;
using QuadTrees;
using QuadTrees.Wrappers;

namespace TagCloud;

public class CircularCloudLayouter : ICloudLayouter
{
    private readonly QuadTreeRect<QuadTreeRectWrapper> rectanglesQuadTree;
    private readonly IEnumerator<Point> spiralEnumerator;

    public CircularCloudLayouter(Point center)
    {
        Center = center;
        spiralEnumerator = new SpiralPointGenerator(0.01, 0.01)
            .Generate(center).GetEnumerator();
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
        spiralEnumerator.MoveNext();
        var point = new Point(spiralEnumerator.Current.X - size.Width / 2,
            spiralEnumerator.Current.Y - size.Height / 2);
        return new Rectangle(point, size);
    }
}