using System.Drawing;
using FluentAssertions;
using TagsCloud.Distributors;

namespace TagsCloudTests;

[TestFixture]
public class SpiralDistributorTests
{
    [SetUp]
    public void SetUp()
    {
        center = new Point();
        spiralDistribution = new SpiralDistributor();
    }

    private Point center;
    private SpiralDistributor spiralDistribution;


    [Test]
    public void SprialDistribution_Initialize__Default_Params()
    {
        spiralDistribution.Center.Should().Be(center);
        spiralDistribution.Angle.Should().Be(0);
        spiralDistribution.Radius.Should().Be(0);
        spiralDistribution.AngleStep.Should().Be(0.1);
        spiralDistribution.RadiusStep.Should().Be(0.1);
    }

    [Test]
    public void SprialDistribution_Initialize__Custom_Params()
    {
        var customCenter = new Point(1, 1);
        var customSpiralDistribution = new SpiralDistributor(customCenter, 0.6, 0.7);

        customSpiralDistribution.Center.Should().Be(customCenter);
        customSpiralDistribution.AngleStep.Should().Be(0.6);
        customSpiralDistribution.RadiusStep.Should().Be(0.7);
    }

    [TestCase(0, 1, TestName = "When_AngleStep_Is_Zero")]
    [TestCase(0.6, -1, TestName = "When_Radius_Is_Negative")]
    [TestCase(0.6, 0, TestName = "When_Radius_Is_Zero")]
    public void SpiralDistribution_Initialize_Throw_ArgumentException(double angleStep, double radiusStep)
    {
        Assert.Throws<ArgumentException>(() => new SpiralDistributor(center, angleStep, radiusStep));
    }

    [Test]
    public void SpiralDistribution_First_Call_GetNextPoint_Should_Return_Center()
    {
        spiralDistribution.GetNextPosition().Should().Be(center);
    }

    [Test]
    public void SpiralDistribution_Should_Increase_Angle_After_Call_GetNextPoint()
    {
        spiralDistribution.GetNextPosition();
        spiralDistribution.Angle.Should().Be(spiralDistribution.AngleStep);
    }

    [Test]
    public void SpiralDistribution_Should_Increase_Radius_When_Angle_More_Than_2Pi()
    {
        var expectedAngle = spiralDistribution.Angle * 64 - 2 * Math.PI;
        for (var i = 0; i < 63; i++) spiralDistribution.GetNextPosition();
        spiralDistribution.Radius.Should().Be(spiralDistribution.Radius);
    }
}