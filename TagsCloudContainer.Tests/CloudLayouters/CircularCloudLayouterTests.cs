using System.Drawing;
using FluentAssertions;
using NUnit.Framework.Internal;
using TagsCloudContainer.CloudLayouters;
using TagsCloudContainer.Settings;

namespace TagsCloudContainer.Tests.CloudLayouters;

[TestFixture]
[TestOf(typeof(CircularCloudLayouter))]
public class CircularCloudLayouterTests
{
    private Point center;
    private CircularCloudLayouter sut;
    private ImageSettings imageSettings;

    private Randomizer Random => TestContext.CurrentContext.Random;

    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        imageSettings = new ImageSettings();
        center = new Point(imageSettings.ImageSize.Width / 2, imageSettings.ImageSize.Height / 2);
    }

    [SetUp]
    public void SetUp()
    {
        sut = new CircularCloudLayouter(imageSettings);
    }

    [Test]
    public void PutNextRectangle_ShouldThrow_WhenSizeNotPositive()
    {
        var action = () => sut.PutNextRectangle(new Size(0, 0));

        action.Should().Throw<ArgumentException>();
    }

    [Test]
    public void PutNextRectangle_ShouldReturnRectangleWithGivenSize()
    {
        var size = new Size(10, 10);

        var rectangle = sut.PutNextRectangle(size);

        rectangle.Size.Should().Be(size);
    }

    [Test]
    public void PutNextRectangle_ShouldPlaceFirstRectangleInCenter()
    {
        var size = new Size(50, 50);
        var offsetCenter = center - size / 2;

        var rectangle = sut.PutNextRectangle(size);

        rectangle.Location.Should().Be(offsetCenter);
    }

    [Test]
    public void PutNextRectangle_ShouldReturnNotIntersectingRectangles()
    {
        var sizes = GetRandomSizes(70);

        var actualRectangles = sizes.Select(x => sut.PutNextRectangle(x)).ToList();

        for (var i = 0; i < actualRectangles.Count - 1; i++)
        {
            for (var j = i + 1; j < actualRectangles.Count; j++)
            {
                actualRectangles[i].IntersectsWith(actualRectangles[j]).Should().BeFalse();
            }
        }
    }

    [Test]
    public void PutNextRectangle_ShouldCreateLayoutCloseToCircle()
    {
        var sizes = GetRandomSizes(50);

        var rectangles = sizes.Select(size => sut.PutNextRectangle(size)).ToList();
        var rectanglesSquare = rectangles
            .Select(x => x.Width * x.Height)
            .Sum();
        var circleSquare = CalculateBoundingCircleSquare(rectangles);

        rectanglesSquare.Should().BeInRange((int)(circleSquare * 0.7), (int)(circleSquare * 1.3));
    }

    private Size GetRandomSize()
    {
        //return new Size(Random.Next(30, 40), Random.Next(30, 40));
        return new Size(Random.Next(100, 120), Random.Next(30, 90));
    }

    private IEnumerable<Size> GetRandomSizes(int count)
    {
        var sizes = new List<Size>(count);
        for (var i = 0; i < count; i++)
            sizes.Add(GetRandomSize());
        return sizes;
    }

    private double CalculateBoundingCircleSquare(List<Rectangle> rectangles)
    {
        var rect = rectangles
            .Where(x => x.Contains(x.X, center.Y))
            .MaxBy(x => Math.Abs(x.X - center.X));
        var width = Math.Abs(rect.X - center.X);

        rect = rectangles
            .Where(x => x.Contains(center.X, x.Y))
            .MaxBy(x => Math.Abs(x.Y - center.Y));
        var height = Math.Abs(rect.Y - center.Y);

        return Math.Max(width, height) * Math.Max(width, height) * Math.PI;
    }
}