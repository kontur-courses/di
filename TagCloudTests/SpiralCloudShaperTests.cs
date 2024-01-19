using System.Drawing;
using System.Runtime.ExceptionServices;
using FluentAssertions;
using TagCloud;

namespace TagCloudTests;

public class SpiralCloudShaperTests
{
    private Point center;
    private SpiralCloudShaper shaper;

    [SetUp]
    public void SetUp()
    {
        center = new Point(0, 0);
        shaper = SpiralCloudShaper.Create(center);
    }

    [TestCase(-1, 1, TestName = "NegativeCoefficient")]
    [TestCase(1, -1, TestName = "NegativeDeltaAngle")]
    [TestCase(-1, -1, TestName = "NegativeDeltaAngleAndCoefficient")]
    public void Throw_OnCreationWith(double coefficient, double deltaAngle)
    {
        Assert.Throws<ArgumentException>(() => SpiralCloudShaper.Create(center, coefficient, deltaAngle));
    }

    [TestCase(1, 1, TestName = "Integer coefficients")]
    [TestCase(0.1D, 0.1D, TestName = "Double coefficients")]
    public void NotThrow_OnCreationWith(double coefficient, double deltaAngle)
    {
        Assert.DoesNotThrow(() => SpiralCloudShaper.Create(center, coefficient, deltaAngle));
    }

    [Test]
    public void RadiusShouldIncrease()
    {
        shaper.GetPossiblePoints()
            .Take(100)
            .Select(point => point.GetDistanceTo(center))
            .Should()
            .BeInAscendingOrder();
    }
}