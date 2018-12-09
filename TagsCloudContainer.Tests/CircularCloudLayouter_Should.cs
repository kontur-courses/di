using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using TagsCloudContainer.Algorithms;

namespace TagsCloudContainer.Tests
{
    [TestFixture]
    class CircularCloudLayouter_Should
    {
        private CircularCloudAlgorithm layouter;
        private int centerWidth = 1000;
        private int centerHeight = 1000;

        [SetUp]
        public void SetUp()
        {
            var center = new Point(centerWidth, centerHeight);
            layouter = new CircularCloudAlgorithm(center, new ArchimedeanSpiral(center));
        }
        [TearDown]
        public void TearDown()
        {
            if (TestContext.CurrentContext.Result.Outcome.Status == TestStatus.Failed)
            {
                var path = TestContext.CurrentContext.TestDirectory;
                var fileName = $"{TestContext.CurrentContext.Test.Name} failed.png";
                var outputFileName = Path.Combine(path, fileName);

                var size = new Size(2 * centerWidth, 2 * centerHeight);
                using (var bitmap = new Bitmap(size.Width, size.Height))
                {
                    using (var graphics = Graphics.FromImage(bitmap))
                    {
                        foreach (var rectangle in layouter.GetRectangles())
                        {
                            graphics.DrawRectangle(new Pen(Color.Red), rectangle);
                        }
                        bitmap.Save(outputFileName);
                    }
                }

                TestContext.WriteLine($"Tag cloud visualization saved to file {outputFileName}");
            }
        }

        [Test]
        public void HaveEmptyRectanglesCollection_OnInitialization()
        {
            layouter.GetRectangles().Should().BeEmpty();
        }

        [Test]
        public void ThrowArgumentException_OnPuttingNextRectangleOfEmptySize()
        {
            Action action = () => layouter.PutNextRectangle(new Size());
            action.Should().Throw<ArgumentException>();
        }

        [Test]
        public void ThrowArgumentException_OnPuttingNextRectangleOfNegativeSize()
        {
            Action action = () => layouter.PutNextRectangle(new Size(-5, -5));
            action.Should().Throw<ArgumentException>();
        }

        [Test]
        public void CreateFirstRectangle_InCenter()
        {
            var rectangle = layouter.PutNextRectangle(new Size(100, 50));
            layouter.GetRectangleCenter(rectangle).Should().Be(layouter.Center);
        }

        [Test]
        public void AddSeveralRectangles()
        {
            for (int i = 0; i < 1000; i++)
            {
                layouter.PutNextRectangle(new Size(100, 50));
            }
            layouter.GetRectangles().Count.Should().Be(1000);
        }

        [Test]
        public void AddRectanglesWithoutIntersections()
        {
            for (int i = 0; i < 1000; i++)
            {
                layouter.PutNextRectangle(new Size(100, 50));
            }

            foreach (var rectangle in layouter.GetRectangles())
            {
                layouter.GetRectangles()
                    .All(e => e.IntersectsWith(rectangle)).Should().BeFalse();
            }
        }


        [Test]
        public void RenderRoundCloud_OfHorizontallyPositionedRectangles()
        {
            for (int i = 0; i < 100; i++)
            {
                layouter.PutNextRectangle(new Size(150, 50));
            }
            var containingRectangle = GetRelativeBoundsAbs(layouter.GetRectangles());

            Assert.That(containingRectangle.rightBound, Is.EqualTo(containingRectangle.leftBound).Within(150));
            Assert.That(containingRectangle.topBound, Is.EqualTo(containingRectangle.bottomBound).Within(150));
        }

        [Test]
        public void RenderRoundCloud_OfVerticallyPositionedRectangles()
        {
            for (int i = 0; i < 100; i++)
            {
                layouter.PutNextRectangle(new Size(50, 150));
            }
            var containingRectangle = GetRelativeBoundsAbs(layouter.GetRectangles());

            Assert.That(containingRectangle.rightBound, Is.EqualTo(containingRectangle.leftBound).Within(150));
            Assert.That(containingRectangle.topBound, Is.EqualTo(containingRectangle.bottomBound).Within(150));
        }

//        [Test]
//        public void RenderRoundCloud_OfRandomGeneratedSizeRectangles()
//        {
//            var rectangles = CircularCloudLayouterGenerator.GenerateRectanglesSet(layouter, 50, 100, 150, 50, 75);
//
//            var containingRectangle = GetRelativeBoundsAbs(rectangles);
//
//            Assert.That(containingRectangle.rightBound, Is.EqualTo(containingRectangle.leftBound).Within(150));
//            Assert.That(containingRectangle.topBound, Is.EqualTo(containingRectangle.bottomBound).Within(150));
//        }

        private (int rightBound, int leftBound, int topBound, int bottomBound) GetRelativeBoundsAbs(IEnumerable<Rectangle> rectangles)
        {
            int leftBound = centerWidth, rightBound = centerWidth, topBound = centerHeight, bottomBound = centerHeight;

            foreach (var rectangle in rectangles)
            {
                if (rectangle.Location.X > rightBound)
                    rightBound = rectangle.Location.X;

                if (rectangle.Location.X < leftBound)
                    leftBound = rectangle.Location.X;

                if (rectangle.Location.Y > topBound)
                    topBound = rectangle.Location.Y;

                if (rectangle.Location.Y < bottomBound)
                    bottomBound = rectangle.Location.Y;
            }

            return (Math.Abs(rightBound - centerWidth), Math.Abs(leftBound - centerWidth),
                Math.Abs(topBound - centerHeight), Math.Abs(bottomBound - centerHeight));
        }
    }
}
