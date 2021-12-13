using System.Drawing;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using TagsCloudVisualization.PointGenerators;
using TagsCloudVisualizationTests.Extensions;

namespace TagsCloudVisualizationTests.PointGeneratorsTest
{
    [TestFixture]
    public class PointGeneratorTest
    {
        private Point start;
        private ArchimedeanSpiralPointGenerator generator;
        
        [SetUp]
        public void SetUp()
        {
            start = new Point(0, 0);
            generator = new ArchimedeanSpiralPointGenerator(start);
        }

        [Test]
        public void GetNextPoint_ShouldReturnStartPoint_WhenCalledFirstTime()
        {
            var actualPoint = generator.GetNextPoint();

            actualPoint.Should().BeEquivalentTo(start);
        }
        
        [Test]
        public void GetNextPoint_ShouldReturnPointsWithIncreasingDistanceToStart()
        {
            var points = Enumerable.Range(0, 100).Select(_ => generator.GetNextPoint()).ToList();
            
            var actualRadius = points.Select(point => point.DistanceTo(start)).ToList();

            var expectedRadius = actualRadius.OrderBy(x => x);
            actualRadius.Should().BeEquivalentTo(expectedRadius);
        }
    }
}