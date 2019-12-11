using System;
using System.Collections.Generic;
using System.Drawing;
using Autofac;
using FluentAssertions;
using FluentAssertions.Extensions;
using NUnit.Framework;
using TagsCloudVisualization.Logic;
using TagsCloudVisualization.Services;

namespace TagsCloudVisualization.Tests
{
    [TestFixture]
    public class CircularCloudLayouterTests
    {
        private CircularCloudLayouter layouter;

        [OneTimeSetUp]
        public void InitializeLayouter()
        {
            var defaultContainer = EntryPoint.InitializeContainer();
            layouter = defaultContainer.Resolve<ILayouter>() as CircularCloudLayouter;
        }

        [TearDown]
        public void ResetLayouter()
        {
            layouter.Reset();
        }

        [TestCase(0, 1, TestName = "Width is zero")]
        [TestCase(1, 0, TestName = "Height is zero")]
        [TestCase(-1, 1, TestName = "Width is negative number")]
        [TestCase(1, -1, TestName = "Height is negative number")]
        public void PutNextRectangle_ThrowsArgumentException(int width, int height)
        {
            Func<Rectangle> act = () => layouter.PutNextRectangle(new Size(width, height));
            act.Should().Throw<ArgumentException>();
        }

        [Test]
        public void PutNextRectangle_ReturnsRectangleWithPositionShiftedByOffsets()
        {
            var recSize = new Size(10, 10);
            var expectedShiftedCenter =
                new Point(-recSize.Width / 2, -recSize.Height / 2);
            var rectangle = layouter.PutNextRectangle(recSize);

            new Point(rectangle.X, rectangle.Y).Should().Be(expectedShiftedCenter);
        }

        [TestCase(1, 1, TestName = "Rectangle is the smallest possible rectangle")]
        [TestCase(42, 19, TestName = "Rectangle with non-unique sizes")]
        [TestCase(int.MaxValue, int.MaxValue, TestName = "Rectangle has maximum size")]
        public void PutNextRectangle_ReturnsRectangleWithCorrectSize(int width, int height)
        {
            var rectSize = new Size(width, height);

            layouter.PutNextRectangle(rectSize).Size.Should().Be(rectSize);
        }


        [TestCase(2, TestName = "2 rectangles created")]
        [TestCase(10, TestName = "10 rectangles created")]
        [TestCase(100, TestName = "100 rectangles created")]
        public void PutNextRectangles_ShouldNotReturnCrossingRectangles(int rectanglesAmount)
        {
            var rectangles = new List<Rectangle>();

            for (var i = 1; i < rectanglesAmount; i++)
                rectangles.Add(layouter.PutNextRectangle(new Size(i, i)));

            for (var i = 0; i < rectangles.Count; i++)
            for (var j = 0; j < i; j++)
                rectangles[i].IntersectsWith(rectangles[j]).Should().BeFalse();
        }

        [Test]
        public void PutNextRectangle_ReturnsRectangleCloseToCenter_IfSmallRectangleIsAfterLargeRectangle()
        {
            var centerRect = layouter.PutNextRectangle(new Size(5, 5));
            layouter.PutNextRectangle(new Size(100, 100));
            var smallRect = layouter.PutNextRectangle(new Size(5, 5));

            var distance = Math.Sqrt(Math.Pow(smallRect.X - centerRect.X, 2) + Math.Pow(smallRect.Y - centerRect.Y, 2));

            distance.Should().BeLessThan(10);
        }

        [TestCase(100, 1000, TestName = "100 rectangles created in less than 1 second")]
        [TestCase(500, 5000, TestName = "500 rectangles created in less than 5 second")]
        [TestCase(1000, 10000, TestName = "1000 rectangles created in less than 10 second")]
        public void PutNextRectangle_IsTimePermissible(int rectanglesAmount, int milliseconds)
        {
            var rnd = new Random();
            Action action = () =>
            {
                for (var i = 0; i < rectanglesAmount; i++)
                    layouter.PutNextRectangle(new Size(5 + rnd.Next(40), 5 + rnd.Next(40)));
            };

            action.ExecutionTime().Should().BeLessThan(milliseconds.Milliseconds());
        }
    }
}