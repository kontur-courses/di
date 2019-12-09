using System.Drawing;
using FluentAssertions;
using NUnit.Framework;
using TagsCloud;

namespace TagsCloudTests
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

		[TestCase(-1, 1, 2, 2, true, TestName = "This rectangle inside another one")]
		[TestCase(-3, 3, 6, 6, true, TestName = "Another rectangle inside this one")]
		[TestCase(1, 1, 2, 2, true, TestName = "Intersects with half area")]
		[TestCase(1, 3, 2, 2, true, TestName = "Intersects with one corner")]
		[TestCase(-2, 2, 4, 4, true, TestName = "Have same area")]
		[TestCase(3, 1, 1, 1, false, TestName = "Has no common points")]
		public void Intersects_ReturnsCorrectResult(int locationX,
													int locationY,
													int rectangleWidth,
													int rectangleHeight,
													bool expectedResult)
		{
			var defaultRectangle = new Rectangle(-2, 2, 4, 4);
			var rectangle = new Rectangle(locationX, locationY, rectangleWidth, rectangleHeight);

			var actualResult = rectangle.Intersects(defaultRectangle);
			actualResult.Should().Be(expectedResult);
		}
	}
}