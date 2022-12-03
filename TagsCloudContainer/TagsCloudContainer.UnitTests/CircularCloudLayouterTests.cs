using System.Drawing;
using NUnit.Framework.Interfaces;

namespace TagsCloudContainer.UnitTests;

[TestFixture]
public class CircularCloudLayouterTests
{
    [SetUp]
    public void SetUp()
    {
        center = new(CloudCenterX, CloudCenterY);
        drawingTracer = new(CloudWidth, CloudHeight, center);
        layouterAlgorithm = new(center, drawingTracer);
    }

    [TearDown]
    public void TearDown()
    {
        var fileName = $"test-{TestContext.CurrentContext.Test.MethodName}-{TestContext.CurrentContext.Test.ID}.jpg";
        var fullPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, fileName);

        if (TestContext.CurrentContext.Result.Outcome.Status == TestStatus.Failed)
        {
            fileName =
                $"error-test-{TestContext.CurrentContext.Test.MethodName}-{TestContext.CurrentContext.Test.ID}.jpg";
            fullPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, fileName);
            TestContext.Out.WriteLine($"Tag cloud visualization saved to file {fullPath}");
        }

        drawingTracer.SaveToFile(fullPath);
        drawingTracer.Dispose();
    }

    private const int CloudWidth = 1000;
    private const int CloudHeight = 1000;
    private const int CloudCenterX = 500;
    private const int CloudCenterY = 500;

    private DrawingCircularCloudLayoutTracer drawingTracer = null!;
    private CircularLayouterAlgorithm layouterAlgorithm = null!;
    private Point center = Point.Empty;

    [Test]
    public void PutNextRectangle_RectangleAtCenter_ValidSize()
    {
        var rectangleSize = new Size(CloudCenterX / 2, CloudCenterY / 2);
        var expectedRectangle = new Rectangle(CloudCenterX - rectangleSize.Width / 2,
            CloudCenterY - rectangleSize.Height / 2, rectangleSize.Width, rectangleSize.Height);

        var actualRectangle = layouterAlgorithm.PutNextRectangle(rectangleSize);

        actualRectangle.Should().BeEquivalentTo(expectedRectangle);
    }

    [Test]
    public void PutNextRectangle_TwoRectangleThatAreTouchesButAreNotIntersects_TwoValidSizes()
    {
        var firstRectangleSize = new Size(10, 10);
        var secondRectangleSize = new Size(10, 10);

        var actualFirstRectangle = layouterAlgorithm.PutNextRectangle(firstRectangleSize);
        var actualSecondRectangle = layouterAlgorithm.PutNextRectangle(secondRectangleSize);

        actualFirstRectangle.TouchesWith(actualSecondRectangle).Should().BeTrue();
    }

    private static IEnumerable<TestCaseData>
        RectangleSizes_Source()
    {
        yield return new(GenerateSizes(0, 80, new(100, 25), new(50, 20)), 0.4d);
        yield return new(GenerateSizes(20021011, 80, new(100, 25), new(50, 20)), 0.4d);
        yield return new(GenerateSizes(20221109, 80, new(100, 25), new(50, 20)), 0.3d);
        yield return new(GenerateSizes(1, 1000, new(10, 10), new(10, 10)), 0.1d);
    }

    private static Size[] GenerateSizes(int randomSeed, int count, Size maxSize, Size minSize)
    {
        var random = new Random(randomSeed);
        return Enumerable.Range(0, count)
            .Select(_ => new Size(random.Next(minSize.Width, maxSize.Width + 1),
                random.Next(minSize.Height, maxSize.Height + 1)))
            .ToArray();
    }

    [TestCaseSource(nameof(RectangleSizes_Source))]
    public void
        PutNextRectangle_SeveralRectangleThatAreNotIntersects_SeveralValidSizes(
            Size[] sizes, double expectedAreaErrorRate)
    {
        var rectangles = _ = sizes.Select(layouterAlgorithm.PutNextRectangle).ToArray();

        foreach (var rectangle in rectangles)
            rectangles
                .Where(other => other != rectangle)
                .Should()
                .Match(otherRectangles => otherRectangles.All(other => !other.IntersectsWith(rectangle)));
    }


    [TestCaseSource(nameof(RectangleSizes_Source))]
    public void
        PutNextRectangle_SeveralRectangleThatAreTouches_SeveralValidSizes(
            Size[] sizes, double expectedAreaErrorRate)
    {
        var rectangles = _ = sizes.Select(layouterAlgorithm.PutNextRectangle).ToArray();

        rectangles.Should().Match(enumerable => enumerable.All(rectangle => rectangles
            .Where(other => other != rectangle)
            .Any(other => other.TouchesWith(rectangle))));
    }


    [TestCaseSource(nameof(RectangleSizes_Source))]
    public void
        PutNextRectangle_SeveralRectangleThatAreLocatedQuiteTightlyInsideCircle_SeveralValidSizes(
            Size[] sizes, double expectedAreaErrorRate)
    {
        var rectangles = _ = sizes.Select(layouterAlgorithm.PutNextRectangle).ToArray();
        var rectanglesArea = (double)rectangles
            .Select(x => x.Width * x.Height)
            .Sum();
        var maxDistanceFromCenter = rectangles
            .Select(rectangle => rectangle.GetFarthestDeltaFromTarget(center))
            .Select(point => point.DistanceTo(Point.Empty))
            .Max();
        var expectedCircleArea = maxDistanceFromCenter * maxDistanceFromCenter * Math.PI;

        rectanglesArea.Should()
            .BeApproximately(expectedCircleArea, expectedCircleArea * expectedAreaErrorRate);
    }
}