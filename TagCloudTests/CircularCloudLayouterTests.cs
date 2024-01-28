using System.Drawing;
using TagCloud;
using TagCloud.Extensions;

namespace TagCloudTests;

public class CircularCloudLayouterTests
{
    private CircularCloudLayouter layouter;
    private ICloudDrawer drawer;
    private Point center;

    [SetUp]
    public void Setup()
    {
        center = new Point(0, 0);
        layouter = new CircularCloudLayouter(SpiralCloudShaper.Create(center));
    }

    [Test]
    public void Rectangles_ReturnEmptyList_WhenCreated()
    {
        layouter.Rectangles.Should().BeEmpty();
    }

    [Test]
    public void Rectangles_ReturnOneElementList_WhenAddOne()
    {
        layouter.PutNextRectangle(new Size(1, 1));
        layouter.Rectangles.Count().Should().Be(1);
    }

    [Test]
    public void Rectangles_ReturnTwoElementList_WhenAddTwo()
    {
        layouter.PutNextRectangle(new Size(1, 1));
        layouter.PutNextRectangle(new Size(1, 1));
        layouter.Rectangles.Count().Should().Be(2);
        NotIntersectedAssertion(layouter.Rectangles);
    }
    
    [TestCase(1, 1, 500, 0.77D, TestName = "WithSquareShape")]
    [TestCase(20, 10, 500, 0.67D, TestName = "WithRectangleShape")]
    public void Layouter_ShouldLocateConstantSizeRectangles_ByCircleShapeAndWithoutIntersection(int width, int height, int count, double accuracy)
    {
        var size = new Size(width, height);
        for (int i = 0; i < count; i++)
            layouter.PutNextRectangle(size);

        layouter.Rectangles.Count().Should().Be(count);
        NotIntersectedAssertion(layouter.Rectangles);
        CircleShapeAssertion(layouter.Rectangles, accuracy);
    }

    [Test]
    public void Layouter_ShouldLocateVariableSizeRectangles_ByCircleShapeAndWithoutIntersection()
    {
        var rnd = new Random(DateTime.Now.Microsecond);

        for (int i = 0; i < 200; i++)
        {
            var size = new Size(rnd.Next(20, 40), rnd.Next(20, 40));
            layouter.PutNextRectangle(size);
        }

        layouter.Rectangles.Count().Should().Be(200);
        NotIntersectedAssertion(layouter.Rectangles);
        CircleShapeAssertion(layouter.Rectangles, 0.6D);
    }

    [TestCase(-1, 1, TestName = "WithNegativeWidth")]
    [TestCase(1, -1, TestName = "WithNegativeHeight")]
    [TestCase(-1, -1, TestName = "WithNegativeWidthAndHeight")]
    public void PutNextRectangle_ShouldThrow_ThenTryPutRectangle(int width, int height)
    {
        Assert.Throws<ArgumentException>(() => layouter.PutNextRectangle(new Size(width, height)));
    }

    private void CircleShapeAssertion(IEnumerable<Rectangle> rectangles, double accuracy)
    {
        var circleRadius = rectangles
            .SelectMany(rect => new[]
            {
                rect.Location,
                rect.Location with { X = rect.Right },
                rect.Location with { Y = rect.Bottom },
                rect.Location with { X = rect.Right, Y = rect.Bottom }
            })
            .Max(point => point.GetDistanceTo(center));
        Console.WriteLine(circleRadius);
        var containingCircleRadius = Math.PI * circleRadius * circleRadius;
        var rectanglesTotalArea = rectangles.Sum(rect => rect.Width * rect.Height);
        Assert.GreaterOrEqual(rectanglesTotalArea / containingCircleRadius, accuracy);
    }

    public static void NotIntersectedAssertion(IEnumerable<Rectangle> rectangles)
    {
        rectangles
            .HasIntersectedRectangles()
            .Should()
            .BeFalse();
    }
}