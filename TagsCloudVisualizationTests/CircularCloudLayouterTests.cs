using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using FluentAssertions.Execution;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using TagsCloudVisualization;

namespace TagsCloudVisualizationTests
{
    [TestFixture]
    public class CircularCloudLayouterTests
    {
        private static readonly Point CanvasCenter = new(0, 0);
        private static readonly Size MinRectangle = new(20, 15);
        private static readonly Size MaxRectangle = new(200, 100);

        private RectangleGenerator generator;
        private CircularCloudLayouter layouter;
        private List<Rectangle> rectangles;
        private Painter painter;

        [SetUp]
        public void SetUp()
        {
            generator = new RectangleGenerator(MinRectangle, MaxRectangle);
            layouter = new CircularCloudLayouter(CanvasCenter);
            rectangles = new List<Rectangle>();
            painter = new Painter(new Size(1920, 1080));
        }

        [Test, Category("DrawImageWhenFail")]
        public void PutNextRectangle_RectanglesShouldBeCompact_For1000Rectangles()
        {
            double square = 0;
            for (var i = 0; i < 1000; i++)
            {
               
                var rectangle = layouter.PutNextRectangle(generator.GetRandomRectangle());
                square += rectangle.Size.Width * rectangle.Size.Height;
                rectangles.Add(rectangle);
            }

            var minX = rectangles.Min(t => t.Left);
            var maxX = rectangles.Max(t => t.Right);
            var minY = rectangles.Min(t => t.Top);
            var maxY = rectangles.Max(t => t.Bottom);

            var width = maxX - minX;
            var height = maxY - minY;
            var radius = Math.Max(width, height) / 2.0;
            var circleSquare = Math.PI * Math.Pow(radius, 2);

            (square / circleSquare).Should().BeGreaterThan(0.7);
        }


        [Test, Category("DrawImageWhenFail")]
        public void PutNextRectangle_RectanglesShouldNotBeDiscrete_ForCenter()
        {
            rectangles.Add(layouter.PutNextRectangle(generator.GetRandomRectangle()));
            using (new AssertionScope())
            {
                for (var i = 0; i < 100; i++)
                {
                    var rectangle = layouter.PutNextRectangle(generator.GetRandomRectangle());

                    var closerRectangle = new Rectangle(rectangle.Location, rectangle.Size);
                    closerRectangle.Y += Math.Sign(CanvasCenter.Y - rectangle.Center().Y);
                    closerRectangle.X += Math.Sign(CanvasCenter.X - rectangle.Center().X);

                    var result = closerRectangle.IntersectsWith(rectangles);
                    result.Should().BeTrue($"Iteration: {i}, rectangles should intersect: {closerRectangle.Location} can be placed");
                    rectangles.Add(rectangle);
                }
            }
        }

        [Test, Category("DrawImageWhenFail")]
        public void PutNextRectangle_ShouldNotIntersect_For100Rectangles()
        {
            using (new AssertionScope())
            {
                for (var i = 0; i < 100; i++)
                {
                    var rectangle = layouter.PutNextRectangle(generator.GetRandomRectangle());

                    var result = rectangle.IntersectsWith(rectangles);
                    result.Should().BeFalse($"Iteration: {i}, rectangles should not intersect. {rectangle.Location} wrong");

                    rectangles.Add(rectangle);
                }
            }
        }

        [Test]
        public void PutNextRectangle_ShouldThrowException_WhenRectangleSizeBiggerThanCanvas()
        {
            Action action = () =>
            {
                var layouter = new CircularCloudLayouter(CanvasCenter);
                layouter.PutNextRectangle(new Size(10000, 1000));
            };
            action.Should().Throw<Exception>();
        }


        [TestCase(0, 0)]
        [TestCase(-7, 1)]
        public void PutNextRectangle_ShouldThrowArgumentException_WhenRectangleSizeNotPositive(int width, int height)
        {
            Action action = () =>
            {
                var layouter = new CircularCloudLayouter(CanvasCenter);
                layouter.PutNextRectangle(new Size(width, height));
            };
            action.Should().Throw<ArgumentException>();
        }

        [TearDown]
        public void TearDown()
        {
            if (!TestContext.CurrentContext.Test.Properties["Category"].Contains("DrawImageWhenFail") ||
                TestContext.CurrentContext.Result.Outcome.Status != TestStatus.Failed) return;

            var path = "../../../errors/" + TestContext.CurrentContext.Test.MethodName + ".png";
            painter.DrawRectanglesToFile(rectangles, path);
            Console.WriteLine($"Tag cloud visualization saved to file {path}");
        }
    }
}
