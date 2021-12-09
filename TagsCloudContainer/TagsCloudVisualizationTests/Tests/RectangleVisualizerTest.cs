using System;
using System.Collections.Generic;
using System.Drawing;
using NUnit.Framework;
using TagsCloudVisualizationTests.TestingLibrary;

namespace TagsCloudVisualizationTests.Tests
{
    public class RectangleVisualizerTest
    {
        [Test]
        public void Constructor_ThrowException_WithEmptyCollection()
        {
            Assert.Throws<ArgumentException>(
                () =>
                    new RectangleVisualizer(new List<Rectangle>()));
        }

        [TestCaseSource(nameof(GetBitmapSizeAssertSizeCases))]
        public Size GetBitmapSize_AssertSize(List<Rectangle> rectangles) =>
            new RectangleVisualizer(rectangles).GetBitmapSize();

        public static IEnumerable<TestCaseData> GetBitmapSizeAssertSizeCases()
        {
            yield return new TestCaseData(
                new List<Rectangle>
                {
                    new(0, 0, 3, 4)
                }) {ExpectedResult = new Size(4, 5), TestName = "With single rectangle"};

            yield return new TestCaseData(
                new List<Rectangle>
                {
                    new(0, 0, 3, 4),
                    new(1, 3, 1, 1)
                }) {ExpectedResult = new Size(4, 5), TestName = "With one into other"};

            yield return new TestCaseData(
                new List<Rectangle>
                {
                    new(0, 0, 3, 4),
                    new(1, 3, 4, 2)
                }) {ExpectedResult = new Size(6, 6), TestName = "With two positive rectangles"};

            yield return new TestCaseData(
                new List<Rectangle>
                {
                    new(-1, -2, 3, 4),
                    new(0, 0, 1, 2)
                }) {ExpectedResult = new Size(4, 5), TestName = "With one negative rectangle"};

            yield return new TestCaseData(
                new List<Rectangle>
                {
                    new(-6, -5, 2, 3),
                    new(-5, -4, 2, 1)
                }) {ExpectedResult = new Size(4, 4), TestName = "With two negative rectangles"};
        }

        [TestCaseSource(nameof(DrawAssertBitmapCases))]
        public void Draw_AssertBitmap(List<Rectangle> rectanglesToDraw, List<Point> expectedPoints, Size bitmapSize)
        {
            var visualizer = new RectangleVisualizer(rectanglesToDraw);
            VisualizerTestHelper.AssertBitmap(visualizer, bitmapSize, expectedPoints, Color.Red);
        }

        public static IEnumerable<TestCaseData> DrawAssertBitmapCases()
        {
            var expectedPoints = new List<Point>
            {
                new(0, 0),
                new(1, 0),
                new(2, 0),
                new(2, 1),
                new(2, 2),
                new(1, 2),
                new(0, 2),
                new(0, 1)
            };

            yield return new TestCaseData(
                    new List<Rectangle>
                    {
                        new(0, 0, 2, 2)
                    },
                    expectedPoints,
                    new Size(3, 3))
                {TestName = "With positive rectangle"};

            yield return new TestCaseData(
                    new List<Rectangle>
                    {
                        new(-1, -1, 2, 2)
                    },
                    expectedPoints,
                    new Size(3, 3))
                {TestName = "With negative rectangle"};
        }
    }
}