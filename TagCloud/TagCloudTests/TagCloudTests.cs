using System;
using System.Drawing;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using TagCloud2;

namespace TagCloudTests
{
    [TestFixture]
    public class Tests
    {
        private SpiralTagCloudEngine _engine = null!;

        [SetUp]
        public void Setup()
        {
            _engine = new SpiralTagCloudEngine(new Point(1280 / 2, 768 / 2));
        }

        [TearDown]
        public void TearDown()
        {
            if (TestContext.CurrentContext.Result.Outcome.Status != TestStatus.Failed)
                return;

            SpiralTagCloudBitmapDrawer drawer = new(
                new Size(1280, 768),
                "Consolas",
                80,
                40,
                Color.White,
                Color.White,
                Color.Black);
            drawer.DrawRectangles(_engine.Rectangles.ToArray());
            drawer.Bitmap.Save($"{TestContext.CurrentContext.Test.Name}.jpg");
            Console.WriteLine($"Test {TestContext.CurrentContext.Test.Name} saved to \"{TestContext.CurrentContext.Test.Name}.jpg\"");
        }

        [Test]
        public void PutNextRectangle_ShouldPasteNotIntersect()
        {
            for (var i = 0; i < 50; i++)
                _engine.PutNextRectangle(new Size(200, 60));

            foreach (var rect1 in _engine.Rectangles)
                foreach (var rect2 in _engine.Rectangles.Where(r => rect1 != r))
                    rect1.IntersectsWith(rect2).Should().Be(false);
        }

        [Test]
        public void PutNextRectangle_ShouldPasteNearToCenter()
        {
            const int radius = 400;
            const int centerX = 1280 / 2;
            const int centerY = 768 / 2;

            //https://www.geeksforgeeks.org/find-if-a-point-lies-inside-or-on-circle/
            Func<int, int, bool> IsPointInsideTheCircle = (x, y) =>
                (x - centerX) * (x - centerX) +
                (y - centerY) * (y - centerY) <= radius * radius;

            for (var i = 0; i < 50; i++)
                _engine.PutNextRectangle(new Size(50, 50));

            foreach (var rect1 in _engine.Rectangles)
            {
                IsPointInsideTheCircle(
                    rect1.Left + rect1.Width / 2,
                    rect1.Top + rect1.Height / 2)
                    .Should().Be(true);
            }
        }

        [Test]
        public void CreateTagCloud_ShouldNotThrow_WhenSameWeights()
        {
            var maxFontSize = 80;
            var minFontSize = 20;
            var size = new Size(1280, 768);

            var cloud = new SpiralTagCloud(
                new SpiralTagCloudEngine(new Point(size / 2)),
                new SpiralTagCloudBitmapDrawer(
                    size,
                    "Consolas",
                    80,
                    40,
                    Color.White,
                    Color.White,
                    Color.Black),
                new DataParser(),
                minFontSize,
                maxFontSize
            );

            cloud.Parser.ParseText("q\nw\ne\nr\nt\ny");
            Action action = () => cloud.CreateTagCloud();
            action.Should().NotThrow();
        }

        [Test, Timeout(3000)]
        public void PutNextRectangle_ShouldBeFast_WhenInsertingManyItems()
        {
            for (var i = 0; i < 500; i++)
                _engine.PutNextRectangle(new Size(200, 60));
        }

        [Test]
        public void PutNextRectangle_ShouldAddRectangle_WhenInsertingItems()
        {
            for (var i = 0; i < 500; i++)
                _engine.PutNextRectangle(new Size(200, 60));

            _engine.Rectangles.Count.Should().Be(500);
        }

        [TestCase(500, 500, 0, 60, TestName = "bad rectangle size")]
        [TestCase(500, 500, 200, -1, TestName = "bad rectangle size")]
        public void PutNextRectangle_ShouldThrow_WhenBadRectSize(int px, int py, int rw, int rh)
        {
            Action a = () => _engine.PutNextRectangle(new Size(rw, rh));

            a.Should().Throw<ArgumentException>();
        }

        [TestCase(0, 500, TestName = "bad point")]
        [TestCase(500, -1, TestName = "bad point")]
        public void SpiralTagCloudEngineCtor_ShouldThrow_WhenBadPoint(int px, int py)
        {
            Action a = () => new SpiralTagCloudEngine(new Point(px, py));
            a.Should().Throw<ArgumentException>();
        }
    }
}
