using System.Drawing;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using TagsCloud.Visualization.PointGenerator;

namespace TagsCloud.Tests
{
    public class ArchimedesSpiralPointGeneratorTest
    {
        private readonly Point center = new(10, 10);
        private ArchimedesSpiralPointGenerator sut;

        [SetUp]
        public void InitGenerator()
        {
            sut = new ArchimedesSpiralPointGenerator(center);
        }

        [Test]
        public void GetNext_OnFirstCall_Should_ReturnCenter()
        {
            var point = sut.GenerateNextPoint().First();

            point.Should().BeEquivalentTo(center);
        }

        [Test]
        public void GetNext_Should_ReturnPoints_WithSameRadii()
        {
            var points = sut.GenerateNextPoint()
                .Take(10)
                .ToList();

            var radii = points.Select(x => x.GetDistance(center)).ToList();

            foreach (var (previous, current) in radii.Zip(radii.Skip(1)))
                current.Should().BeInRange(previous, previous + 1);
        }

        [Test]
        public void GetNext_Should_ReturnPoints_WithIncreasingRadius()
        {
            var points = sut.GenerateNextPoint()
                .Take(100)
                .ToList();

            var radii = points
                .Select(x => x.GetDistance(center))
                .ToList();

            foreach (var (previous, current) in radii.Zip(radii.Skip(1)))
                (current - previous).Should().BeGreaterThanOrEqualTo(0);
        }
    }
}