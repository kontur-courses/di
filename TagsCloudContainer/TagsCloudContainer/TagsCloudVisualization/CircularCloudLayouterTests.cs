using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using NUnit.Framework.Interfaces;

namespace TagsCloudContainer.TagsCloudVisualization
{
    public class CircularCloudLayouterTests
    {
        private CircularCloudLayouter layouter;
        private const string FailsDirectory = "FailsImages";

        [SetUp]
        public void InitLayouter()
        {
            layouter = new CircularCloudLayouter(new SpiralPointsGenerator(new Point(100, 100)));
        }

        [TearDown]
        public void TearDown()
        {
            if (TestContext.CurrentContext.Result.Outcome.Status == TestStatus.Passed) return;
            var context = TestContext.CurrentContext;
            var fileName = context.Test.FullName + ".png";
            var path = Path.Combine("../../" + FailsDirectory, fileName);
            CloudVisualizer.Draw(layouter, new List<Color>() {Color.Black}, Color.White).Save(path, ImageFormat.Png);
            Console.WriteLine($"Tag cloud visualization saved to file {fileName} in {FailsDirectory}");
        }

        [TestCase(0, 1, TestName = "width is zero")]
        [TestCase(-1, 1, TestName = "width is negative")]
        [TestCase(1, 0, TestName = "height is zero")]
        [TestCase(1, -1, TestName = "height is negative")]
        public void PutNextRectangle_Throws_WhenSizeIsInvalid(int width, int height)
        {
            Action act = () => layouter.PutNextRectangle(new Size(width, height));

            act.Should().Throw<ArgumentException>();
        }

        [Test]
        public void PutNextRectangle_NotThrows_WhenSizeIsValid()
        {
            Action act = () => layouter.PutNextRectangle(new Size(10, 10));

            act.Should().NotThrow();
        }

        [Test]
        public void PutNextRectangle_ReturnsRectangleFromSpiralPointsGenerator()
        {
            var pointsGenerator = new SpiralPointsGenerator(new Point(100, 100));

            for (var i = 0; i < 100; i++)
            {
                var rectangle = layouter.PutNextRectangle(new Size(10, 10));
                pointsGenerator.GetSpiralPoints().Should().Contain(p => p.Equals(rectangle.Location));
            }
        }

        [Test]
        public void PutNextRectangle_ReturnsRectangleWithCorrectSize()
        {
            for (var i = 0; i < 100; i++)
            {
                var size = new Size(10, 10);
                var rectangle = layouter.PutNextRectangle(size);
                rectangle.Size.Should().Be(size);
            }
        }

        [Test]
        public void PutNextRectangle_Throws_WhenCantPutFirstRectangle()
        {
            Action act = () => layouter.PutNextRectangle(new Size(1000, 1000));

            act.Should().Throw<Exception>();
        }

        [Test]
        public void PutNextRectangle_Throws_WhenCantPutNextRectangle()
        {
            layouter.PutNextRectangle(new Size(100, 100));
            Action act = () => layouter.PutNextRectangle(new Size(300, 300));

            act.Should().Throw<Exception>();
        }

        [Test]
        public void PutNextRectangle_ReturnsRectangle_WhichNotIntersectsWithPreviousRectangles()
        {
            var rectangles = new List<Rectangle>();

            for (var i = 0; i < 100; i++)
            {
                rectangles.Add(layouter.PutNextRectangle(new Size(10, 10)));
            }

            rectangles.Where(rectangle =>
                    rectangle.IntersectsWithRectangles(rectangles.Where(r => !r.Equals(rectangle)).ToList()))
                .Should()
                .BeEmpty();
        }

        [Test, Repeat(10), Timeout(200)]
        public void PutNextRectangle_PutsThousandRectangles_InTwoHundredMilliseconds()
        {
            layouter = new CircularCloudLayouter(new SpiralPointsGenerator(new Point(300, 300)));
            for (var i = 0; i < 1000; i++)
            {
                layouter.PutNextRectangle(new Size(10, 10));
            }
        }
    }
}