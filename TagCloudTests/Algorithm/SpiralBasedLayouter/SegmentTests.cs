using System.Drawing;
using FluentAssertions;
using NUnit.Framework;
using TagCloud.Algorithm.SpiralBasedLayouter;

namespace TagCloudTests.Algorithm.SpiralBasedLayouter
{
    public class SegmentTests
    {
        [Test]
        public void SegmentGetCenter_ShouldReturnCenter_WhenItIsInteger()
        {
            var segment = new Segment(new Point(0, 0), new Point(2, 0));
            var expectedCenter = new PointF(1.0f, 0f);

            var result = segment.GetCenter();

            result.Should().Be(expectedCenter);

        }

        [Test]
        public void SegmentGetCenter_ShouldReturnCenter_WhenItIsDouble()
        {
            var segment = new Segment(new Point(0, 0), new Point(1, 0));
            var expectedCenter = new PointF(0.5f, 0f);

            var result = segment.GetCenter();

            result.Should().Be(expectedCenter);
        }

        [Test]
        public void SegmentsAreIntersected_ShouldReturnTrue_WhenSegmentsAreIntersected()
        {
            var firstSegment = new Segment(1, 3);
            var secondSegment = new Segment(2, 4);

            Segment.SegmentsAreIntersected(firstSegment, secondSegment)
                .Should()
                .BeTrue();
        }

        [Test]
        public void SegmentsAreIntersected_ShouldReturnFalse_WhenSegmentsAreNotIntersected()
        {
            var firstSegment = new Segment(1, 3);
            var secondSegment = new Segment(4, 5);

            Segment.SegmentsAreIntersected(firstSegment, secondSegment)
                .Should()
                .BeFalse();
        }

        [Test]
        public void SegmentsAreIntersected_ShouldReturnFalse_WhenSegmentsAreTouching()
        {
            var firstSegment = new Segment(1, 3);
            var secondSegment = new Segment(3, 5);

            Segment.SegmentsAreIntersected(firstSegment, secondSegment)
                .Should()
                .BeFalse();
        }
    }
}
