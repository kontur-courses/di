using System;
using System.Linq;
using NUnit.Framework;
using System.Drawing;
using FluentAssertions;
using NUnit.Framework.Interfaces;
using TagsCloudContainer.CloudLayouters;

namespace TagsCloudContainerTests
{
    [TestFixture]
    class CircularCloudLayouterTests
    {
        private CircularCloudLayouter cloud;

        [TearDown]
        public void TearDown()
        {
            if (TestContext.CurrentContext.Result.Outcome.Status == TestStatus.Failed)
            {
                var fileName = $"{TestContext.CurrentContext.Test.Name}_layout.bmp";
                MyConverter.GetBitmapFromRectangles(cloud.rectangles, fileName);
                Console.Error.WriteLine($"Tag cloud visualization saved to file {fileName}");
            }
        }

        [SetUp]
        public void SetUp()
        {
            cloud = new CircularCloudLayouter(new Point());
        }

        [Test]
        public void PutNextRectangle_NotThrow_WhenEmptySize()
        {
            var putNextRectangle = new Action(() => cloud.PutNextRectangle(new Size()));
            putNextRectangle.Should().NotThrow();
        }

        [Test]
        public void PutNextRectangle_OneSimpleRectangle_RectangleInCenter()
        {
            cloud.PutNextRectangle(new Size(10, 20)).Should().Be(new Rectangle(0, 0, 10, 20));
        }

        [Test]
        public void PutNextRectangle_TwoRectangle_DoNotIntersect()
        {
            var rectangle1 = cloud.PutNextRectangle(new Size(10, 20));
            var rectangle2 = cloud.PutNextRectangle(new Size(20, 20));
            rectangle1.IntersectsWith(rectangle2).Should().BeFalse();
        }

        [Test]
        public void PutNextRectangle_TwoRectangle_RadiusLessThenMaxAmount()
        {
            cloud.PutNextRectangle(new Size(10, 20));
            var rectangle = cloud.PutNextRectangle(new Size(20, 20));
            var radius =
                Math.Sqrt(
                    Math.Pow(rectangle.X, 2) +
                    Math.Pow(rectangle.Y, 2));
            radius.Should().BeLessThan(30);
        }

        [Test]
        public void PutNextRectangle_OneHundredRectangle_DoNotIntersect()
        {
            for (var i = 0; i < 100; i++)
            {
                var rectangle = cloud.PutNextRectangle(new Size(10, 20));
            }
            cloud.rectangles
                .ForEach(rectangle1 => cloud.rectangles.Any(
                    rectangle2 => rectangle1.IntersectsWith(rectangle2) && rectangle1 != rectangle2).Should().BeFalse());
        }

        [TestCase(-1, 1)]
        [TestCase(1, -1)]
        public void PutNextRectangle_NegativeSize_ArgumentException(int width, int height)
        {
            var message = "The dimensions of the rectangle must be greater than or equal to zero";

            var putNextRectangle = new Action(() => cloud.PutNextRectangle(new Size(width, height)));
            
            putNextRectangle.Should().Throw<ArgumentException>().WithMessage(message);
        }
    }
}

