using System;
using System.Drawing;
using FluentAssertions;
using NUnit.Framework;

namespace TagsCloudVisualization
{
	public class ExMath_Tests
	{
		[TestCase(1.1, 0, 2, TestName = "Positive value and center at 0")]
		[TestCase(-1.1, 0, -2, TestName = "Negative value and center at 0")]
		[TestCase(0, 0, 0, TestName = "Zero value and center at 0")]
		[TestCase(1.1, 5, 1, TestName = "Positive value less than center when center at 5")]
		[TestCase(5.1, 5, 6, TestName = "Positive value grater than center when center at 5")]
		[TestCase(5.0, 5, 5, TestName = "Value equals to center when center at 5")]
		[TestCase(-1.1, 5, -2, TestName = "Negative value when center at 5")]
		[TestCase(1.1, -5, 2, TestName = "Positive value when center at -5")]
		[TestCase(-1.1, -5, -1, TestName = "Negative value grater then center when center at -5")]
		[TestCase(-5.1, -5, -6, TestName = "Negative value less then center when center at -5")]
		[TestCase(-5.0, -5, -5, TestName = "Negative value equals to center when center at -5")]
		public void RoundCoordinate_RoundsFromCenter(double value, int centerCoordinate, int expectedValue)
		{
			var actualValue = ExMath.RoundCoordinate(value, centerCoordinate);
			actualValue.Should().Be(expectedValue);
		}

		[TestCase(0, 0, 1, 0, 1, 0, TestName = "(1, 0) => (1, 0)")]
		[TestCase(0, 0, 1, Math.PI / 4, 1, 1, TestName = "(1, PI/4) => (1, 1)")]
		[TestCase(0, 0, 1, Math.PI / 2, 1, 1, TestName = "(1, PI/2) => (1, 1)")]
		[TestCase(5, 5, 1, Math.PI / 4, 1+5, 1+5, TestName = "(1, PI/4) => (6, 6) when center at (5,5)")]
		[TestCase(-5, -5, 1, 3*Math.PI/4, -1-5, 1-5, TestName = "(1, 3*PI/4) => (-6, -4) when center at (-5,-5)")]
		public void ToCartesianCoordinateSystem_ReturnsCorrectValue(int centerX,
																	int centerY,
																	double radius, 
																	double angle,
																	int expectedX,
																	int expectedY)
		{
			var expectedLocation = new Point(expectedX, expectedY);
			var center = new Point(centerX, centerY);
			
			var actualValue = ExMath.ToCartesianCoordinateSystem(radius, angle, center);
			actualValue.Should().Be(expectedLocation);
		}
	}
}