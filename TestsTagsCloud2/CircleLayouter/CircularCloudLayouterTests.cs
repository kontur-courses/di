using System.Drawing;
using FluentAssertions;
using TagsCloud2.TagsCloudMaker.Layouter;

namespace TagsCloudTests;

public class CircularCloudLayouterTests
{
    [Test]
    public void PutNextRectangle_AdequacySameWidthAndHeigth()
    {
        var layouter = new CircularCloudLayouter(new Point(0, 0));
        var size = new Size(100, 25);
        var rectangle = layouter.PutNextRectangle(size);
        rectangle.Width.Should().Be(100);
        rectangle.Height.Should().Be(25);
    }
    
    [Test]
    public void PutNextRectangle_CloudDensity_RandomRectangles(){
        var layouter = new CircularCloudLayouter(new Point(0, 0));
        var rectangles = new List<Rectangle>();
        var pieces = 500;
        var rnd = new Random();
        for (var i = 0; i < pieces; i++)
        {
            var size = new Size(rnd.Next(10, 100), rnd.Next(10, 25));
            rectangles.Add(layouter.PutNextRectangle(size));
        }
        var allRectanglesArea = rectangles.Sum(rectangle => rectangle.Height * rectangle.Width);
        var maxDistance = CountMaxDistance(rectangles);

        var circleArea = Math.PI * maxDistance * maxDistance;
        var density = allRectanglesArea / circleArea;
        (density > 0.6).Should().Be(true);
    }
    
    
    [Test]
    public void PutNextRectangle_CloudDensity_500Squares()
    {
        var layouter = new CircularCloudLayouter(new Point(0, 0));
        var size = new Size(25, 25);
        var rectangles = new List<Rectangle>();
        var pieces = 500;
        for (var i = 0; i < pieces; i++)
        {
            rectangles.Add(layouter.PutNextRectangle(size));
        }
        var maxDistance = CountMaxDistance(rectangles);
        var allRectanglesArea = size.Height * size.Width * pieces;
        
        var circleArea = Math.PI * maxDistance * maxDistance;
        var density = allRectanglesArea / circleArea;
        (density > 0.5).Should().Be(true);
    }
    
    [Test]
    public void PutNextRectangle_CloudDensity_500RandomRectangles()
    {
        var layouter = new CircularCloudLayouter(new Point(0, 0));
        var rectangles = new List<Rectangle>();
        var pieces = 500;
        var rnd = new Random();
        for (var i = 0; i < pieces; i++)
        {
            var size = new Size(rnd.Next(10, 100), rnd.Next(10, 25));
            rectangles.Add(layouter.PutNextRectangle(size));
        }
        var allRectanglesArea = rectangles.Sum(rectangle => rectangle.Height * rectangle.Width);
        var maxDistance = CountMaxDistance(rectangles);

        var circleArea = CircleArea(maxDistance);
        var density = allRectanglesArea / circleArea;
        (density > 0.5).Should().Be(true);
    }

    private static double CircleArea(double maxDistance)
    {
        return Math.PI * maxDistance * maxDistance;
    }

    private double CountMaxDistance(List<Rectangle> rectangles)
    {
        var maxDistance = 0.0;
        foreach (var rectangle in rectangles)
        {
            var d1 = GetDistance(rectangle.Right, rectangle.Bottom);
            var d2 = GetDistance(rectangle.Right, rectangle.Top);
            var d3 = GetDistance(rectangle.Left, rectangle.Bottom);
            var d4 = GetDistance(rectangle.Left, rectangle.Top);
            var maxdi = MaxD(d1, d2, d3, d4);
            maxDistance = Math.Max(maxdi, maxDistance);
        }

        return maxDistance;
    }

    private double GetDistance(int x, int y)
    {
        return Math.Sqrt(x * x + y * y);
    }

    private double MaxD(double d1, double d2, double d3, double d4)
    {
        return Math.Max(Math.Max(d1, d2), Math.Max(d3, d4));
    }
}