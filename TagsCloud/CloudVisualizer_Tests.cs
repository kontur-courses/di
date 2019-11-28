using System.Linq;
using System.Drawing;
using NUnit.Framework;
using FluentAssertions;

namespace TagsCloudVisualization
{
	[TestFixture]
	public class CloudVisualizer_Tests
	{
		[Test]
		public void CalculateImageSize_ReturnsCorrectSize()
		{
			var rectangleSize = new Size(10, 10);
			var locations = new[]
			{
				new Point(-10, 10),
				new Point(0, 10),
				new Point(0, 0),
				new Point(-10, 0),
			};
			var rectangles = locations.Select(location => new Rectangle(location, rectangleSize));
			const int imagePadding = CloudVisualizer.RectangleBorderWidth;
			var expectedImageSize = new Size(20 + imagePadding, 20 + imagePadding);
			
			var actualImageSize = CloudVisualizer.CalculateImageSize(rectangles);
			actualImageSize.Should().Be(expectedImageSize);
		}

		[TestCase(1, 1, 1, 1, TestName = "All numbers are the same")]
		[TestCase(2, 1, 1, 2, TestName = "First number is the largest")]
		[TestCase(1, 2, 1, 2, TestName = "Second number is the largest")]
		[TestCase(1, 1, 2, 2, TestName = "Third number is the largest")]
		[TestCase(-2, 1, 1, 2, TestName = "Negative number is the largest in absolute value")]
		[TestCase(1, 2, 2, 2, TestName = "Two numbers are the same and largest")]
		public void GetAbsoluteMax_ReturnsCorrectValue(int number1, int number2, int number3, int expectedValue)
		{
			var actualValue = CloudVisualizer.GetAbsoluteMax(number1, number2, number3);
			actualValue.Should().Be(expectedValue);
		}
		
		[TestCase(0, 0, 50, 50, 25, 25, TestName = "Location at (0, 0) and image size (50, 50)")]
		[TestCase(0, 0, 25, 25, 12, 12, TestName = "Location at (0, 0) and odd image width and height")]
		[TestCase(-5, 5, 20, 20, 5, 5, TestName = "Square with center at image center and image size (20, 20)")]
		public void MoveToImageCenter_ReturnsCorrectValue(int locationX, 
			int locationY,
			int imageWidth,
			int imageHeight,
			int expectedX,
			int expectedY)
		{
			var rectangle = new Rectangle(locationX, locationY, 10, 10);
			var imageSize = new Size(imageWidth, imageHeight);
			var expectedLocation = new Point(expectedX, expectedY);
			
			rectangle = CloudVisualizer.MoveToImageCenter(rectangle, imageSize);
			rectangle.Location.Should().Be(expectedLocation);
		}
	}
}