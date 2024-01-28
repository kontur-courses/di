using System.Drawing;
using FluentAssertions;
using TagsCloudContainer.utility;

namespace TagsCloudContainerTests.UtilityTests;

[TestFixture]
public class PointMathTests
{
    [TestCase(0, 0, 0, 0)]
    [TestCase(15, 90, 0, 15)]
    [TestCase(10, 45, 7, 7)]
    [TestCase(10, 45, -3, -3, -10, -10)]
    public void ShouldReturn_CartesianCoordinate(int radius, int angle, int expectedX, int expectedY,
        int offsetX = 0, int offsetY = 0)
    {
        var expected = new Point(expectedX, expectedY);

        var actual = PointMath.PolarToCartesian(radius, angle, offsetX, offsetY);

        actual.Should().Be(expected);
    }
}