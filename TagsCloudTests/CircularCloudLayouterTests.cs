using System.Drawing;
using System.Drawing.Imaging;
using FluentAssertions;
using NUnit.Framework.Interfaces;
using TagsCloud;

namespace TagsCloudTests;

public class CircularCloudLayouterTests
{
    private ISpiral spiral;
    private ICircularCloudLayouter sut;

    [SetUp]
    public void SetUp()
    {
        spiral = new Spiral(new Point(10, 10));
        sut = new CircularCloudLayouter(spiral);
    }

    [TearDown]
    public void TearDown()
    {
        if (TestContext.CurrentContext.Result.Outcome != ResultState.Failure) return;
        var workingDirectory = Environment.CurrentDirectory;
        var parentDirectory = Directory.GetParent(workingDirectory)?.Parent;
            
        var directoryPath = parentDirectory != null
            ? Path.Combine(parentDirectory.FullName, "FailedTestImages")
            : Path.Combine(workingDirectory, "FailedTestImages");
            
        if (!Directory.Exists(directoryPath))
        {
            Directory.CreateDirectory(directoryPath);
        }

        var imageName = TestContext.CurrentContext.Test.Name;
        var imagePath = Path.Combine(directoryPath, $"{imageName}.png");
            
        RectanglesVisualizer.GetTagsCloudImage(sut.Rectangles).Save(imagePath, ImageFormat.Png);

        Console.WriteLine($"Tag cloud visualization saved to file {imagePath}");
    }


    private static bool IsRectanglesIntersect(List<Rectangle> rectangles) =>
    rectangles.Any(rectangle => rectangles.Any(nextRectangle =>
            nextRectangle.IntersectsWith(rectangle) && !rectangle.Equals(nextRectangle)));


    [Test]
    public void GetLocationAfterInitialization_ShouldBeEmpty()
    {
        var location = sut.GetRectanglesLocation();
        location.Should().BeEmpty();
    }

    [TestCase(-1, 10, TestName = "width is negative")]
    [TestCase(1, -10, TestName = "height is negative")]
    [TestCase(1, 0, TestName = "Zero height, correct width")]
    [TestCase(0, 10, TestName = "Zero width, correct height")]
    public void PutRectangleWithNegativeParams_ShouldBeThrowException(int width, int height)
    {
        var size = new Size(width, height);
        Action action = () => sut.PutNextRectangle(size);
        action.Should().Throw<ArgumentException>()
            .WithMessage("Sides of the rectangle should not be non-positive");
    }

    [Test]
    public void PutOneRectangle_IsNotEmpty()
    {
        var rectangle = sut.PutNextRectangle(new Size(10, 10));
        var location = sut.GetRectanglesLocation();
        location.Should().NotBeEmpty();
    }

    [Test]
    public void Put1000Rectangles_RectanglesShouldNotIntersect()
    {
        for (var i = 0; i < 1000; i++)
        {
            var size = Utils.GetRandomSize();
            var rectangle = sut.PutNextRectangle(size);
        }

        IsRectanglesIntersect(sut.Rectangles).Should().BeFalse();
    }
}