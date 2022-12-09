using System.Drawing;
using FluentAssertions;
using TagsCloudVisualization;

namespace TagsCloudVisualizationTests;

public class RectangleExtensions_Should
{
    [TestCase(-50, -50, 100, 100)]
    [TestCase(499, 300, 50, 50)]
    [TestCase(450, 450, 200, 200)]
    public void WhenHasIntersection_ReturnsTrue(int locationX, int locationY, int width, int height)
    {
        var location = new Point(locationX, locationY);
        var size = new Size(width, height);

        var r1 = new Rectangle(location, size);
        ;

        var rectangles = new List<Rectangle>();
        rectangles.Add(new Rectangle(new Point(-500, -500), new Size(1000, 1000)));
        rectangles.Add(new Rectangle(new Point(500, 500), new Size(1000, 1000)));

        var actual = r1.CheckForIntersectionWithRectangles(rectangles);
        actual.Should().BeTrue();
    }

    [TestCase(500, 500, 100, 100)]
    public void WhenHasNoIntersection_ReturnsFalse(int locationX, int locationY, int width, int height)
    {
        var location = new Point(locationX, locationY);
        var size = new Size(width, height);

        var r1 = new Rectangle(location, size);
        ;

        var rectangles = new List<Rectangle>();
        rectangles.Add(new Rectangle(new Point(-500, -500), new Size(1000, 1000)));

        var actual = r1.CheckForIntersectionWithRectangles(rectangles);
        actual.Should().BeFalse();
    }
}