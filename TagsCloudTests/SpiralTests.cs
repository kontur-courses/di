using System.Drawing;
using FluentAssertions;
using TagsCloud.CloudLayouter;

namespace TagsCloudTests;

public class SpiralTests
{
    private Point center;

    [SetUp]
    public void Setup()
    {
        center = new Point(10, 10);
    }

    private static IEnumerable<TestCaseData> ConstructorSpiralPoints => new[]
    {
        new TestCaseData(2.5f,
                new Point[] { new(10, 10), new(5, 14), new(14, -2), new(16, 28), new(-11, -4), new(41, 8) })
            .SetName("AngleStep is positive"),
        new TestCaseData(-2.5f,
                new Point[] { new(10, 10), new(5, 6), new(14, 22), new(16, -8), new(-11, 24), new(41, 12) })
            .SetName("AngleStep is negative")
    };

    [Test]
    public void Spiral_StepAngleEquals0_ShouldBeThrowException()
    {
        Action action = () => new Spiral(0);
        action.Should().Throw<ArgumentException>()
            .WithMessage("the step must not be equal to 0");
    }

    [TestCaseSource(nameof(ConstructorSpiralPoints))]
    public void Spiral_GetNextPoint_CreatePointsWithCustomAngle_ReturnsCorrectPoints(float angleStep,
        Point[] expectedPoints)
    {
        var spiral = new Spiral(angleStep);
        spiral.GetPoints(center).Take(expectedPoints.Length).Should().BeEquivalentTo(expectedPoints);
    }
}