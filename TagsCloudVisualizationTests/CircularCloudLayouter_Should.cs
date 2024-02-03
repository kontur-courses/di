using System.Drawing;
using FluentAssertions;
using TagsCloudVisualization;

namespace TagsCloudVisualizationTests;

public class CircularCloudLayouter_Should
{
    private CircularCloudLayouter cloud;

    private double expectedAreaRatio = 1.7;
    private Point center;
    private List<Rectangle> rectangles;
    private TestContext TestContext { get; set; }

    [SetUp]
    public void Setup()
    {
        center = new Point(250, 250);
        cloud = new CircularCloudLayouter(new SpiralPoints(center));
        rectangles = new List<Rectangle>();
    }

    [Test]
    public void PutNextRectangle_OnCenter_AfterFirstPut()
    {
        var rectangleSize = new Size(100, 50);
        var expectedRectangle = new Rectangle(center - new Size(50, 25), rectangleSize);
        var rectangle = cloud.PutNextRectangle(rectangleSize);
        rectangles.Add(rectangle);
        rectangle.Should().BeEquivalentTo(expectedRectangle);
    }

    [Test]
    public void PutNextRectangle_NoIntersects_AfterPutting()
    {
        var rectangleSize = new Size(10, 50);

        rectangles = new List<Rectangle>();
        for (var i = 0; i < 100; i++)
        {
            rectangles.Add(cloud.PutNextRectangle(rectangleSize));
        }

        foreach (var rectangle in rectangles)
        {
            foreach (var secondRectangle in rectangles.Where(x => x != rectangle))
            {
                rectangle.IntersectsWith(secondRectangle).Should().BeFalse();
            }
        }
    }
    
    private static double GetDistance(Point start, Point end) =>
        Math.Sqrt(Math.Pow(start.X - end.X, 2) + Math.Pow(start.Y - end.Y, 2));
    
    [TestCase(0, 0, 0, 0, 0)]
    [TestCase(1, 0, 0, 0, 1)]
    [TestCase(0, 1, 0, 0, 1)]
    [TestCase(0, 0, 1, 0, 1)]
    [TestCase(0, 0, 0, 1, 1)]
    [TestCase(3, 4, 0, 0, 5)]
    [TestCase(1, 2, -2, -2, 5)]
    [TestCase(1, 0, -2, 0, 3)]
    [TestCase(1, 0, 1, 0, 0)]
    [TestCase(1, 0, 2, 0, 1)]
    [TestCase(0, 1, 0, -2, 3)]
    [TestCase(0, 1, 0, 1, 0)]
    [TestCase(0, 1, 0, 2, 1)]
    public void CalculateDistance(int x1, int y1, int x2, int y2, double expected)
    {
        GetDistance(new Point(x1, y1), new Point(x2, y2)).Should().Be(expected);
    }

    [Test]
    public void PutNextRectangle_LikeCircle_AfterPutting()
    {
        var cloudSquare = 0;
        var circleRadius = 0.0;

        var random = new Random();
        for (var i = 0; i < 100; i++)
        {
            var rectangle = cloud.PutNextRectangle(new Size(random.Next(10, 50), random.Next(10, 50)));
            cloudSquare += rectangle.Height * rectangle.Width;
            circleRadius = new[]
            {
                circleRadius,
                GetDistance(center, new Point(rectangle.Left, rectangle.Top)),
                GetDistance(center, new Point(rectangle.Right, rectangle.Top)),
                GetDistance(center, new Point(rectangle.Right, rectangle.Bottom)),
                GetDistance(center, new Point(rectangle.Left, rectangle.Bottom))
            }.Max();
            rectangles.Add(rectangle);
        }

        var circleSquare = Math.PI * circleRadius * circleRadius;
        (circleSquare / cloudSquare).Should().BeLessThan(expectedAreaRatio);
    }
}