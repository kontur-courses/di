using System.Drawing;
using FluentAssertions;
using NUnit.Framework;
using TagsCloud;

namespace TagsCloudTests
{
	[TestFixture]
	public class RectangleExtensions_Tests
	{
		[TestCase(0, 0, 2, 2, 1, -1, TestName = "Square_At_Zero_Center")]
		[TestCase(-1, 1, 2, 2, 0, 0, TestName = "Square_At_Second_Quarter")]
		[TestCase(-5, -2, 2, 2, -4, -3, TestName = "Square_At_Third_Quarter")]
		[TestCase(0, 0, 3, 5, 1.5f, -2.5f, TestName = "Rectangle_With_Odd_Width_And_Height_At_Zero_Center")]
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

		[TestCase(-1, 1, 2, 2, true, TestName = "This_Rectangle_Inside_Another_One")]
		[TestCase(-3, 3, 6, 6, true, TestName = "Another_Rectangle_Inside_This_One")]
		[TestCase(1, 1, 2, 2, true, TestName = "Intersects_With_Half_Area")]
		[TestCase(1, 3, 2, 2, true, TestName = "Intersects_With_One_Corner")]
		[TestCase(-2, 2, 4, 4, true, TestName = "Have_Same_Area")]
		[TestCase(3, 1, 1, 1, false, TestName = "Has_No_Common_Points")]
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