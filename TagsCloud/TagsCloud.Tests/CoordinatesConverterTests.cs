using System.Drawing;
using FluentAssertions;
using NUnit.Framework;
using TagsCloud.Core.Helpers;

namespace TagsCloud.Tests;

[TestFixture]
public class CoordinatesConverterTests
{
	[TestCaseSource(nameof(PointsSource))]
	public void ToPolar_Then_ToCartesian_ShouldReturn_SameCoordinates(int x, int y)
	{
		var point = new Point(x, y);
		var (radius, angle) = CoordinatesConverter.ToPolar(point);
		var point2 = CoordinatesConverter.ToCartesian(radius, angle);

		point2.Should().Be(point);
	}

	[TestCaseSource(nameof(PolarPointsSource))]
	public Point ToCartesian_ShouldReturnCorrectCartesianCoordinates(double radius, double angle)
	{
		return CoordinatesConverter.ToCartesian(radius, angle);
	}

	[TestCaseSource(nameof(CartesianPointsSource))]
	public (double radius, double angle) ToPolar_ShouldReturnCorrectPolarCoordinates(Point cartesian)
	{
		var (radius, angle) = CoordinatesConverter.ToPolar(cartesian);
		return (Math.Round(radius, 2), angle);
	}

	public static IEnumerable<TestCaseData> PointsSource()
	{
		yield return new TestCaseData(0, 0).SetName("Zero point");
		yield return new TestCaseData(5, 4).SetName("Point in the first quadrant");
		yield return new TestCaseData(-9, 122).SetName("Point in the second quadrant");
		yield return new TestCaseData(-1, -1).SetName("Point in the third quadrant");
		yield return new TestCaseData(3, -7).SetName("Point in the fourth quadrant");
	}

	public static IEnumerable<TestCaseData> PolarPointsSource()
	{
		yield return new TestCaseData(5.66, Math.PI / 4).Returns(new Point(4, 4))
			.SetName("Polar coordinate with angle Pi/4 should return point in the first quadrant");

		yield return new TestCaseData(5.66, 3 * Math.PI / 4).Returns(new Point(-4, 4))
			.SetName("Polar coordinate with angle 3Pi/4 should return point in the second quadrant");

		yield return new TestCaseData(5.66, -3 * Math.PI / 4).Returns(new Point(-4, -4))
			.SetName("Polar coordinate with angle -3Pi/4 should return point in the third quadrant");

		yield return new TestCaseData(5.66, -Math.PI / 4).Returns(new Point(4, -4))
			.SetName("Polar coordinate with angle -Pi/4 should return point in the fourth quadrant");
	}

	public static IEnumerable<TestCaseData> CartesianPointsSource()
	{
		yield return new TestCaseData(new Point(4, 4)).Returns((5.66, Math.PI / 4))
			.SetName("Polar angle of point in the first quadrant should be positive");

		yield return new TestCaseData(new Point(-4, 4)).Returns((5.66, 3 * Math.PI / 4))
			.SetName("Polar angle of point in the second quadrant should be positive");

		yield return new TestCaseData(new Point(-4, -4)).Returns((5.66, -3 * Math.PI / 4))
			.SetName("Polar angle of point in the third quadrant should be negative");

		yield return new TestCaseData(new Point(4, -4)).Returns((5.66, -Math.PI / 4))
			.SetName("Polar angle of point in the fourth quadrant should be negative");
	}
}