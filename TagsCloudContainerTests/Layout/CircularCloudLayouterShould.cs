using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Mime;
using FluentAssertions;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using TagsCloudContainer.Drawing;
using TagsCloudContainer.Extensions;
using TagsCloudContainer.Layout;


namespace TagsCloudVisualizationTests
{
    [TestFixture]
    public class CircularCloudLayouterShould
    {
        private CircularCloudLayout layouter;

        [SetUp]
        public void SetUp()
        {
            layouter = new CircularCloudLayout(512, 512, 1024, 1024);
        }

        [Test]
        public void NotConatinIntersectedRectangles()
        {
            var random = new Random();
            for (var i = 0; i < 100; i++)
            {
                var randomSize = new Size(random.Next(3, 30), random.Next(3, 20));
                layouter.PutNextRectangle(randomSize);
            }

            foreach (var rectangle1 in layouter.Rectangles)
                foreach (var rectangle2 in layouter.Rectangles)
                    if (rectangle1 != rectangle2)
                        rectangle1.IntersectsWith(rectangle2).Should().BeFalse();
        }

        [Test]
        public void HaveTheFormOfCirle()
        {
            layouter.PutNextRectangle(new Size(10, 5));
            layouter.PutNextRectangle(new Size(10, 10));
            layouter.PutNextRectangle(new Size(10, 5));
            layouter.PutNextRectangle(new Size(10, 10));
            var expectedRadius = 21;

            var actualMaxRadius = double.MinValue;

            foreach (var rectangle in layouter.Rectangles)
            {
                var currentMaxRadius = rectangle
                    .Vertices()
                    .Select(p =>
                        Math.Sqrt(Math.Pow(layouter.Center.X - p.X, 2) +
                                  Math.Pow(layouter.Center.Y - p.Y, 2)))
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
            Action action = () => layouter.PutNextRectangle(new Size(1028, 10));

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
                layouter.PutNextRectangle(randomSize);
                rectanglesArea += randomSize.Width * randomSize.Height;
            }

            rectanglesArea.Should().BeLessOrEqualTo(0.7 * layouter.Area);
        }

        //[TearDown]
        //public void TearDown()
        //{
        //    if (TestContext.CurrentContext.Result.Outcome == ResultState.Failure)
        //    {
        //        var currentDirectory = Path.GetDirectoryName(MediaTypeNames.Application.ExecutablePath);
        //        var imagePath = Path.Combine(currentDirectory, $"{TestContext.CurrentContext.Test.Name}-fail.png");

        //        new ImageWriter().Write(new ImageDrawer().Draw(layouter), imagePath);
        //        Console.WriteLine($"Tag cloud visualization saved to file {imagePath}");
        //    }
        //}
    }
}