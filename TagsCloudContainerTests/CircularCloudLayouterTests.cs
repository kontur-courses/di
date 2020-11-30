using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using TagsCloudContainer;
using TagsCloudContainer.Drawer;
using TagsCloudContainer.Layouter;

namespace TagsCloudContainerTests
{
    [TestFixture]
    public class CircularCloudLayouterTests
    {
        private Point center;
        private RectangleLayout layout;
        private CircularCloudLayouter layouter;
        private ILayoutDrawer drawer;
        private Options options;

        [SetUp]
        public void SetUp()
        {
            options = new Options(500, 500, "Arial",
                @"C:\Users\Алишер\Desktop\Kontur\Homework\di\TagsCloudContainer\test.txt",
                @"C:\Users\Алишер\Desktop\Kontur\Homework\di\TagsCloudContainer\output.txt");
            center = new Point(options.Width / 2, options.Height / 2);
            layouter = new CircularCloudLayouter();
            drawer = new LayoutDrawer(options);
            layout = new RectangleLayout(layouter, drawer, options);
        }

        // [TearDown]
        // public void TearDown()
        // {
        //     if (TestContext.CurrentContext.Result.Outcome.Status != TestStatus.Failed) return;
        //     var testName = TestContext.CurrentContext.Test.Name;
        //     options.Output = Directory.GetCurrentDirectory() + testName;
        // }


        [Test]
        public void FirstRectangle_ShouldBeCentral()
        {
            layouter.PutNextRectangle(new Size(10, 10)).Location
                .Should().Be(new Point(center.X - 5, center.Y - 5));
        }

        [Test]
        public void Rectangles_ShouldntIntersect()
        {
            var random = new Random();
            var rectangles = Enumerable
                .Range(0, 200)
                .Select(_ => new Size(random.Next(100, 200), random.Next(50, 100)))
                .Select(size => layouter.PutNextRectangle(size))
                .ToList();

            for (var i = 0; i < rectangles.Count; i++)
            {
                for (var j = i + 1; j < rectangles.Count; j++)
                    rectangles[i].IntersectsWith(rectangles[j]).Should().BeFalse();
            }
        }

        [Test]
        [TestCase(-1, -1)]
        [TestCase(0, 0)]
        public void ShouldThrowOnIncorrectRectangle(int width, int height)
        {
            Action action = () => { layouter.PutNextRectangle(new Size(width, height)); };
            action.Should().Throw<ArgumentException>().WithMessage("Rectangle size should be positive.");
        }

        [Test]
        public void PutNextRectangle_ShouldReturnCorrectRectangle()
        {
            var rng = new Random();
            for (var i = 0; i < 100; i++)
            {
                var size = new Size(rng.Next(10, 100), rng.Next(10, 100));
                layouter.PutNextRectangle(size).Size.Should().Be(size);
            }
        }

        // [Test]
        // public void Rectangles_ShouldBeInsideCircle()
        // {
        //     var random = new Random();
        //     var rectangles = Enumerable
        //         .Range(0, 200)
        //         .Select(_ => new Size(random.Next(10, 100), random.Next(10, 100)))
        //         .Select(size => new WordRectangle(layouter.PutNextRectangle(size), ""))
        //         .ToList();
        //
        //     var radius = GetCircleRadius(rectangles);
        //
        //     foreach (var rectangle in rectangles)
        //     {
        //         var distanceToCenter = GetMaximumDistance(rectangle, center);
        //         distanceToCenter.Should().BeLessThan(radius);
        //     }
        // }

        private static int GetCircleRadius(IEnumerable<WordRectangle> rectangles)
        {
            const double radiusMultiplier = 1.25;
            var square = rectangles
                .Select(rectangle => rectangle.Rectangle.Width * rectangle.Rectangle.Height)
                .Sum();
            return (int) (Math.Sqrt(square / Math.PI) * radiusMultiplier);
        }

        // private static double GetMaximumDistance(Rectangle rectangle, Point center)
        // {
        //     var maxX = Math.Max(Math.Abs(center.X - rectangle.X), Math.Abs(center.X - rectangle.X - rectangle.Width));
        //     var maxY = Math.Max(Math.Abs(center.Y - rectangle.Y), Math.Abs(center.Y - rectangle.Y - rectangle.Height));
        //     return Math.Sqrt(maxX * maxX + maxY * maxY);
        // }
    }

    internal class Options : IOptions
    {
        public int Width { get; set; }
        public int Height { get; set; }
        public string Font { get; set; }
        public string FilePath { get; set; }
        public string Output { get; set; }
        
        public Options(int width, int height, string font, string path, string output)
        {
            Width = width;
            Height = height;
            Font = font;
            FilePath = path;
            Output = output;
        }
    }
}