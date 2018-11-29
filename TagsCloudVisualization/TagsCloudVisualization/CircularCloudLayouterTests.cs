using System;
using System.Drawing;
using System.Linq;
using NUnit.Framework;
using FluentAssertions;
using NUnit.Framework.Interfaces;
using TagsCloudVisualization.Curves;

namespace TagsCloudVisualization
{
	[TestFixture]
	internal class CircularCloudLayouterTests
	{
		private CircularCloudLayouter cloud;

		[SetUp]
		public void SetUp()
		{
			var spiral = new Spiral(factorStep: 0.5, degreeStep: Math.PI / 18);
			cloud = new CircularCloudLayouter(spiral);
		}

		[TearDown]
		public void TearDown()
		{
			if (TestContext.CurrentContext.Result.Outcome.Status != TestStatus.Passed)
			{
				var rectangles = cloud.GetRectangles();
				var picture = RectangleTagsCloudVisualizer.GetPicture(rectangles, Color.Aqua);
				var path = $"{TestContext.CurrentContext.TestDirectory}\\{TestContext.CurrentContext.Test.FullName}";
				picture.Save($"{path}.png");
				TestContext.WriteLine($"Tag cloud visualization saved to file {path}");
			}
		}


		[TestCase(1, TestName = "one")]
		[TestCase(42, TestName = "fourty two")]
		[TestCase(100, TestName = "one hundred")]
		public void PutNextRectangle_ShouldAddNumberOfRectangles(int number)
		{
			for (var i = 0; i < number; i++)
			{
				var size = new Size(10 + i, 5 + i);
				cloud.PutNextRectangle(size);
			}

			var rectangles = cloud.GetRectangles();

			rectangles.Should().HaveCount(number);
		}

		[Test]
		public void PutNextRectangle_WithCorrectSize_ReturnRectangleWithThisSize()
		{
			var size = new Size(100, 50);
			var rectangle = cloud.PutNextRectangle(size);

			rectangle.Size.ShouldBeEquivalentTo(size);
		}

		[TestCase(100, 200, TestName = "height more than width")]
		[TestCase(300, 100, TestName = "width more than height")]
		public void NextRectangleShouldNotIntersectWithPrevious(int width, int height)
		{
			var size = new Size(width, height);
			var nextRectangle = cloud.PutNextRectangle(size);
			var previousRectangle = cloud.PutNextRectangle(size);

			nextRectangle.IntersectsWith(previousRectangle).Should().BeFalse();
		}

		[Test]
		public void PutNextRectangle_WithNegativeSize_ShouldThrowException()
		{
			var size = new Size(-100, -100);
			Assert.Throws<ArgumentException>(() => cloud.PutNextRectangle(size));
		}

		[TestCase(50)]
		[TestCase(150)]
		[TestCase(400)]
		public void CloudLayouter_ShouldPlaceRectanglesTightly(int number)
		{
			for (var i = 0; i < number; i++)
				cloud.PutNextRectangle(new Size(2 * (i + 1), i + 1));

			var rectangles = cloud.GetRectangles();
			var min = new Point(rectangles.Min(r => r.Left), rectangles.Min(r => r.Top));
			var max = new Point(rectangles.Max(r => r.Right), rectangles.Max(r => r.Bottom));
			double rectanglesArea = rectangles.Sum(r => r.Width * r.Height);
			double wholeArea = (max.X - min.X) * (max.Y - min.Y);

			(rectanglesArea / wholeArea).Should().BeGreaterThan(0.4);
		}

		[Test]
		public void CloudLayouter_ShouldPlaceRectanglesInCircle()
		{
			for (var i = 0; i < 100; i++)
				cloud.PutNextRectangle(new Size(2 * (i + 1), i + 1));

			var rectangles = cloud.GetRectangles();
			var min = new Point(rectangles.Min(r => r.Left), rectangles.Min(r => r.Top));
			var max = new Point(rectangles.Max(r => r.Right), rectangles.Max(r => r.Bottom));
			double width = max.X - min.X;
			double height = max.Y - min.Y;

			var centerOfRectangles = new Point((max.X + min.X) / 2, (max.Y + min.Y) / 2);
			var differenceX = Math.Abs(centerOfRectangles.X);
			var differenceY = Math.Abs(centerOfRectangles.Y);

			(differenceY / height).Should().BeLessThan(0.1);
			(differenceX / width).Should().BeLessThan(0.1);
		}
	}
}