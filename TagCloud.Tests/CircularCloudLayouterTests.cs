using System.Drawing;
using NUnit.Framework;
using TagCloud.LayoutAlgorithm.CircularLayoutAlgorithm;

namespace TagCloud.Tests;

[TestFixture]
[Parallelizable(ParallelScope.Children)]
public class CircularCloudLayouterTests
{
    private static readonly Size CanvasSize = new Size(250, 250);
    private static readonly Point Center = new Point(CanvasSize.Width / 2, CanvasSize.Height / 2);
    
    [TestCase(0, 0, Description = "Zero coordinates")]
    [TestCase(1, 1, Description = "Positive coordinates")]
    [TestCase(-1, -1, Description = "Negative coordinates")]
    public void Constructor_ShouldAcceptAnyPoint(int x, int y)
    {
        Assert.DoesNotThrow(() => new CircularLayoutAlgorithm(new Point(x, y)));
    }

    [TestCase(-1, 0)]
    [TestCase(0, -1)]
    [TestCase(-1, -1)]
    public void PutNextRectangle_ShouldThrowArgumentException_OnNegativeSize(int width, int height)
    {
        var layouter = new CircularLayoutAlgorithm(Center);
        var size = new Size(-1, -1);
        Assert.Throws<ArgumentException>(() => layouter.PutNextRectangle(size));
    }

    [Test]
    public void PutNextRectangle_ShouldPlaceFirstRectangleInCenter()
    {
        var layouter = new CircularLayoutAlgorithm(Center);
        var expectedSize = new Size(10, 10);
        var expectedLocation = new Point(Center.X - expectedSize.Width / 2, Center.Y - expectedSize.Height / 2);
        var expectedRectangle = new Rectangle(expectedLocation, expectedSize);
        
        var actualRectangle = layouter.PutNextRectangle(expectedSize);
        
        Assert.That(actualRectangle, Is.EqualTo(expectedRectangle));
    }
    
    [Test(Description = "A thousand rectangles should not intersect")]
    public void PutNextRectangle_RectanglesShouldNotIntersect_WithSameRectangles()
    {
        var sizeSelector = () => new Size(10, 10);
        AssertRectanglesDontIntersect(1000, sizeSelector);
    }
    
    [Test(Description = "A thousand rectangles should not intersect")]
    public void PutNextRectangle_RectanglesShouldNotIntersect_WithRandomRectangles()
    {
        var random = new Random(123);
        var sizeSelector = () => new Size(random.Next(1, 11), random.Next(1, 11));
        AssertRectanglesDontIntersect(1000, sizeSelector);
    }
    
    [TestCase(Description = "A thousand rectangles should create a circle")]
    public void PutNextRectangle_ShouldCreateATightCircleAroundCenter_WithSameRectangles()
    {
        var sizeSelector = () => new Size(10, 10);
        AssertShapeIsATightCircleAroundCenter(1000, sizeSelector);
    }
    
    [TestCase(Description = "A thousand rectangles should create a circle")]
    public void PutNextRectangle_ShouldCreateATightCircleAroundCenter_WithRandomRectangles()
    {
        var random = new Random(123);
        var sizeSelector = () => new Size(random.Next(1, 11), random.Next(1, 11));

        AssertShapeIsATightCircleAroundCenter(1000, sizeSelector);
    }

    private void AssertRectanglesDontIntersect(int count, Func<Size> rectangleSizeSelector)
    {
        var layouter = new CircularLayoutAlgorithm(Center);

        var checkedRectangles = new List<Rectangle>();
        for (var i = 0; i < count; i++)
        {
            var rectangle = layouter.PutNextRectangle(rectangleSizeSelector());
            if (checkedRectangles.Any(r => r.IntersectsWith(rectangle)))
                Assert.Fail($"Rectangles should not intersect, failed on iteration {i + 1}");
            checkedRectangles.Add(rectangle);
        }
    }
    
    private void AssertShapeIsATightCircleAroundCenter(int count, Func<Size> rectangleSizeSelector)
    { 
        var layouter = new CircularLayoutAlgorithm(Center);
        
        var layout = new List<Rectangle>();
        for (var i = 0; i < count; i++)
            layout.Add(layouter.PutNextRectangle(rectangleSizeSelector()));

        var areaCoveredByRectangles = GetAreaCoveredByRectangles(layout);
        var (upperRadius, lowerRadius, rightRadius, leftRadius) = GetRadii(layout);

        var verticalDiameter = upperRadius + lowerRadius;
        var horizontalDiameter = rightRadius + leftRadius;
        
        var boundingBoxArea = horizontalDiameter > verticalDiameter
            ? horizontalDiameter * horizontalDiameter
            : verticalDiameter * verticalDiameter;
        var boundingCircleArea = boundingBoxArea * Math.PI / 4d;
        Assert.Multiple(() =>
        {
            Assert.That(Math.Abs(upperRadius - lowerRadius), Is.LessThanOrEqualTo(10));
            Assert.That(Math.Abs(rightRadius - leftRadius), Is.LessThanOrEqualTo(10));
            Assert.That(Math.Abs(verticalDiameter - horizontalDiameter), Is.LessThanOrEqualTo(10));
            Assert.That(areaCoveredByRectangles / (double)boundingBoxArea - Math.PI / 4d, Is.LessThanOrEqualTo(0.01));
            Assert.That(areaCoveredByRectangles / boundingCircleArea, Is.GreaterThanOrEqualTo(0.8));
        });
    }

    private int GetAreaCoveredByRectangles(List<Rectangle> layout)
    {
        return layout.Sum(rectangle => rectangle.Width * rectangle.Height);
    }

    private (int upper, int lower, int right, int left) GetRadii(List<Rectangle> layout)
    {
        var maxX = int.MinValue;
        var maxY = int.MinValue;
        var minX = int.MaxValue;
        var minY = int.MaxValue;
        foreach (var rectangle in layout)
        {
            if (rectangle.Bottom > maxY) maxY = rectangle.Bottom;
            if (rectangle.Top < minY) minY = rectangle.Top;
            if (rectangle.Right > maxX) maxX = rectangle.Right;
            if (rectangle.Bottom < minX) minX = rectangle.Left;
        }
        var upperRadius = Center.Y - minY;
        var lowerRadius = maxY - Center.Y;
        var rightRadius = maxX - Center.X;
        var leftRadius = Center.X - minX;
        
        return (upperRadius, lowerRadius, rightRadius, leftRadius);
    }
}