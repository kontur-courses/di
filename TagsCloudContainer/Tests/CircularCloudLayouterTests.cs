using System;
using System.Drawing;
using System.IO;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using NUnit.Framework.Internal;
using TagsCloudContainer.Core.Generators;
using TagsCloudContainer.Core.Layouters;
using TagsCloudContainer.Visualization;
using TagsCloudContainer.Visualization.Painters;

namespace TagsCloudContainer.Tests
{
    [TestFixture]
    internal class CircularCloudLayouterTests
    {
        private const int X = 200;
        private const int Y = 200;
        private const string LayouterKey = "layouter";

        private static readonly IPainter Painter =
            new ConstantColorsPainter(Color.Gold, Color.SlateBlue, Color.Black);

        private static readonly Point Center = new Point(X, Y);

        [SetUp]
        public void SetUp()
        {
            SetTestProperty(LayouterKey, new CircularCloudLayouter(Center, new ArchimedeanSpiral()));
        }

        [TestCase(X - 1, Y - 1, 2, 2, Description = "Coordinates without bias")]
        [TestCase(X, Y, 1, 1, Description = "Coordinates with bias")]
        public void PutNextRectangle_CenterRectangle_OnTheFirstCall(int x, int y, int width, int height)
        {
            var expected = new Rectangle(x, y, width, height);
            var layouter = GetTestProperty<CircularCloudLayouter>(LayouterKey);

            layouter.PutNextRectangle(expected.Size).Should().Be(expected);
        }

        [Test]
        public void PutNextRectangle_RectanglesDoNotIntersect_AfterPuttingRectangles()
        {
            var layouter = GetTestProperty<CircularCloudLayouter>(LayouterKey);
            var rectangles = Enumerable.Repeat(new Size(10, 10), 30)
                .Select(layouter.PutNextRectangle)
                .ToArray();

            rectangles
                .Where(f => rectangles.Any(s => f != s && f.IntersectsWith(s)))
                .Should().BeEmpty();
        }

        [Test]
        public void PutNextRectangle_UsesOptimalPlace_OnPuttingRectangles()
        {
            const int numberOfLayers = 4;
            const int count = 4 * numberOfLayers * (numberOfLayers - 1) + 1;
            var size = new Size(50, 10);
            var maxDistance = Math.Pow(numberOfLayers, 2) * GetSquaredDiagonal(size);
            var layouter = GetTestProperty<CircularCloudLayouter>(LayouterKey);

            var rectangles = Enumerable.Repeat(size, count)
                .Select(layouter.PutNextRectangle)
                .ToArray();

            rectangles
                .Select(rect => GetSquaredDistanceTo(Center, GetCenter(rect)))
                .Where(distance => distance > maxDistance)
                .Should().BeEmpty();
        }

        [TearDown]
        public void TearDown()
        {
            var context = TestContext.CurrentContext;
            if (context.Result.Outcome.Status != TestStatus.Failed)
            {
                return;
            }

            var dir = Path.Combine(context.TestDirectory, "Failed layouts");
            Directory.CreateDirectory(dir);
            var file = Path.ChangeExtension(context.Test.Name, "png");
            var path = Path.Combine(dir, file);

            var visualizer = new TagsCloudVisualizer();
            var layouter = GetTestProperty<CircularCloudLayouter>(LayouterKey);
            var rectangles = layouter.Rectangles.ToArray();
            using (var bitmap = visualizer.Visualize(Painter.Colorize(rectangles)))
            {
                bitmap.Save(path);
            }

            TestContext.WriteLine($"Tag cloud visualization saved to file {path}");
        }

        private static double GetSquaredDistanceTo(Point point, Point other)
        {
            return Math.Pow(point.X - other.X, 2) + Math.Pow(point.Y - other.Y, 2);
        }

        private static Point GetCenter(Rectangle rectangle)
        {
            return new Point(
                rectangle.X + rectangle.Width / 2,
                rectangle.Y + rectangle.Width / 2
            );
        }

        private static double GetSquaredDiagonal(Size size)
        {
            return Math.Pow(size.Width, 2) + Math.Pow(size.Height, 2);
        }

        private static void SetTestProperty<T>(string key, T value)
        {
            TestExecutionContext.CurrentContext.CurrentTest.Properties.Add(key, value);
        }

        private static T GetTestProperty<T>(string key)
        {
            return (T) TestExecutionContext.CurrentContext.CurrentTest.Properties.Get(key);
        }
    }
}