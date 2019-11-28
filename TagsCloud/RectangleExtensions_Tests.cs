using System.Drawing;
using FluentAssertions;
using NUnit.Framework;

namespace TagsCloudVisualization
{
	[TestFixture]
	public class RectangleExtensions_Tests
	{
		[TestCase(0, 0, 2, 2, 1, -1, TestName = "Square at (0, 0)")]
		[TestCase(-1, 1, 2, 2, 0, 0, TestName = "Square at (-1, 1)")]
		[TestCase(-5, -2, 2, 2, -4, -3, TestName = "Square at (-1, 1)")]
		[TestCase(0, 0, 3, 5, 1.5f, -2.5f, TestName = "Rectangle with odd width and height at (0, 0)")]
		public void GetCenter_ReturnsCorrectValue(int locationX,
													int locationY,
													int rectangleWidth,
													int rectangleHeight,
													float centerX,
													float centerY)
		{
			var rectangle = new Rectangle(locationX, locationY, rectangleWidth, rectangleHeight);
			var expectedCenter = new PointF(centerX, centerY);

			var actualCenter = rectangle.GetCenter();
			actualCenter.Should().Be(expectedCenter);
		}
	}
}