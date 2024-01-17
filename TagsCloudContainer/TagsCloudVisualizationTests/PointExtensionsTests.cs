using TagsCloudVisualization.Extensions;

namespace TagsCloudVisualizationTests;

public class PointExtensionsTests
{
    [Test]
    [Repeat(25)]
    public void WithOffset_ReturnsPoint_WithCorrectOffset()
    {
        var random = new Random();
        var offset = new Size(random.Next(-100, 100), random.Next(-100, 100));
        var originalPoint = new Point(random.Next(-100, 100), random.Next(-100, 100));
        var actual = originalPoint.WithOffset(offset);
        var expected = new Point(originalPoint.X + offset.Width, originalPoint.Y + offset.Height);
            
        actual.Should().BeEquivalentTo(expected, "original point is "+ $"{originalPoint}" + " and offset is " + $"{offset}");
    }
    
    [Test]
    [Repeat(25)]
    public void DistanceTo_CalculateDistanceCorrectly()
    {
        var random = new Random();
        var point1 = new Point(random.Next(-100, 100), random.Next(-100, 100));
        var point2 = new Point(random.Next(-100, 100), random.Next(-100, 100));
        var actual = point1.DistanceTo(point2);
        var expected = Math.Sqrt(Math.Pow(point2.X - point1.X, 2) + Math.Pow(point2.Y - point1.Y, 2));
            
        actual.Should().BeApproximately(expected, 0.0003);
    }
}
