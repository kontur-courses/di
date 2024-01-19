using System.Drawing;
using FluentAssertions;
using NUnit.Framework.Interfaces;
using TagCloud;

namespace TagCloudTests;

public class CircularCloudLayouterTests
{
    private const string RelativePathToFailDirectory = @"..\..\..\Fails";
    
    private CircularCloudLayouter layouter;
    private Point center;
    private ICloudDrawer drawer;

    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        drawer = TagCloudDrawer.Create(Path.GetFullPath(RelativePathToFailDirectory), new RandomColorSelector());
    }

    [SetUp]
    public void Setup()
    {
        center = new Point(0, 0);
        layouter = new CircularCloudLayouter(center);
    }

    [TearDown]
    public void Tear_Down()
    {
        if (TestContext.CurrentContext.Result.Outcome.Status == TestStatus.Failed)
            drawer.Draw(layouter.Rectangles, TestContext.CurrentContext.Test.FullName);
    }

    [Test]
    public void ReturnEmptyList_WhenCreated()
    {
        layouter.Rectangles.Should().BeEmpty();
    }

    [Test]
    public void ReturnOneElementList_WhenAddOne()
    {
        layouter.PutNextRectangle(new Size(1, 1));
        layouter.Rectangles.Count().Should().Be(1);
    }

    [Test]
    public void ReturnTwoElementList_WhenAddTwo()
    {
        layouter.PutNextRectangle(new Size(1, 1));
        layouter.PutNextRectangle(new Size(1, 1));
        layouter.Rectangles.Count().Should().Be(2);
        NotIntersectedAssert(layouter.Rectangles);
    }

    [TestCase(1, 1, 500, 0.77D, TestName = "WithSquareShape")]
    [TestCase(20, 10, 500, 0.67D, TestName = "WithRectangleShape")]
    public void AddManyNotIntersectedRectangles_WithConstantSize_ByCloudShape(int width, int height, int count, double accuracy)
    {
        var size = new Size(width, height);
        for (int i = 0; i < count; i++)
            layouter.PutNextRectangle(size);

        layouter.Rectangles.Count().Should().Be(count);
        NotIntersectedAssert(layouter.Rectangles);
        CircleShapeAssertion(layouter.Rectangles, accuracy);
    }

    [Test]
    public void AddManyNotIntersectedRectangles_WithVariableSize_ByCloudShape()
    {
        var rnd = new Random(DateTime.Now.Microsecond);

        for (int i = 0; i < 200; i++)
        {
            var size = new Size(rnd.Next(20, 40), rnd.Next(20, 40));
            layouter.PutNextRectangle(size);
        }

        layouter.Rectangles.Count().Should().Be(200);
        NotIntersectedAssert(layouter.Rectangles);
        CircleShapeAssertion(layouter.Rectangles, 0.6D);
    }

    [TestCase(-1, 1, TestName = "WithNegativeWidth")]
    [TestCase(1, -1, TestName = "WithNegativeHeight")]
    [TestCase(-1, -1, TestName = "WithNegativeWidthAndHeight")]
    public void Throw_ThenTryPutRectangle(int width, int height)
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

    public static void NotIntersectedAssert(IEnumerable<Rectangle> rectangles)
    {
        rectangles
            .HasIntersectedRectangles()
            .Should()
            .BeFalse();
    }
}