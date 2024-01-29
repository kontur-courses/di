using System.Drawing;
using FluentAssertions;
using TagsCloud.CloudLayouter;

namespace TagsCloudTests;

public class FlowerSpiralTests
{
    private Point center;

    [SetUp]
    public void Setup()
    {
        center = new Point(10, 10);
    }

    private static IEnumerable<TestCaseData> ConstructorSpiralPoints => new[]
    {
        new TestCaseData(0, 6, 2.5f,
                new Point[] { new(10, 10), new(10, 10), new(10, 10) })
            .SetName("petal Length equals 0"),
        new TestCaseData(6, 0, 2.5f,
                new Point[] { new(10, 10), new(10, 10), new(10, 10) })
            .SetName("petal Count equals 0"),
        new TestCaseData(4, 4, 2.5f,
                new Point[] { new(10, 10), new(14, 7), new(15, -8), new(0, -18), new(-15, -6), new(-3, 11) })
            .SetName("AngleStep is positive"),
        new TestCaseData(4, 4, -2.5f,
                new Point[] { new(10, 10), new(14, 13), new(15, 28), new(0, 38), new(-15, 26), new(-3, 9) })
            .SetName("AngleStep is negative")
    };

    [Test]
    public void FlowerSpiral_WhenPetalCountAndPetalLengthLessThat0_ShouldBeThrowException()
    {
        Action action = () => new FlowerSpiral(-1, -1);
        action.Should().Throw<ArgumentException>()
            .WithMessage("petalCount or petalLength must not be less than 0");
    }

    [TestCaseSource(nameof(ConstructorSpiralPoints))]
    public void FlowerSpiral_GetNextPoint_CreatePointsWithCustomAngle_ReturnsCorrectPoints(double petalLength,
        int petalCount, float angleStep,
        Point[] expectedPoints)
    {
        var spiral = new FlowerSpiral(petalLength, petalCount, angleStep);
        spiral.GetPoints(center).Take(expectedPoints.Length).Should().BeEquivalentTo(expectedPoints);
    }
}