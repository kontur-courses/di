using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading;
using FluentAssertions;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using TagsCloudVisualization;
using TagsCloudVisualization.Layouters;
using TagsCloudVisualizationTest.Builders;
using static FluentAssertions.FluentActions;


namespace TagsCloudVisualizationTest
{
    [TestFixture]
    public class CircularCloudLayouter_Should
    {
        private readonly ThreadLocal<List<Rectangle>> rectangles = new ThreadLocal<List<Rectangle>>(() => new List<Rectangle>());

        [TearDown]
        public void TearDown()
        {
            var context = TestContext.CurrentContext;
            var methodAttr = typeof(CircularCloudLayouter_Should)
                .GetMethod(context.Test.MethodName)
                ?.GetCustomAttributes(true)
                .OfType<SaveBitmapWhenFailure>()
                .FirstOrDefault();
            
            if (TestContext.CurrentContext.Result.Outcome.Status == TestStatus.Failed && !(methodAttr is null))
            {
                var outputText = methodAttr.TrySave(rectangles.Value, context.Test.MethodName)
                    ? $"Tag cloud visualization saved to file <{methodAttr.SavePath}>"
                    : "Tag cloud too big to save to file";

                TestContext.Out.WriteLine(outputText);
            }
            
            if (rectangles.IsValueCreated)
                rectangles.Value.Clear();
        }
        
        [TestCase(-1, -1)]
        [TestCase(-1, 0)]
        [TestCase(-1, 1)]
        [TestCase(0, -1)]
        [TestCase(0, 0)]
        [TestCase(0, 1)]
        [TestCase(1, -1)]
        [TestCase(1, 0)]
        [TestCase(1, 1)]
        public void NotThrowAnyException_OnAnyCenterPoint_InConstructor(int x, int y)
        {
            var builder = CircularCloudLayouterBuilder
                .ACircularCloudLayouter()
                .WithCenterAt(new Point(x, y));

            Invoking(() => builder.Build()).Should().NotThrow($"X = {x}; Y = {y}");
        }
        
        [Test]
        public void NotThrowAnyException_OnPositiveSize_InPutNextRectangle()
        {
            var layouter = CircularCloudLayouterBuilder
                .ACircularCloudLayouter()
                .WithCenterAt(Point.Empty)
                .Build() as ILayouter<Rectangle>;
            
            Invoking(() => layouter.PutNextRectangle(new Size(1, 1))).Should().NotThrow();
        }
        
        [TestCase(-1, -1)]
        [TestCase(-1, 0)]
        [TestCase(0, -1)]
        [TestCase(0, 0)]
        [TestCase(1, 0)]
        [TestCase(0, 1)]
        [TestCase(1, -1)]
        [TestCase(-1, 1)]
        public void ThrowArgumentException_OnNonPositiveWidthOrHeight_InPutNextRectangle(int width, int height)
        {
            var layouter = CircularCloudLayouterBuilder
                .ACircularCloudLayouter()
                .WithCenterAt(Point.Empty)
                .Build() as ILayouter<Rectangle>;
            
            Invoking(() => layouter.PutNextRectangle(new Size(width, height)))
                .Should()
                .Throw<ArgumentException>($"width = {width}, height = {height}");
        }
        
        [Test]
        public void NotThrowAnyException_OnNonNullParameter_InConstructor()
        {
            var builder = CircularCloudLayouterBuilder
                .ACircularCloudLayouter()
                .WithCenterAt(Point.Empty)
                .WithDegreesDelta(1)
                .WithDensityParameter(1);

            Invoking(() => builder.Build()).Should().NotThrow();
        }
        
        [Test]
        public void ThrowArgumentException_OnNullParameter_InConstructor()
        {
            Invoking(() => new CircularCloudLayouter(null))
                .Should()
                .Throw<ArgumentException>();
        }
        
        [TestCase(-1, -1)]
        [TestCase(-1, 0)]
        [TestCase(-1, 1)]
        [TestCase(0, -1)]
        [TestCase(0, 0)]
        [TestCase(0, 1)]
        [TestCase(1, -1)]
        [TestCase(1, 0)]
        [TestCase(1, 1)]
        [SaveBitmapWhenFailure]
        public void PlaceFirstRectangleOnTheMiddle_InPutNextRectangle(int x, int y)
        {
            var layouter = CircularCloudLayouterBuilder
                .ACircularCloudLayouter()
                .WithCenterAt(new Point(x, y))
                .Build() as ILayouter<Rectangle>;
            var rectangleSize = new Size(100, 100);

            var actual = layouter.PutNextRectangle(rectangleSize);
            rectangles.Value.Add(actual);

            actual.Should().Be(
                new Rectangle(x - 50, y - 50, 100, 100), 
                $"Point is {Point.Empty.ToString()} and rectangle size is {rectangleSize.ToString()}"
                );
        }

        [Test]
        [Repeat(20)]
        [SaveBitmapWhenFailure]
        public void PlaceRectanglesWithoutIntersects_InPutNextRectangle_AutoTest()
        {
            var rnd = new Random();
            var layouter = CircularCloudLayouterBuilder
                .ACircularCloudLayouter()
                .WithCenterAt(Point.Empty)
                .Build() as ILayouter<Rectangle>;
            
            var lastRectangle = layouter.PutNextRectangle(new Size(200, 200));

            for (var i = 0; i < 1000; i++)
            {
                var randomSize = new Size(rnd.Next(1, 1000), rnd.Next(1, 1000));
                var rectangle = layouter.PutNextRectangle(randomSize);
                rectangles.Value.Add(rectangle);

                lastRectangle.IntersectsWith(rectangle).Should().BeFalse($"on try {i}");
            }
        }
        
        [Test]
        [Repeat(100)]
        [SaveBitmapWhenFailure]
        public void PlaceRectanglesCircular_InPutNextRectangle_AutoTest()
        {
            var rnd = new Random();
            var layouter = CircularCloudLayouterBuilder
                .ACircularCloudLayouter()
                .WithCenterAt(Point.Empty)
                .Build() as ILayouter<Rectangle>;

            for (var i = 0; i < 1000; i++)
            {
                rectangles.Value.Add(layouter.PutNextRectangle(new Size(rnd.Next(25, 300), rnd.Next(25, 300))));
            }

            var smallCircleRadius = rectangles.Value
                .Select(x => new[]
                    {
                        Math.Abs(x.Top),
                        Math.Abs(x.Bottom),
                        Math.Abs(x.Left),
                        Math.Abs(x.Right)
                    }.Max()
                ).Max();
                
            var bigCircleRadius = rectangles.Value
                .Select(x => new []
                    {
                        x.Location.MetricTo(Point.Empty),
                        new Point(x.X, x.Y + x.Height).MetricTo(Point.Empty),
                        new Point(x.X + x.Width, x.Y).MetricTo(Point.Empty),
                        new Point(x.X + x.Width, x.Y + x.Height).MetricTo(Point.Empty)
                    }.Max()
                ).Max();

            var outsideCircleSquare = Math.Sqrt(2 * Math.Pow(bigCircleRadius, 2));
            var actualRatio = outsideCircleSquare / bigCircleRadius;
            var expectedRatio = bigCircleRadius / smallCircleRadius;

            actualRatio.Should().BeGreaterThan(expectedRatio * 1.3);
        }
        
        [Test]
        [Repeat(100)]
        [SaveBitmapWhenFailure]
        public void PlaceRectanglesCompact_InPutNextRectangle_AutoTest()
        {
            var rnd = new Random();
            var layouter = CircularCloudLayouterBuilder
                .ACircularCloudLayouter()
                .WithCenterAt(Point.Empty)
                .Build() as ILayouter<Rectangle>;

            for (var i = 0; i < 100; i++)
            {
                rectangles.Value.Add(layouter.PutNextRectangle(new Size(rnd.Next(3, 105-i), rnd.Next(3, 105-i))));
            }

            var rectanglesSquareSum = (ulong)rectangles.Value.Select(x => x.Height * x.Width).Sum();
            var outsideRectangleSquare = (ulong)Math.Abs(rectangles.Value.Max(x => x.Right) - rectangles.Value.Min(x => x.Left)) 
                                         * (ulong)Math.Abs(rectangles.Value.Max(x => x.Bottom) - rectangles.Value.Min(x => x.Top));

            var actualRatio = (double)rectanglesSquareSum / outsideRectangleSquare;
            var expectedMaxRatio = 0.16;
            
            // To check the reaction to test failure, uncomment the next line.
            // expectedMaxRatio = 2d;

            actualRatio.Should().BeGreaterThan(expectedMaxRatio);
        }
    }
}