using System.Drawing;
using FluentAssertions;
using TagsCloudVisualization;

namespace TagsCloudVisualizationTests;

public class RectangleLayouter_Should
{
    private RectangleLayouter? rectangleLayouter;
    private TagLayoutSettings tagLayoutSettings;
    private IEnumerable<IPointGenerator> pointGenerators;

    [SetUp]
    public void SetCircularCloudFieldToNull()
    {
        rectangleLayouter = null;
        tagLayoutSettings = new TagLayoutSettings(Algorithm.Spiral, new HashSet<string>(), null);
        pointGenerators = new[] { new SpiralPointGenerator() };
    }
    
    [TestCase(-1, 0, TestName = "Negative width")]
    [TestCase(0, -1, TestName = "Negative height")]
    [TestCase(-5, -5, TestName = "Negative width and height")]
    public void PutNextRectangleThrowsArgumentException_WhenNegativeParameters(int rectWidth, int rectHeight)
    {
        rectangleLayouter = new RectangleLayouter(tagLayoutSettings, pointGenerators);
        var rectangleSize = new Size(rectWidth, rectHeight);
        var rectangleCreation = () => rectangleLayouter.PutNextRectangle(rectangleSize);
        rectangleCreation.Should().Throw<ArgumentException>();
    }

    [TestCaseSource(nameof(GeneratorsAndMaxDistance))]
    public void PutNextRectanglePlacesSquaresNear_WithDifferentRealisations(
        IPointGenerator pointGenerator,
        int closestRectangleMaxDistance)
    {
        rectangleLayouter = new RectangleLayouter(tagLayoutSettings, pointGenerators);
        var squareSide = 20;
        var rectangleSize = new Size(squareSide, squareSide);
        var rectanglesWithoutCurrent = new List<Rectangle>();

        var firstRectangle = rectangleLayouter.PutNextRectangle(rectangleSize);
        rectanglesWithoutCurrent.Add(firstRectangle);

        for (var i = 1; i < 15; i++)
        {
            var currentRectangle = rectangleLayouter.PutNextRectangle(rectangleSize);

            var closestRectangleDistance = rectanglesWithoutCurrent
                .Min(existingRectangle => CalculateDistanceBetweenRectangles(currentRectangle, existingRectangle));

            closestRectangleDistance.Should().BeLessThan(closestRectangleMaxDistance);
            rectanglesWithoutCurrent.Add(currentRectangle);
        }
    }

    private static object[][] GeneratorsAndMaxDistance =
    {
        new object[] { new SpiralPointGenerator(), 21 }
    };

    private double CalculateDistanceBetweenRectangles(Rectangle firstRectangle, Rectangle secondRectangle)
    {
        var xSquare = (firstRectangle.X - secondRectangle.X) * (firstRectangle.X - secondRectangle.X);
        var ySquare = (firstRectangle.Y - secondRectangle.Y) * (firstRectangle.Y - secondRectangle.Y);
        return Math.Sqrt(xSquare + ySquare);
    }
}