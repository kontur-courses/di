using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using TagsCloudContainer.Layouter.PointsCreators;

namespace TagsCloudContainer_Tests
{
    [TestFixture]
    public class SpiralPointsProvider_Should
    {
        [TestCase(0, 0, TestName = "zero center is allowed")]
        [TestCase(-1, 0, TestName = "negative x centre coordinate is allowed")]
        [TestCase(0, -1, TestName = "negative y centre coordinate is allowed")]
        [TestCase(-1, -1, TestName = "negative both coordinates is allowed")]
        [TestCase(1, 1, TestName = "positive both coordinates is allowed")]
        public void CreateFirstPointInCenter(int xCenter, int yCenter)
        {
            var center = new Point(xCenter, yCenter);
            var creator = new SpiralPointsProvider(center);
            creator.GetNextPoint().Should().Be(center);
        }

        [Test]
        public void CreateDifferentPoints()
        {
            var pointsCount = 100;
            var creator = new SpiralPointsProvider(new Point(0, 0));
            var points = new List<Point>();
            for (var i = 1; i <= pointsCount; i++) points.Add(creator.GetNextPoint());
            points.Should().HaveSameCount(points.Distinct());
        }

        [Test]
        public void CreateExpectedNumberOfPoints()
        {
            var pointsCount = 100;
            var creator = new SpiralPointsProvider(new Point(0, 0));
            var points = new List<Point>();
            for (var i = 1; i <= pointsCount; i++) points.Add(creator.GetNextPoint());

            points.Should().HaveCount(pointsCount);
        }
    }
}