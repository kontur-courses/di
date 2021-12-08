using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using TagsCloud.Visualization;
using TagsCloud.Visualization.ContainerVisitor;
using TagsCloud.Visualization.Drawers;
using TagsCloud.Visualization.Extensions;
using TagsCloud.Visualization.LayoutContainer;
using TagsCloud.Visualization.PointGenerator;

namespace TagsCloud.Tests
{
    public class CircularCloudLayouterTests
    {
        private readonly Drawer drawer = new(new RandomColorDrawerVisitor());
        private Point center;
        private List<Rectangle> rectangles;
        private CircularCloudLayouter sut;

        [SetUp]
        public void SetUp()
        {
            rectangles = new List<Rectangle>();
            center = new Point(10, 10);
            sut = new CircularCloudLayouter(new ArchimedesSpiralPointGenerator(center));
        }

        [TearDown]
        public void TearDown()
        {
            if (TestContext.CurrentContext.Result.Outcome == ResultState.Failure
                && rectangles.Count > 0)
            {
                var testName = TestContext.CurrentContext.Test.Name;
                var rectanglesContainer = new RectanglesContainer {Items = rectangles};
                using var image = drawer.Draw(rectanglesContainer);
                var path = Path.Combine(GetDirectoryForSavingFailedTest(), $"{testName}.png");
                image.Save(path);

                Console.WriteLine(TestContext.CurrentContext.Test.Name + " failed");
                Console.WriteLine("Tag cloud visualization saved to file " + path);
            }
        }


        [TestCase(0, 1, TestName = "Only one is zero")]
        [TestCase(0, 0, TestName = "Both coordinates are zero")]
        [TestCase(-1, 3, TestName = "Negative width")]
        public void PutNextRectangle_Should_ThrowException_WhenSizeNotPositive(int width, int height)
        {
            var size = new Size(width, height);

            Assert.Throws<ArgumentException>(() => sut.PutNextRectangle(size));
        }

        [TestCase(1)]
        [TestCase(5)]
        [TestCase(10)]
        public void PutNextRectangle_RectanglesAmount_ShouldBeEqual_AmountOfAdded(int count)
        {
            rectangles = PutRandomRectangles(count);

            rectangles.Should().HaveCount(count);
        }

        [Test]
        public void PutNextRectangle_ShouldNot_ChangeSize()
        {
            var size = new Size(12, 34);

            var rectangle = sut.PutNextRectangle(size);

            rectangle.Size.Should().Be(size);
        }

        [TestCase(10, 10)]
        [TestCase(10, 1)]
        [TestCase(123, 45)]
        [TestCase(1, 200)]
        public void PutNextRectangle_FirstRectangle_Should_BePlacedInCenter(int width, int height)
        {
            var rectangle = sut.PutNextRectangle(new Size(width, height));

            var rectangleCenter = rectangle.GetCenter();

            rectangleCenter.Should().Be(center);
        }

        [Test]
        public void PutNextRectangle_Rectangles_Should_HaveDifferentCentres()
        {
            rectangles = PutRandomRectangles(10);

            rectangles.Should().OnlyHaveUniqueItems(x => x.GetCenter());
        }

        [Test]
        public void PutNextRectangle_Should_PlaceRectangles_WithoutIntersection()
        {
            rectangles = PutRandomRectangles(20);

            foreach (var (rectangle, otherRectangles) in GetItemAndListWithoutIt(rectangles))
                rectangle.IntersectsWith(otherRectangles).Should().BeFalse();
        }

        [TestCase(200, TestName = "Big count")]
        [TestCase(5, TestName = "Little count")]
        public void PutNextRectangle_Should_PlaceRectangles_CloseToRoundForm(int count)
        {
            var random = new Random();
            var width = random.Next(300);
            var height = random.Next(200);
            var size = new Size(width, height);
            var expectedSquareSqrt = Math.Sqrt(width * height * count);

            rectangles = Enumerable.Range(0, count)
                .Select(_ => sut.PutNextRectangle(size))
                .ToList();

            foreach (var rectangle in rectangles)
                rectangle.Location.GetDistance(center).Should()
                    .BeLessThan(expectedSquareSqrt, rectangle.ToString());
        }

        [TestCase(1000, TestName = "Big count")]
        [TestCase(5, TestName = "Little count")]
        public void PutNextRectangle_Should_PlaceRectangles_Tightly(int count)
        {
            rectangles = PutRandomRectangles(count);

            var expected = Math.Max(
                rectangles.Max(rectangle => rectangle.Width),
                rectangles.Max(rectangle => rectangle.Height)
            );

            foreach (var (rect, otherRects) in GetItemAndListWithoutIt(rectangles))
            {
                var minDistanceToOtherRectangles = otherRects
                    .Min(x => x.GetCenter().GetDistance(rect.GetCenter()));

                minDistanceToOtherRectangles.Should().BeLessOrEqualTo(expected);
            }
        }

        private List<Rectangle> PutRandomRectangles(int count, int maxWidth = 100, int maxHeight = 100)
        {
            var rnd = new Random();
            var sizes = Enumerable.Range(0, count)
                .Select(_ => new Size(rnd.Next(1, maxWidth), rnd.Next(1, maxHeight)));

            return sizes.Select(x => sut.PutNextRectangle(x)).ToList();
        }

        private IEnumerable<(Rectangle, IEnumerable<Rectangle>)> GetItemAndListWithoutIt(
            IReadOnlyCollection<Rectangle> rects)
        {
            return rects.Select(x => (x, rects.Where(y => y != x)));
        }

        private static string GetDirectoryForSavingFailedTest()
        {
            var solutionPath = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..\\..\\..\\..\\"));
            var path = Path.Combine(solutionPath, "FailedTestsPictures");
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
            return path;
        }
    }
}