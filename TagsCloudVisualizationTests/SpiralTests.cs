using NUnit.Framework;
using FluentAssertions;
using System.Drawing;
using TagsCloudVisualization.PointCreators;
using TagsCloudVisualization.Settings;

namespace TagsCloudVisualizationTests;

[TestFixture]
public class SpiralTests
{
    private Spiral sut;
    private readonly static Point center = new(10, 10);

    [SetUp]
    public void SetUp()
    {
        var imageSettings = new ImageSettings(20, 20, "red");
        var spiralSettings = new SpiralSettings(0.1, 0.1);
        sut = new Spiral(imageSettings, spiralSettings);
    }

    [TestCase(-0.1, 1, TestName = "Constructor_DeltaAngleNegtaive_ThrowsArgumentException")]
    [TestCase(0.1, -0.1, TestName = "Constructor_DeltaRadiusNegative_ThrowsArgumentException")]
    [TestCase(0, 0.1, TestName = "Constructor_DeltaAngleZero_ThrowsArgumentException")]
    [TestCase(0.1, 0, TestName = "Constructor_DeltaRadiusZero_ThrowsArgumentException")]
    public void Constructor_IncorrectArguments_ThrowsArgumentException(double deltaAngle, double deltaRadius)
    {
        var imageSettings = new ImageSettings(20, 20, "red");
        var spiralSettings = new SpiralSettings(deltaAngle, deltaRadius);

        var action = () => new Spiral(imageSettings, spiralSettings);
        action.Should().Throw<ArgumentException>();
    }

    [Test]
    public void Constructor_DeltaAngleAndDeltaRadiusPositive_NotThrow()
    {
        var imageSettings = new ImageSettings(20, 20, "red");
        var spiralSettings = new SpiralSettings(0.1, 0.1);
        var action = () => new Spiral(imageSettings, spiralSettings);
        action.Should().NotThrow<ArgumentException>();
    }

    [TestCase(0, 1, 1, 0, TestName = "ConvertFromPolarCoordinates_AngleZeroRadiusPositive_ReturnsCorrectPoint")]
    [TestCase(2, 0, 0, 0, TestName = "ConvertFromPolarCoordinates_AnglePositiveRadiusZero_ReturnsCorrectPoint")]
    [TestCase(-5, -5, -1, -4, TestName = "ConvertFromPolarCoordinates_AngleAndRadiusNegative_ReturnsCorrectPoint")]
    [TestCase(-5, 4, 2, 4, TestName = "ConvertFromPolarCoordinates_AngleNegativeRadiusPositive_ReturnsCorrectPoint")]
    [TestCase(4, -5, 4, 4, TestName = "ConvertFromPolarCoordinates_AnglePoisitiveRadiusNegative_ReturnsCorrectPoint")]
    public void ConvertFromPolarCoordinates_SomeCorrectValues_ReturnsCorrectPoint(double angle, double radius,
        int expectedX, int expectedY)
    {
        var result = Spiral.ConvertFromPolarCoordinates(angle, radius);
        var expectedResult = new Point(expectedX, expectedY);

        result.Should().BeEquivalentTo(expectedResult);
    }

    [Test]
    public void GetPointsOnSpiral_CorrectValues_FirstPointShoulBeCenterPoint()
    {
        var points = sut.GetPoints().GetEnumerator();
        points.MoveNext();

        points.Current.Should().BeEquivalentTo(center);
    }

    [Test]
    public void GetPointsOnSpiral_CorrectValues_ReturnsCorrectSecondPoint()
    {
        var points = sut.GetPoints().GetEnumerator();
        points.MoveNext();
        points.MoveNext();

        points.Current.Should().BeEquivalentTo(new Point(11, 11));
    }
}
