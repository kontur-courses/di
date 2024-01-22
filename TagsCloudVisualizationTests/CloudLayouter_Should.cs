using System.Drawing;
using FluentAssertions;
using TagsCloudVisualization;

namespace TagsCloudVisualizationTests;

public class CloudLayouter_Should
{
    private CloudLayouter? circularCloudLayouter;

    [SetUp]
    public void SetCircularCloudFieldToNull()
    {
        circularCloudLayouter = null;
    }

    [TearDown]
    public void CreateLayoutImage_IfTestFailed()
    {
        if (TestContext.CurrentContext.Result.FailCount < 1) return;

        var testName = TestContext.CurrentContext.Test.Name;
        var workDirectory = TestContext.CurrentContext.WorkDirectory;

        LayoutDrawer.CreateLayoutImage(circularCloudLayouter?.CreatedRectangles!, testName, workDirectory);

        var filePath = $@"{workDirectory}\{testName}.png";

        TestContext.WriteLine($"Tag cloud visualization saved to file {filePath}");
        TestContext.AddTestAttachment($"{testName}.png");
    }

    [TestCase(-1, 0, TestName = "Negative width")]
    [TestCase(0, -1, TestName = "Negative height")]
    [TestCase(-5, -5, TestName = "Negative width and height")]
    public void PutNextRectangleThrowsArgumentException_WhenNegativeParameters(int rectWidth, int rectHeight)
    {
        circularCloudLayouter = new CloudLayouter(new SpiralGenerator());
        var rectangleSize = new Size(rectWidth, rectHeight);
        var rectangleCreation = () => circularCloudLayouter.PutNextRectangle(rectangleSize);
        rectangleCreation.Should().Throw<ArgumentException>();
    }

    [TestCaseSource(nameof(GeneratorsAndMaxDistance))]
    public void PutNextRectanglePlacesSquaresNear_WithDifferentRealisations(
        IPointGenerator pointGenerator,
        int closestRectangleMaxDistance)
    {
        circularCloudLayouter = new CloudLayouter(pointGenerator);
        var squareSide = 20;
        var rectangleSize = new Size(squareSide, squareSide);
        var rectanglesWithoutCurrent = new List<Rectangle>();

        var firstRectangle = circularCloudLayouter.PutNextRectangle(rectangleSize);
        rectanglesWithoutCurrent.Add(firstRectangle);

        for (var i = 1; i < 15; i++)
        {
            var currentRectangle = circularCloudLayouter.PutNextRectangle(rectangleSize);

            var closestRectangleDistance = rectanglesWithoutCurrent
                .Min(existingRectangle => CalculateDistanceBetweenRectangles(currentRectangle, existingRectangle));

            closestRectangleDistance.Should().BeLessThan(closestRectangleMaxDistance);
            rectanglesWithoutCurrent.Add(currentRectangle);
        }
    }

    private static object[][] GeneratorsAndMaxDistance =
    {
        new object[] { new SpiralGenerator(), 21 }
    };

    private double CalculateDistanceBetweenRectangles(Rectangle firstRectangle, Rectangle secondRectangle)
    {
        var xSquare = (firstRectangle.X - secondRectangle.X) * (firstRectangle.X - secondRectangle.X);
        var ySquare = (firstRectangle.Y - secondRectangle.Y) * (firstRectangle.Y - secondRectangle.Y);
        return Math.Sqrt(xSquare + ySquare);
    }
}