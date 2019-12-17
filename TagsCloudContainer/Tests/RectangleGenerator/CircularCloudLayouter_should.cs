using System;
using System.Drawing;
using System.IO;
using FluentAssertions;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using TagsCloudContainer.RectangleGenerator;
using TagsCloudContainer.RectangleGenerator.PointGenerator;
using TagsCloudContainer.Visualization;

namespace TagsCloudContainer.Tests.RectangleGenerator
{
    class CircularCloudLayouter_Should
    {
        private static Point center;
        private static CircularCloudLayouter layouter;
        private IPointGenerator pointGenerator;

        [SetUp]
        public void SetUp()
        {
            center = new Point(10, 10);
            pointGenerator = new SpiralGenerator(center);
            layouter = new CircularCloudLayouter(pointGenerator);
        }

        [Test]
        public void Constructor_WhenInitialized_CenterIsPointInInitialized()
        {
            layouter.Center.Should().Be(center);
        }

        [TestCase(-10, -10, TestName = "Constructor_WhenInitializedPointWithNegativeCoords_ThrowArgumentException")]
        [TestCase(-10, 10, TestName = "Constructor_WhenInitializedPointWithNegativeX_ThrowArgumentException")]
        [TestCase(10, -10, TestName = "Constructor_WhenInitializedPointWithNegativeY_ThrowArgumentException")]
        public void Constructor_WhenInitializedPointWithNegativeCoords_ThrowArgumentException(int x, int y)
        {
            var center = new Point(x, y);
            pointGenerator = new SpiralGenerator(center);
            Action action = () =>
            {
                // ReSharper disable once ObjectCreationAsStatement
                new CircularCloudLayouter(pointGenerator);
            };
            action.Should().Throw<ArgumentException>();
        }

        [TestCase(0, 0, TestName = "PutNextRectangle_WhenSizeIsZero_ThrowArgumentException")]
        [TestCase(-5, -5, TestName = "PutNextRectangle_WhenSizeIsNegative_ThrowArgumentException")]
        [TestCase(-5, 0, TestName = "PutNextRectangle_WhenWidthIsNegativeHeightIsZero_ThrowArgumentException")]
        [TestCase(-5, 5, TestName = "PutNextRectangle_WhenHeightIsNegativeHeightIsPositive_ThrowArgumentException")]
        public void PutNextRectangle_WhenSizeIncorrect_ThrowArgumentException(int width, int height)
        {
            Action action = () =>
            {
                // ReSharper disable once ObjectCreationAsStatement
                layouter.PutNextRectangle(new Size(width, height));
            };
            action.Should().Throw<ArgumentException>();
        }

        [Test]
        public void PutNextRectangle_WhenPutOneRectangle_RectangleCenterShouldBeCenterCircular()
        {
            var size = new Size(4, 4);
            var rectangle = layouter.PutNextRectangle(size);
            rectangle.GetCenter().Should().Be(center);
        }

        [Test]
        public void PutNextRectangle_WhenPutOneRectangle_RectangleBeSameSize()
        {
            var size = new Size(4, 4);
            var rectangle = layouter.PutNextRectangle(size);
            rectangle.Size.Should().Be(size);
        }

        [Test]
        public void PutNextRectangle_WhenPutTwoRectangle_RectanglesDosntIntersects()
        {
            var random = new Random(0);
            for (var i = 0; i < 50; i++)
            {
                var firstSize = new Size(random.Next(10, 50), random.Next(10, 100));
                var secondSize = new Size(random.Next(10, 50), random.Next(10, 100));

                var rectangle1 = layouter.PutNextRectangle(firstSize);
                var rectangle2 = layouter.PutNextRectangle(secondSize);

                Assert.False(rectangle2.IntersectsWith(rectangle1), $"{rectangle2} intersects with {rectangle1}");
            }
        }

        [Test]
        public void PutNextRectangle_WhenPutEnoughRectangles_RectanglesShouldBeLikeCircle()
        {
            pointGenerator = new SpiralGenerator(new Point(500, 500));
            layouter = new CircularCloudLayouter(pointGenerator);
            var random = new Random(0);
            var areaRectangles = 0;
            double radius = 0;

            for (var i = 0; i < 100; i++)
            {
                var size = new Size(random.Next(10, 50), random.Next(10, 50));
                var rect = layouter.PutNextRectangle(size);
                areaRectangles += rect.Height * rect.Width;

                var currentRadius = GetDistance(layouter.Center, rect.GetCenter());
                if (currentRadius > radius)
                    radius = currentRadius;
            }

            var expectedRadius = Math.Sqrt(areaRectangles / Math.PI) * 1.2;
            radius.Should().BeLessThan(expectedRadius);
        }

        [Test]
        public void PutNextRectangle_WhenPutEnoughRectangles_RectanglesShouldBeDense()
        {
            layouter = new CircularCloudLayouter(pointGenerator);
            var random = new Random(0);

            for (var i = 0; i < 100; i++)
            {
                var size = new Size(random.Next(10, 50), random.Next(10, 50));
                var rect = layouter.PutNextRectangle(size);

                var direction = center - (Size) rect.GetCenter();

                var recMoveXToCenter = new Rectangle(rect.Location, rect.Size);
                var recMoveYToCenter = new Rectangle(rect.Location, rect.Size);
                recMoveXToCenter.Offset(new Point(Math.Sign(direction.X), 0));
                recMoveYToCenter.Offset(new Point(0, Math.Sign(direction.Y)));

                recMoveXToCenter.IntersectsWith(layouter.Rectangles).Should().BeTrue();
                recMoveYToCenter.IntersectsWith(layouter.Rectangles).Should().BeTrue();
            }
        }

        public double GetDistance(Point point, Point othPoint)
        {
            return Math.Sqrt(Math.Pow(point.X - othPoint.X, 2) + Math.Pow(point.Y - othPoint.Y, 2));
        }
        
        [TearDown]
        public void TearDown()
        {
            if (TestContext.CurrentContext.Result.Outcome.Status != TestStatus.Failed)
                return;

            var visualizer = new Visualizer(TagsCloudSetting.GetDefault());
            visualizer.DrawRectangles(layouter.Rectangles);
            var path = Path.Combine(TestContext.CurrentContext.TestDirectory, $"{TestContext.CurrentContext.Test.Name}.bmp");
            visualizer.Save().Save(path);
            Console.WriteLine($"Error Tests TagCloud saved to file {path}");
        }
    }
}