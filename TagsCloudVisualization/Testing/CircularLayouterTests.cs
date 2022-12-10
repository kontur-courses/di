using System.Drawing;
using FluentAssertions;
using TagsCloudVisualization;

namespace Testing;

[TestFixture]
class CircularLayouterTests
{
    private const string FailedTestBitmapDirectory = "..\\..\\..\\FailedResults";

    private const int
        BITMAP_WIDTH = 300,
        BITMAP_HEIGHT = 300;

    private CircularCloudLayouterSpiral _layouterSpiral;

    [SetUp]
    public void SetUp()
    {
        _layouterSpiral = new CircularCloudLayouterSpiral(new Point(BITMAP_WIDTH / 2, BITMAP_HEIGHT / 2));
    }

    [Test]
    public void Have_Zero_Rectangles_When_Created()
    {
        _layouterSpiral
            .Rectangles
            .Should()
            .HaveCount(0);
    }

    [TestCase(1)]
    [TestCase(10)]
    [TestCase(100)]
    public void Have_Some_Rectangles_After_They_Are_Added(int count)
    {
        GetRandomRectangles(count, new Size(1, 1), new Size(10, 10))
            .Should()
            .HaveCount(count);
    }

    [TestCase(2)]
    [TestCase(3)]
    [TestCase(10)]
    [TestCase(100)]
    public void Rectangles_Should_Not_Intersect(int count)
    {
        GetRandomRectangles(count, new Size(1, 1), new Size(10, 10))
            .SelectMany(
                (x, i) => _layouterSpiral.Rectangles.Skip(i + 1),
                (x, y) => Tuple.Create(x, y))
            .Any(t => t.Item1.IntersectsWith(t.Item2))
            .Should()
            .Be(false);
    }

    [TestCase(400)]
    [TestCase(500)]
    [TestCase(600)]
    public void Shape_Test_By_Areas_Compare(int count, float expectedDensity = 0.3f)
    {
        var rects = GetRandomRectangles(count, new Size(1, 1), new Size(10, 10));

        var approximatingCircleRadius = rects
            .SelectMany(r => new[]
            {
                new Point(r.Top, r.Left),
                new Point(r.Top, r.Right),
                new Point(r.Bottom, r.Left),
                new Point(r.Bottom, r.Right),
            })
            .Max(p => DistanceBetweenPoints(_layouterSpiral.Center, p));
        var approximatingCircleArea = Math.PI * approximatingCircleRadius * approximatingCircleRadius;

        var totalArea = rects
            .Select(r => r.Width * r.Height)
            .Sum();

        var density = totalArea / approximatingCircleArea;
        density
            .Should()
            .BeGreaterThan(expectedDensity);
    }

    [TearDown]
    public void TestIsFailed()
    {
        if (TestContext.CurrentContext.Result.FailCount != 0)
        {
            var bitmap = new Bitmap(BITMAP_WIDTH, BITMAP_HEIGHT);
            var g = Graphics.FromImage(bitmap);
            var pen = new Pen(Color.Black, 2);
            foreach (var rect in _layouterSpiral.Rectangles)
            {
                g.DrawRectangle(pen, rect);
            }

            var path = $"{FailedTestBitmapDirectory}\\{TestContext.CurrentContext.Test.Name}.bmp";
            Console.WriteLine($"Tag cloud visualization saved to file {Path.GetFullPath(path)}");
            bitmap.Save(path);
        }
    }

    #region helper methods

    private List<Rectangle> GetRectangles(int n, Size size)
    {
        var result = new List<Rectangle>();
        for (int i = 0; i < n; i++)
            result.Add(_layouterSpiral.PutNextRectangle(Size.Empty));
        return result;
    }

    private List<Rectangle> GetRandomRectangles(int n, Size minSize, Size maxSize)
    {
        var rand = new Random();
        var result = new List<Rectangle>();
        for (int i = 0; i < n; i++)
            result.Add(_layouterSpiral.PutNextRectangle(
                new Size(
                    rand.Next(minSize.Width, maxSize.Height),
                    rand.Next(minSize.Height, maxSize.Height)
                )
            ));
        return result;
    }

    public float DistanceBetweenPoints(Point a, Point b) =>
        (float)Math.Sqrt((a.X - b.X) * (a.X - b.X) + (a.Y - b.Y) * (a.Y - b.Y));

    #endregion
}