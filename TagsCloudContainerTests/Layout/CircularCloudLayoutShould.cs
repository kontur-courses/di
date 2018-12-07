using System;
using System.Drawing;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using TagsCloudContainer.Extensions;
using TagsCloudContainer.Layout;

namespace TagsCloudContainerTests.Layout
{
    [TestFixture]
    public class CircularCloudLayoutShould
    {
        private CircularCloudLayout layout;

        [SetUp]
        public void SetUp()
        {
            layout = new CircularCloudLayout(512, 512, 1024, 1024);
        }

        [Test]
        public void NotContainIntersectedRectangles()
        {
            var random = new Random();
            for (var i = 0; i < 100; i++)
            {
                var randomSize = new Size(random.Next(3, 30), random.Next(3, 20));
                layout.PutNextRectangle(randomSize);
            }

            foreach (var rectangle1 in layout.Rectangles)
                foreach (var rectangle2 in layout.Rectangles)
                    if (rectangle1 != rectangle2)
                        rectangle1.IntersectsWith(rectangle2).Should().BeFalse();
        }

        [Test]
        public void HaveTheFormOfCircle()
        {
            layout.PutNextRectangle(new Size(10, 5));
            layout.PutNextRectangle(new Size(10, 10));
            layout.PutNextRectangle(new Size(10, 5));
            layout.PutNextRectangle(new Size(10, 10));
            var expectedRadius = 21;

            var actualMaxRadius = double.MinValue;

            foreach (var rectangle in layout.Rectangles)
            {
                var currentMaxRadius = rectangle
                    .Vertices()
                    .Select(p =>
                        Math.Sqrt(Math.Pow(layout.Center.X - p.X, 2) +
                                  Math.Pow(layout.Center.Y - p.Y, 2)))
                    .Max();

                actualMaxRadius = Math.Max(actualMaxRadius, currentMaxRadius);
            }

            actualMaxRadius.Should().BeLessOrEqualTo(expectedRadius);
        }

        [Test]
        public void NotHaveInvalidCloudSize()
        {
            Action action = () => new CircularCloudLayout(new Point(0, 0), new Size(-100, 12));

            action.Should().Throw<ArgumentException>();
        }

        [Test]
        public void NotPutBigSizeRectangles()
        {
            Action action = () => layout.PutNextRectangle(new Size(1028, 10));

            action.Should().Throw<ArgumentException>();
        }

        [Test]
        public void PlaceRectanglesTightly()
        {
            var random = new Random();
            double rectanglesArea = 0;
            for (var i = 0; i < 100; i++)
            {
                var randomSize = new Size(random.Next(3, 70), random.Next(3, 50));
                layout.PutNextRectangle(randomSize);
                rectanglesArea += randomSize.Width * randomSize.Height;
            }

            rectanglesArea.Should().BeLessOrEqualTo(0.7 * layout.Area);
        }
    }
}