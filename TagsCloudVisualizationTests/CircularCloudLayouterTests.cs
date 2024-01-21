using NUnit.Framework;
using FluentAssertions;
using TagsCloudVisualization;
using System.Drawing;

namespace TagsCloudVisualizationTests;

[TestFixture]
public class CircularCloudLayouterTests
{
    private Point center = new(250, 250);
    private CircularCloudLayouter sut;

    [SetUp]
    public void SetUp()
    {
        sut = new CircularCloudLayouter(center);
    }

    [Test]
    public void Constructor_NotTrows()
    {
        var action = () => new CircularCloudLayouter(center);
        action.Should().NotThrow();
    }

    [TestCase(-1, 1, TestName = "PutNextRectangle_WidthNotPositive_ThrowsArgumentException")]
    [TestCase(1, -1, TestName = "PutNextRectangle_HeightNotPositive_ThrowsArgumentException")]
    public void PutNextRectangle_IncorrectSize_ThrowsArgumentException(int rectangleWidth, int rectangleHeight)
    {
        var action = () => sut.PutNextRectangle(new Size(rectangleWidth, rectangleHeight));
        action.Should().Throw<ArgumentException>();
    }

    [Test]
    public void PutNextRectangle_WithCorrectSize_ReturnsCorrectRectangle()
    {
        var rectangle = sut.PutNextRectangle(new Size(15, 15));
        rectangle.Size.Should().BeEquivalentTo(new Size(15, 15));
    }

    [Test]
    public void PutNextRectangle_FirstRectangle_ReturnsRectangleWithCenterInLayoutCenter()
    {
        var rectangle = sut.PutNextRectangle(new Size(15, 15));
        var expectedRectangleCenter = new Point(rectangle.Left + rectangle.Width / 2, rectangle.Top + rectangle.Height / 2);

        expectedRectangleCenter.Should().BeEquivalentTo(center);
    }

    [Test]
    public void PutNextRectangle_TwoRectangles_ReturnsSecondRectangleWithCenterNotInLayoutCenter()
    {
        sut.PutNextRectangle(new Size(15, 15));
        var secondRectangle = sut.PutNextRectangle(new Size(10, 10));
        var expectedRectangleCenter = new Point(secondRectangle.Left + secondRectangle.Width / 2,
            secondRectangle.Top + secondRectangle.Height / 2);

        expectedRectangleCenter.Should().NotBeEquivalentTo(center);
    }

    [Test]
    public void PutNextRectangle_TwoRectangles_ReturnsTwoRectangles()
    {
        sut.PutNextRectangle(new Size(15, 15));
        sut.PutNextRectangle(new Size(10, 10));

        sut.Rectangles.Count.Should().Be(2);
    }

    [Test]
    public void PutNextRectangle_TwoRectangles_ReturnsTwoNotIntersectedRectangles()
    {
        var firstRectangle = sut.PutNextRectangle(new Size(15, 15));
        var secondRectangle = sut.PutNextRectangle(new Size(10, 10));
        var isIntersected = firstRectangle.IntersectsWith(secondRectangle);

        isIntersected.Should().BeFalse();
    }

    [TestCase(10, TestName = "PutNextRectangle_10Rectangles_RectanglesWithNoIntersects")]
    [TestCase(100, TestName = "PutNextRectangle_100Rectangles_RectanglesWithNoIntersects")]
    [TestCase(200, TestName = "PutNextRectangle_200Rectangles_RectanglesWithNoIntersects")]
    public void PutNextRectangle_ManyRectangles_RectanglesWithNoIntersects(int rectanglesCount)
    {
        for (var i = 0; i < rectanglesCount; i++)
        {
            sut.PutNextRectangle(new Size(10, 10));
        }

        HasIntersectedRectangles(sut.Rectangles).Should().BeFalse();
    }

    [TearDown]
    public void SaveImageWhenTestFails()
    {
        if (TestContext.CurrentContext.Result.Outcome.Status == NUnit.Framework.Interfaces.TestStatus.Failed)
        {
            var pathToTestsFailsImages = @"..\..\..\FailsTests";
            if (!Directory.Exists(pathToTestsFailsImages))
            {
                Directory.CreateDirectory(pathToTestsFailsImages);
            }
            var image = Visualizer.Visualize(sut.Rectangles, 500, 500);
            var fileName = $"{TestContext.CurrentContext.Test.Name}.png";
            Visualizer.SaveBitmap(image, fileName, pathToTestsFailsImages);
        }
    }

    private bool HasIntersectedRectangles(IList<Rectangle> rectangles)
    {
        for (var i = 0; i < rectangles.Count - 1; i++)
        {
            for (var j = i + 1; j < rectangles.Count; j++)
            {
                if (rectangles[i].IntersectsWith(rectangles[j]))
                    return true;
            }
        }

        return false;
    }
}