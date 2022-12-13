using System.Collections.Concurrent;
using System.Drawing;
using System.Numerics;
using FluentAssertions;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using TagCloud;
using TagCloud.Abstractions;

namespace TagCloudTests;

[TestFixture]
[Parallelizable(ParallelScope.All)]
public class CircularCloudLayouterTests
{
    [SetUp]
    public void SetUp()
    {
        var layouter = new CircularCloudLayouter(new Point(0, 0));
        var testId = TestContext.CurrentContext.Test.ID;
        layouterByTestId[testId] = layouter;
    }

    [TearDown]
    public void TearDown()
    {
        if (TestContext.CurrentContext.Result.Outcome != ResultState.Failure) return;

        var testId = TestContext.CurrentContext.Test.ID;
        if (!layouterByTestId.ContainsKey(testId)) return;

        var filename = $"{TestContext.CurrentContext.Test.Name}.jpg";
        var directory = new DirectoryInfo("../../../FallingTestsImages");
        if (!directory.Exists) directory.Create();
        Console.WriteLine($"Tag cloud visualization saved to file {directory.FullName}\\{filename}");
    }

    private readonly ConcurrentDictionary<string, CircularCloudLayouter> layouterByTestId = new();

    [TestCase(0, 0)]
    [TestCase(10, 10)]
    [TestCase(10, -10)]
    [TestCase(-10, 10)]
    [TestCase(-10, -10)]
    public void Constructor_DontThrowException(int x, int y)
    {
        var point = new Point(x, y);

        var act = () => new CircularCloudLayouter(point);

        act.Should().NotThrow<Exception>();
    }

    [TestCase(0, 0, TestName = "{m}IsEmpty")]
    [TestCase(0, 10, TestName = "{m}WithZeroWidth")]
    [TestCase(10, 0, TestName = "{m}WithZeroHeight")]
    [TestCase(-10, 10, TestName = "{m}WithNegativeWidth")]
    [TestCase(10, -10, TestName = "{m}WithNegativeHeight")]
    public void PutNextRectangle_ThrowArgumentException_OnSize(int width, int height)
    {
        var layouter = layouterByTestId[TestContext.CurrentContext.Test.ID];
        var size = new Size(width, height);

        var act = () => layouter.PutNextRectangle(size);

        act.Should().Throw<ArgumentException>()
            .WithMessage($"Width and height of the rectangle must be positive, but {size}");
    }

    [TestCase(1, TestName = "{m}CorrectSize")]
    [TestCase(5, TestName = "{m}MultipleRectangles")]
    public void PutNextRectangle_DontThrowException_On(int rectanglesCount)
    {
        var layouter = layouterByTestId[TestContext.CurrentContext.Test.ID];
        var size = new Size(10, 10);

        var act = () =>
        {
            for (var i = 0; i < rectanglesCount; i++)
                layouter.PutNextRectangle(size);
        };

        act.Should().NotThrow<Exception>();
    }

    [Test]
    public void PutNextRectangle_CenterRectangle_HasCorrectParameters()
    {
        var layouter = layouterByTestId[TestContext.CurrentContext.Test.ID];
        var cloudCenter = layouter.Center;
        var size = new Size(10, 10);
        var expectedPosition = new Point(cloudCenter.X - size.Width / 2, cloudCenter.Y - size.Height / 2);
        var expectedRectangle = new Rectangle(expectedPosition, size);

        var rectangle = layouter.PutNextRectangle(size);

        rectangle.Should().BeEquivalentTo(expectedRectangle);
    }

    [TestCase(2)]
    [TestCase(1000)]
    public void PutNextRectangle_ReturnRectangles_DontIntersect(int rectanglesCount)
    {
        var layouter = layouterByTestId[TestContext.CurrentContext.Test.ID];
        var random = new Random();
        var sizes = Enumerable.Range(1, rectanglesCount)
            .Select(_ => new Size(random.Next(10, 31), random.Next(10, 31)));

        foreach (var size in sizes)
            layouter.PutNextRectangle(size);

        foreach (var rect1 in layouter.Rectangles)
        foreach (var rect2 in layouter.Rectangles.Where(r => r != rect1))
            rect1.IntersectsWith(rect2).Should().BeFalse();
    }

    [TestCase(1, 20)]
    [TestCase(100, 50)]
    [TestCase(250, 65)]
    [TestCase(500, 75)]
    [TestCase(1000, 80)]
    public void ResultRectanglesSet_LooksLikeCircle(int rectanglesCount, int expectedMinPercent)
    {
        var layouter = layouterByTestId[TestContext.CurrentContext.Test.ID];
        var random = new Random();
        var sizes = Enumerable.Range(1, rectanglesCount)
            .Select(_ => new Size(random.Next(10, 31), random.Next(10, 31)));
        foreach (var size in sizes)
            layouter.PutNextRectangle(size);

        var result = GetRadiusLengthRatioPercent(layouter);

        result.Should().BeGreaterOrEqualTo(expectedMinPercent);
    }

    private static int GetRadiusLengthRatioPercent(ICloudLayouter layouter)
    {
        var radiusLengths = layouter.Rectangles
            .SelectMany(AllPerimeterPoints)
            .GroupBy(Angle)
            .Select(g => g.Max(p => Distance(p, layouter.Center)))
            .ToArray();
        var minLength = radiusLengths.Min();
        var maxLength = radiusLengths.Max();
        return (int)Math.Round(minLength / maxLength * 100);
    }

    private static IEnumerable<Point> AllPerimeterPoints(Rectangle rect)
    {
        for (var i = Math.Min(rect.Left, rect.Right); i <= Math.Max(rect.Left, rect.Right); i++)
        for (var j = Math.Min(rect.Bottom, rect.Top); j <= Math.Max(rect.Bottom, rect.Top); j++)
            yield return new Point(i, j);
    }

    private static int Angle(Point point)
    {
        return (int)Math.Round(Math.Atan2(point.Y, point.X) * 180 / Math.PI);
    }

    private static double Distance(Point a, Point b)
    {
        return Vector2.Distance(new Vector2(a.X, a.Y), new Vector2(b.X, b.Y));
    }
}