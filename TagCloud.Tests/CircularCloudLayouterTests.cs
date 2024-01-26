using System.Reflection;
using Aspose.Drawing;
using NUnit.Framework.Interfaces;
using TagCloud.Domain.Layout;
using TagCloud.Domain.Layouter;
using TagCloud.Domain.Settings;
using TagCloud.Tests.Extensions;

namespace TagCloud.Tests;

[TestFixture]
public class CircularCloudLayouterTests
{
    [SetUp]
    public void SetUp()
    {
        layouter = new CircularCloudLayouter(new LayoutSettings(), new Layout());
    }
    
    private CircularCloudLayouter layouter;
    
    [TestCase(0, 0, TestName = "ZeroDimensions")]
    [TestCase(-1, -1, TestName = "NegativeDimensions")]
    public void PutNextRectangle_ThrowsArgumentExceptionOn(int width, int height)
    {
        new Action(() => layouter.PutNextRectangle(new Size(width, height)))
            .Should()
            .ThrowExactly<ArgumentException>()
            .Where(e => e.Message.Equals("Width and height of rectangle must be positive"));
    }

    [Test]
    public void PutNextRectangle_RectanglesShouldNotIntersect()
    {
        var rectangles = AddRectangles(layouter).ToArray();
        
        rectangles
            .Where(r1 => rectangles.Where(r2 => r2 != r1).Any(r1.IntersectsWith))
            .Should()
            .BeEmpty();
    }

    [Test, Timeout(500)]
    public void PutNextRectangle_MustBeEfficient()
    {
        AddRectangles(layouter).Count();
    }
    
    [TestCase(1, TestName = "WhenOneRectangleGiven")]
    [TestCase(6, TestName = "WhenMultipleRectanglesGiven")]
    public void HasCenterInInitCoords(int rectanglesToAdd)
    {
        for (var i = 0; i < rectanglesToAdd; i++)
            layouter.PutNextRectangle(new Size(1, 1));

        layouter.Center
            .Should()
            .Be(new Point(400, 400));
    }
    
    [Test]
    public void CanPutRectangle_ReturnsFalseOnPuttingRectangleToOccupiedPlace()
    {
        var type = layouter.GetType();
        
        type.GetAndInvokeMethod(
            "PutRectangle", 
            BindingFlags.NonPublic | BindingFlags.Instance,
            layouter, 
            new Rectangle(0, 0, 1, 1));
        
        type.GetAndInvokeMethod(
                "CanPutRectangle", 
                BindingFlags.NonPublic | BindingFlags.Instance,
                layouter, 
                new Rectangle(0, 0, 1, 1))
            .Should()
            .Be(false);
    }
    
    [Test]
    public void CanPutRectangle_ReturnsTrueOnPuttingRectangleToEmptyPlace()
    {
        var type = layouter.GetType();
        
        type.GetAndInvokeMethod(
            "PutRectangle", 
            BindingFlags.NonPublic | BindingFlags.Instance,
            layouter, 
            new Rectangle(0, 0, 1, 1));
        
        type.GetAndInvokeMethod(
                "CanPutRectangle", 
                BindingFlags.NonPublic | BindingFlags.Instance,
                layouter, 
                new Rectangle(2, 2, 1, 1))
            .Should()
            .Be(true);
    }
    
    private static IEnumerable<Rectangle> AddRectangles(CircularCloudLayouter layouter, int count = 100)
    {
        var rnd = new Random(228_666);
        
        for (var i = 0; i < count; i++) 
            yield return layouter.PutNextRectangle(GetNextRandomSize(rnd));
    }

    private static Size GetNextRandomSize(Random rnd)
    {
        return new Size(rnd.Next(1, 10), rnd.Next(1, 10));
    }
}