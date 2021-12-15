using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using TagCloud.Layout;

namespace TagsCloud_Test
{
    public class PutNextRectangleShould
    {
        private CircularCloudLayouter _layout;

        [TearDown]
        public void TearDown()
        {
            if (TestContext.CurrentContext.Result.Outcome.Status != TestStatus.Failed || _layout == null) return;
            var context = TestContext.CurrentContext;
            _layout = null;
            Console.WriteLine($"{context.Test.Name} {context.Result.Outcome.Status}");
        }

        [TestCase(0, 0)]
        [TestCase(10, 10)]
        [TestCase(-10, 10)]
        [TestCase(-10, -10)]
        [TestCase(10, -10)]
        public void SetCenter_InConstructor_ByParameter(int x, int y)
        {
            var center = new Point(x, y);
            GetLayouter(center).Center.Should().Be(center);
        }

        [Test]
        public void PutRectangle_WithCorrectSize_ByParameter()
        {
            var rectSize = new Size(50, 20);
            GetLayouter(Point.Empty).PutNextRectangle(rectSize).Size.Should().Be(rectSize);
        }

        [TestCase(-50, -20)]
        [TestCase(-50, 20)]
        [TestCase(50, -20)]
        public void Throw_OnNegativeSize(int width, int height)
        {
            var rectSize = new Size(width, height);
            Action putRect = () => GetLayouter(Point.Empty).PutNextRectangle(rectSize).Size.Should().Be(rectSize);
            putRect.Should().Throw<ArgumentException>();
        }

        [TestCase(0, 0)]
        [TestCase(0, 20)]
        [TestCase(50, 0)]
        public void Throw_OnZeroSize(int width, int height)
        {
            var rectSize = new Size(width, height);
            Action putRect = () => GetLayouter(Point.Empty).PutNextRectangle(rectSize).Size.Should().Be(rectSize);
            putRect.Should().Throw<ArgumentException>();
        }

        [TestCase(0, 0)]
        [TestCase(10, 10)]
        [TestCase(-10, 10)]
        [TestCase(-10, -10)]
        [TestCase(10, -10)]
        public void PutFirstRectangle_InCenter(int x, int y)
        {
            var center = new Point(x, y);
            var rectSize = new Size(50, 20);
            GetLayouter(center).PutNextRectangle(rectSize).IntersectsWith(new Rectangle(center, Size.Empty)).Should()
                .BeTrue();
        }

        [Test]
        public void KeepLaidRectangles()
        {
            var rectSizes = TestHelper.GenerateSizes(10);
            GetLayouter(null, rectSizes).Rectangles.Select(r => r.Size).Should().BeEquivalentTo(rectSizes);
        }

        [Test]
        public void SecondRectangle_NotInterceptWithFirst()
        {
            var rectSizes = TestHelper.GenerateSizes(2);
            var rects = GetLayouter(null, rectSizes).Rectangles;
            rects.First().IntersectsWith(rects.Last()).Should().BeFalse();
        }

        [Test]
        public void PutManyRectangles_And_HasNoIntersects()
        {
            var rectSizes = TestHelper.GenerateSizes(300);
            var layouter = GetLayouter(null, rectSizes);
            TestHelper.CheckIntersects(layouter.Rectangles.ToList()).Should().BeEmpty();
        }

        [Test]
        [Repeat(10)]
        [Timeout(20_000)]
        public void Put1000Rectangles_TakesLess2SecondsAverage()
        {
            var rectSizes = TestHelper.GenerateSizes(1000);
            GetLayouter(null, rectSizes);
        }

        [Test]
        [Timeout(2_000)]
        public void Put1000Rectangles_TakesLess2Seconds()
        {
            var rectSizes = TestHelper.GenerateSizes(1000);
            GetLayouter(null, rectSizes);
        }

        [TestCase(50)]
        [TestCase(100)]
        [TestCase(500)]
        [TestCase(1000)]
        public void PutRectangles_WithOver70PercentAverageDensity(int amount)
        {
            var targetFactor = 0.70;
            var testAmount = 10;
            double sumFactor = 0;
            for (var i = 0; i < testAmount; i++)
            {
                var rectSizes = TestHelper.GenerateSizes(amount);
                var layouter = GetLayouter(null, rectSizes);
                sumFactor += TestHelper.GetDensityFactor(layouter.Rectangles.ToList(), layouter.Center);
            }

            var actualFactor = Math.Round(sumFactor / testAmount, 2);
            actualFactor.Should().BeGreaterThan(targetFactor);
        }

        [Test]
        public void PutVeryBigRectangle_WithOver75PercentAverageDensity()
        {
            var targetFactor = 0.75;
            var testAmount = 10;
            double sumFactor = 0;
            for (var i = 0; i < testAmount; i++)
            {
                var rectSizes = TestHelper.GenerateSizes_WithOneVeryBig(100);

                var layouter = GetLayouter(null, rectSizes);
                sumFactor += TestHelper.GetDensityFactor(layouter.Rectangles.ToList(), layouter.Center);
            }

            var actualFactor = Math.Round(sumFactor / testAmount, 2);
            actualFactor.Should().BeGreaterThan(targetFactor);
        }

        private CircularCloudLayouter GetLayouter(Point? center)
        {
            return new CircularCloudLayouter(center ?? Point.Empty, new ArchimedeanSpiral(center ?? Point.Empty));
        }

        private CircularCloudLayouter GetLayouter(Point? center, List<Size> rectSizes)
        {
            return GetLayouter(center ?? Point.Empty, rectSizes, new ArchimedeanSpiral(center ?? Point.Empty));
        }

        private CircularCloudLayouter GetLayouter(Point? center, List<Size> rectSizes, ICurve curve)
        {
            center ??= Point.Empty;
            var layouter = new CircularCloudLayouter(center.Value, curve);
            rectSizes.ForEach(s => layouter.PutNextRectangle(s));
            _layout = layouter;
            return layouter;
        }
    }
}