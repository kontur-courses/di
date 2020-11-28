using System;
using System.Drawing;
using System.IO;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using TagCloud.Visualizer;

namespace TagCloud.Tests
{
    public class Tests
    {
        private Point center;
        private CircularCloudLayouter cloudLayouter;

        [SetUp]
        public void SetUp()
        {
            center = new Point(500, 500);
            cloudLayouter = new CircularCloudLayouter(center);
        }

        [Test]
        public void CircularCloudLayouter_IsFirstRectInCenter()
        {
            var resultRect = cloudLayouter.PutNextRectangle(new Size(5, 5));

            resultRect.Location.Should().Be(center);
        }

        [Test]
        public void PutNextRectangle_RectanglesDoNotIntersect_AfterAddition()
        {
            var sizes = SizesCreator.CreateSizesArray(30, 80, 30, 50, 12);

            foreach (var size in sizes) cloudLayouter.PutNextRectangle(size);

            cloudLayouter.Rectangles.Any(r => r.IntersectsWith(cloudLayouter.Rectangles.Except(new[] {r}))).Should()
                .BeFalse();
        }

        [Test]
        public void PutNextRectangle_PutRectanglesInCircleForm_AfterAddition()
        {
            var sizes = SizesCreator.CreateSizesArray(30, 80, 30, 50, 12);
            const int expectedRadius = 150;

            foreach (var size in sizes) cloudLayouter.PutNextRectangle(size);
            var mostTopRect = cloudLayouter.Rectangles.OrderBy(rect => rect.Top).First();
            var mostDownRect = cloudLayouter.Rectangles.OrderByDescending(rect => rect.Bottom).First();
            var mostLeftRect = cloudLayouter.Rectangles.OrderBy(rect => rect.Left).First();
            var mostRightRect = cloudLayouter.Rectangles.OrderByDescending(rect => rect.Right).First();
            var dy = Math.Max(center.Y - mostTopRect.Top, mostDownRect.Bottom - center.Y);
            var dx = Math.Max(center.X - mostLeftRect.Left, mostRightRect.X - center.X);
            var difBetweenDeltas = Math.Abs(dx - dy);

            dy.Should().BeLessThan(expectedRadius);
            dx.Should().BeLessThan(expectedRadius);
            difBetweenDeltas.Should().BeLessThan(expectedRadius / 2);
        }

        [TearDown]
        public void TearDown()
        {
            var testResult = TestContext.CurrentContext.Result.Outcome;
            if (testResult != ResultState.Failure) return;

            var path = Path.Combine(Directory.GetCurrentDirectory(), "..", "..", "..", "crash-reports");
            var bitmap = BitmapCreator.DrawBitmap(cloudLayouter.Rectangles);
            bitmap.Save(Path.Combine(path, $"crash-report {TestContext.CurrentContext.Test.FullName}.bmp"));
            Console.WriteLine($"Tag cloud visualization saved to file {path}");
        }
    }
}