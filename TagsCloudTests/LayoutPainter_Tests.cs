using System.Drawing;
using FluentAssertions;
using NUnit.Framework;
using TagsCloud;

namespace TagsCloudTests
{
	[TestFixture]
	public class LayoutPainter_Tests
	{
		[TestCase(0, 0, 50, 50, 25, 25, TestName = "Location_At_0_0_And_Image_Size_50_50")]
		[TestCase(0, 0, 25, 25, 12, 12, TestName = "Location_At_0_0_And_Odd_Image_Width_And_Height")]
		[TestCase(-5, 5, 20, 20, 5, 5, TestName = "Square_With_Center_At_Image_Center_And_Image_Size_20_20")]
		public void ToComputerCoordinates_ReturnsCorrectValue(int locationX,
																int locationY,
																int imageWidth,
																int imageHeight,
																int expectedX,
																int expectedY)
		{
			var rectangle = new Rectangle(locationX, locationY, 10, 10);
			var imageSize = new Size(imageWidth, imageHeight);
			var expectedLocation = new Point(expectedX, expectedY);

			rectangle = LayoutPainter.ToComputerCoordinates(rectangle, imageSize);
			rectangle.Location.Should().Be(expectedLocation);
		}
	}
}