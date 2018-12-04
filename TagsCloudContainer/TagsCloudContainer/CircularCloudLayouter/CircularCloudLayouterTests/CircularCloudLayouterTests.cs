using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using FluentAssertions;
using NUnit.Framework;

namespace TagsCloudContainer.CircularCloudLayouter.CircularCloudLayouterTests
{
    [TestFixture]
    public class CircularCloudLayouterTests
    {
        [SetUp]
        public void SetUp()
        {
            _layout = null;
        }

        [TearDown]
        public void TearDown()
        {
            if (TestContext.CurrentContext.Result.FailCount > 0 && _layout != null)
            {
                var testDir = TestContext.CurrentContext.TestDirectory;
                var methodName = TestContext.CurrentContext.Test.Name;
                var filePath = Path.Combine(testDir, "..", "..", "Tests", "BadCases", "Case" + methodName + ".jpg");

                var visualizer = new Visualizer(_layout);
                visualizer.DrawTagsCloud(filePath);
                Console.WriteLine("Tag cloud visualization saved to file: {0}", filePath);
            }

            _layout = null;
        }

        private CircularCloudLayouter _layout;

        [TestCase(TestName = "ShouldFall_AndCreateFileReport")]
        public void PutNextRectangle_IncorrectPlacement()
        {
            _layout = GetIncorrectRectanglePlacement();
            var result = IsRectanglesIntersect(_layout.GetRectangles()[0], _layout.GetRectangles()[1]);

            result.Should().BeFalse();
        }

        public static CircularCloudLayouter GetIncorrectRectanglePlacement()
        {
            var layout = new CircularCloudLayouter(new RectangleStorage(new Point(0, 0), new Direction()));
            var rect1 = new Rectangle(new Point(0, 10), new Size(20, 10));
            var rect2 = new Rectangle(new Point(-10, 10), new Size(20, 10));

            layout.GetRectangles().AddRange(new List<Rectangle> {rect1, rect2});

            return layout;
        }

        [TestCase(ExpectedResult = false)]
        public bool PutNextRectangle_CorrectInputShouldNotIntersect()
        {
            _layout = new CircularCloudLayouter(new RectangleStorage(new Point(0, 0), new Direction()));
            _layout.PutNextRectangle(new Size(69, 39));
            _layout.PutNextRectangle(new Size(68, 44));
            _layout.PutNextRectangle(new Size(85, 53));
            _layout.PutNextRectangle(new Size(110, 46));

            return IsRectanglesIntersect(_layout.GetRectangles());
        }

        [TestCase(ExpectedResult = false)]
        public bool PutNextRectangle_RandomAmountOfRectanglesShouldNotIntersect()
        {
            _layout = new CircularCloudLayouter(new RectangleStorage(new Point(0, 0), new Direction()));

            var rnd = new Random();
            for (var i = 0; i < rnd.Next(1, 30); i++)
            {
                var x = rnd.Next(60, 120);
                var y = rnd.Next(30, 60);
                if ((x + y) % 2 != 0)
                    x++;

                _layout.PutNextRectangle(new Size(x, y));
            }

            return IsRectanglesIntersect(_layout.GetRectangles());
        }

        [TestCase(10, 4)]
        public void PutNextRectangle(int width, int height)
        {
            _layout = new CircularCloudLayouter(new RectangleStorage(new Point(0, 0), new Direction()));
            var rectangle = _layout.PutNextRectangle(new Size(width, height));

            rectangle.ShouldBeEquivalentTo(new Rectangle(0, height, width, height));
        }

        [TestCase(-2, -3, "both center coordinates should be non-negative",
            TestName = "FallOn_NegativeCoordinates")]
        public void ConstructorIncorrectInput(int centerX, int centerY, string msg)
        {
            Action act = () =>
                new CircularCloudLayouter(new RectangleStorage(new Point(centerX, centerY), new Direction()));

            act.ShouldThrow<ArgumentException>()
                .WithMessage(msg);
        }

        private static bool IsRectanglesIntersect(Rectangle first, Rectangle second)
        {
            if (first.X > second.X)
            {
                var tmp = new Rectangle(first.X, first.Y, first.Width, first.Height);
                first = second;
                second = tmp;
            }

            var xIntersects = first.X + first.Width > second.X;
            var yIntersects = second.Y > first.Y - first.Height && second.Y - second.Height < first.Y;

            return xIntersects && yIntersects;
        }

        private static bool IsRectanglesIntersect(List<Rectangle> rectangles)
        {
            for (var i = 0; i < rectangles.Count; i++)
            for (var j = i + 1; j < rectangles.Count; j++)
                if (IsRectanglesIntersect(rectangles[i], rectangles[j]))
                    return true;

            return false;
        }
    }
}