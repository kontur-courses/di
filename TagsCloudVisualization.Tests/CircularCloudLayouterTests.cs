using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using TagsCloudVisualization.Common.Layouters;
using TagsCloudVisualization.Common.Settings;
using TagsCloudVisualization.Extensions;

namespace TagsCloudVisualization.Tests
{
    [TestFixture]
    public class CircularCloudLayouterTests
    {
        private CanvasSettings canvasSettings;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            canvasSettings = new CanvasSettings(1920, 1080, Color.Black);
        }

        [TestCaseSource(typeof(TestsData), nameof(TestsData.LayoutCenterData))]
        public void CircularCloudLayouter_LayoutCenter_ShouldEqualCanvasCenter(CanvasSettings settings)
        {
            var layout = new CircularCloudLayouter(settings);
            var expected = new Point(settings.Width / 2, settings.Height / 2);

            layout.LayoutCenter.Should().Be(expected);
        }

        [TestCaseSource(typeof(TestsData), nameof(TestsData.NegativeOrZeroSizeRectangleData))]
        public void PutNextRectangle_NegativeOrZeroSize_ShouldThrowException(Size rectSize)
        {
            Action putBadSizeRect = () => { new CircularCloudLayouter(canvasSettings).PutNextRectangle(rectSize); };

            putBadSizeRect.Should().Throw<ArgumentException>();
        }

        [TestCaseSource(typeof(TestsData), nameof(TestsData.SomeCorrectCanvasSettingsAndRectSizesData))]
        public void PutNextRectangle_FirstRectangle_ShouldBeInCenter(CanvasSettings settings, Size rectSize)
        {
            var layout = new CircularCloudLayouter(settings);

            var firstRect = layout.PutNextRectangle(rectSize);

            firstRect.GetCenter().Should().Be(layout.LayoutCenter);
        }

        [TestCaseSource(typeof(TestsData), nameof(TestsData.MultipleRandomRectSizesData),
            new object[] {100, 1, 1, 1, 100, 100})]
        public void CircularCloudLayouter_AllRectsShouldHavePassedSizes(IEnumerable<Size> rectSizes)
        {
            var layout = new CircularCloudLayouter(canvasSettings);

            var rectCount = 0;
            foreach (var rectSize in rectSizes)
            {
                var newRect = layout.PutNextRectangle(rectSize);
                newRect.Size.Should().Be(new Size(rectSize.Width, rectSize.Height)); //exclude reference equals
                rectCount++;
            }

            layout.Rects.Count().Should().Be(rectCount);
        }

        [TestCaseSource(typeof(TestsData), nameof(TestsData.MultipleRandomRectSizesData),
            new object[] {100, 1, 1, 1, 100, 100})]
        public void CircularCloudLayouter_RectsShouldNotIntersect(IEnumerable<Size> rectSizes)
        {
            var layout = new CircularCloudLayouter(canvasSettings);
            foreach (var rectSize in rectSizes)
                layout.PutNextRectangle(rectSize);

            layout.Rects
                .Any(rect1 => layout.Rects
                    .Where(rect2 => !rect1.Equals(rect2))
                    .Any(rect2 => rect2.IntersectsWith(rect1)))
                .Should().BeFalse();
        }

        [TestCaseSource(typeof(TestsData), nameof(TestsData.TightlyPackedRectanglesData))]
        public void CircularCloudLayouter_RectsShouldTightlyPacked(IEnumerable<Size> rectSizes)
        {
            var layout = new CircularCloudLayouter(canvasSettings);
            var rectsSquare = rectSizes
                .Select(rectSize => layout.PutNextRectangle(rectSize))
                .Aggregate(0d, (current, rect) => current + rect.Width * rect.Height);
            var unionRect = layout.Rects.Aggregate(Rectangle.Union);
            var unionRectSquare = unionRect.Width * unionRect.Height;

            rectsSquare.Should().Be(unionRectSquare);
        }

        [TestCaseSource(typeof(TestsData), nameof(TestsData.RectanglesFormCircleData))]
        [TestCaseSource(typeof(TestsData), nameof(TestsData.MultipleRandomRectSizesData),
            new object[] {200, 1, 10, 10, 10, 10})]
        public void CircularCloudLayouter_RectsShouldFormCircle(IEnumerable<Size> rectSizes)
        {
            var layout = new CircularCloudLayouter(canvasSettings);
            var rectsSquare = rectSizes
                .Select(rectSize => layout.PutNextRectangle(rectSize))
                .Aggregate(0d, (current, rect) => current + rect.Width * rect.Height);
            var unionRectangle = layout.Rects.Aggregate(Rectangle.Union);
            var circleRadius =
                unionRectangle.Width > unionRectangle.Height
                    ? unionRectangle.Width / 2
                    : unionRectangle.Height / 2;
            var circleSquare = Math.PI * circleRadius * circleRadius;

            unionRectangle.GetCenter().GetDistance(layout.LayoutCenter).Should().BeInRange(-0.01, 0.01);
            (rectsSquare / circleSquare).Should().BeInRange(0.85, 1.15);
        }
    }

    public class TestsData
    {
        public static IEnumerable<TestCaseData> LayoutCenterData
        {
            get
            {
                yield return new TestCaseData(new CanvasSettings(100, 100, Color.Aqua));
                yield return new TestCaseData(new CanvasSettings(10, 40, Color.Black));
                yield return new TestCaseData(new CanvasSettings(50, 5, Color.DimGray));
            }
        }

        public static IEnumerable<TestCaseData> NegativeOrZeroSizeRectangleData
        {
            get
            {
                yield return new TestCaseData(new Size(-15, -15));
                yield return new TestCaseData(new Size(15, 0));
                yield return new TestCaseData(new Size(0, 15));
                yield return new TestCaseData(new Size(0, 0));
            }
        }

        public static IEnumerable<TestCaseData> SomeCorrectCanvasSettingsAndRectSizesData
        {
            get
            {
                yield return new TestCaseData(new CanvasSettings(100, 100, Color.Aqua), new Size(15, 15));
                yield return new TestCaseData(new CanvasSettings(10, 40, Color.Black), new Size(20, 15));
                yield return new TestCaseData(new CanvasSettings(50, 5, Color.DimGray), new Size(15, 35));
            }
        }

        public static IEnumerable<TestCaseData> MultipleRandomRectSizesData(int count, int rndSeed, int minSizeX,
            int minSizeY, int maxSizeX, int maxSizeY)
        {
            var minSize = new Size(minSizeX, minSizeY);
            var maxSize = new Size(maxSizeX, maxSizeY);

            var rnd = new Random(rndSeed);
            var rectSizes = new List<Size>();
            for (var i = 0; i < count; i++)
                rectSizes.Add(new Size(
                    rnd.Next(minSize.Width, maxSize.Width),
                    rnd.Next(minSize.Width, maxSize.Width)));

            yield return new TestCaseData(rectSizes);
        }

        public static IEnumerable<TestCaseData> TightlyPackedRectanglesData
        {
            get
            {
                yield return new TestCaseData(
                    new[]
                    {
                        new Size(10, 10),
                        new Size(10, 30),
                        new Size(10, 30),
                        new Size(30, 10),
                        new Size(30, 10),
                        new Size(10, 10),
                        new Size(10, 10)
                    });
            }
        }

        public static IEnumerable<TestCaseData> RectanglesFormCircleData()
        {
            const int maxSize = 10;

            var rectSizes = new List<Size>();
            for (var i = 1; i < maxSize; i++)
            for (var y = 0; y < i + 4 * (i - 1); y++)
                rectSizes.Add(new Size(maxSize - i, maxSize - i));

            yield return new TestCaseData(rectSizes);
        }
    }
}