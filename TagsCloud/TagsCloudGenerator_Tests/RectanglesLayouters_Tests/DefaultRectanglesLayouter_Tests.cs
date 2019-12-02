using System;
using System.Linq;
using System.Drawing;
using System.Collections.Generic;
using NUnit.Framework;
using FluentAssertions;
using TagsCloudGenerator.RectanglesLayouters;
using TagsCloudGenerator.PointsSearchers;

namespace TagsCloudGenerator_Tests.RectanglesLayouters_Tests
{
    internal class DefaultRectanglesLayouter_Tests
    {
        private DefaultRectanglesLayouter sut;
        private List<RectangleF> rectangles;

        [SetUp]
        public void SetUp()
        {
            sut = new DefaultRectanglesLayouter(new PointsSearcherOnSpiral());
            rectangles = new List<RectangleF>();
        }

        [TestCase(0, 10)]
        [TestCase(10, 0)]
        [TestCase(-5, -7)]
        [TestCase(0, 0)]
        public void WhenAnyRectSizeIsZeroOrNegative_ShouldThrow(int rectWidth, int rectHeight)
        {
            Action act = () => sut.PutNextRectangle(new SizeF(rectWidth, rectHeight));

            act.Should().Throw<ArgumentException>();
        }

        [TestCase(5, 10)]
        [TestCase(0.01f, 0.01f)]
        public void ReturnedRectangleSize_ShouldBeEqualsSizeFromArgument(float rectWidth, float rectHeight)
        {
            var size = new SizeF(rectWidth, rectHeight);

            var rect = sut.PutNextRectangle(size);

            rect.Size.Should().Be(size);
        }

        [TestCase(5, 10)]
        [TestCase(6, 103)]
        [TestCase(8, 6)]
        [TestCase(7, 5)]
        public void CenterOfFirstRectangle_ShouldBeZeroCentralPoint(int rectWidth, int rectHeight)
        {
            var center = new PointF(0, 0);
            var rect = sut.PutNextRectangle(new SizeF(rectWidth, rectHeight));
            var rectCenter = new PointF(rect.X + rect.Width / 2, rect.Y + rect.Height / 2);

            rectCenter.Should().Be(center);
        }

        [TestCase(100)]
        [TestCase(20)]
        [TestCase(5)]
        public void MultipleRectangles_ShouldNotIntersectEachOther(int rectanglesCount)
        {
            for (var i = 1; i <= rectanglesCount; i++)
                rectangles.Add(sut.PutNextRectangle(new SizeF((i % 6 + 1) * 7, (i % 7 + 1) * 3)));

            foreach (var rect in rectangles)
                rectangles
                    .Where(r => !r.Equals(rect))
                    .Any(r => r.IntersectsWith(rect))
                    .Should().BeFalse();
        }
    }
}