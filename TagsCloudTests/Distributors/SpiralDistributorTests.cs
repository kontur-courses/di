using System.Drawing;
using FluentAssertions;
using TagsCloud.Distributors;

namespace TagsCloudTests.Distributors;

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
    public void SprialDistributor_InitializeDefaultParams()
    {
        spiralDistribution.Center.Should().Be(center);
        spiralDistribution.Angle.Should().Be(0);
        spiralDistribution.Radius.Should().Be(0);
        spiralDistribution.AngleStep.Should().Be(0.1);
        spiralDistribution.RadiusStep.Should().Be(0.1);
    }

    [Test]
    public void SprialDistribution_InitializeCustomParams()
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
    public void SpiralDistributionConstructor_ShouldThrowArgumentException(double angleStep, double radiusStep)
    {
        Assert.Throws<ArgumentException>(() => new SpiralDistributor(center, angleStep, radiusStep));
    }

    [Test]
    public void SpiralDistribution_ShouldReturnCenter_WhenFirstCallGetNextPoint()
    {
        spiralDistribution.GetNextPosition().Should().Be(center);
    }

    [Test]
    public void SpiralDistribution_ShouldIncreaseAngle_WhenCallGetNextPoint()
    {
        spiralDistribution.GetNextPosition();
        spiralDistribution.Angle.Should().Be(spiralDistribution.AngleStep);
    }

    [Test]
    public void SpiralDistribution_ShouldIncreaseRadius_WhenAngleMoreThan2Pi()
    {
        var expectedAngle = spiralDistribution.Angle * 64 - 2 * Math.PI;
        for (var i = 0; i < 63; i++) spiralDistribution.GetNextPosition();
        spiralDistribution.Radius.Should().Be(spiralDistribution.Radius);
    }
}