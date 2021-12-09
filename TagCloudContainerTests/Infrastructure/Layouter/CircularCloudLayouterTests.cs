using System;
using System.Drawing;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using TagCloud.Infrastructure.Layouter;

namespace TagCloudTests.Infrastructure.Layouter;

public class CircularCloudLayouterTests
{
    private CircularCloudLayouter sut;

    [SetUp]
    public void SetUp()
    {
        sut = new CircularCloudLayouter(new Point(0, 0));
    }

    [Test]
    public void LayoutIsEmpty_BeforePutNextRectangle()
    {
        sut.GetLayout().Should().BeEmpty();
    }

    [TestCase(100)]
    [TestCase(1000)]
    [TestCase(5000)]
    public void PutNextRectangle_PutRectanglesWithoutIntersections(int layoutLength)
    {
        sut.GenerateRandomLayout(layoutLength);
        var layout = sut.GetLayout();

        var hasIntersections = layout.Any(x => layout.Any(y => x.IntersectsWith(y) && x != y));
        hasIntersections.Should().BeFalse();
    }

    [Test]
    public void PutNextRectangle_PutFirstRectangleOnCenter()
    {
        var size = new Size(100, 100);
        var expectedLocation = sut.Center - size / 2;

        var rectangle = sut.PutNextRectangle(size);

        rectangle.Location.Should().BeEquivalentTo(expectedLocation);
    }

    [Test]
    public void PutNexRectangle_ShouldThrowArgumentException_WhenRectangleSizeLessThanZero()
    {
        var invalidSize = new Size(-1, -1);

        Action action = () => sut.PutNextRectangle(invalidSize);

        action.Should().Throw<ArgumentException>().WithMessage("Rectangle sizes must be greater than zero");
    }

    [Test]
    public void PutNextRectangle_ShouldCreateTightLayout()
    {
        sut.GenerateRandomLayout(1000);
        var radius = sut.CalculateLayoutRadius();
        var circleArea = Math.PI * radius * radius;

        var rectanglesArea = sut.GetLayout()
            .Aggregate(0.0, (current, rectangle) => current + rectangle.Height * rectangle.Width);

        rectanglesArea.Should().BeInRange(0.8 * circleArea, circleArea);
    }

    [Test]
    public void PutNextRectangle_ShouldCreateCircleLikeLayout()
    {
        const double expectedRadius = 58.309519;
        const double epsilon = 0.000001;

        sut.GenerateLayoutOfSquares(100);

        var actualRadius = sut.CalculateLayoutRadius();
        actualRadius.Should().BeInRange(expectedRadius - epsilon, expectedRadius + epsilon);
    }
}