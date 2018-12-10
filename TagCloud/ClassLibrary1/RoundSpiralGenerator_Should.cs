using System.Drawing;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using TagCloudVisualization;
using Point = TagCloudVisualization.Point;

namespace TagCloudTests
{
    [TestFixture]
    public class RoundSpiralGenerator_Should
    {
        [SetUp]
        public void SetUp()
        {
            generator = new RoundSpiralGenerator().Begin(Point.Empty);
        }

        private const int FullCircleAmount = 64 * 2;
        private AbstractSpiralGenerator generator;

        [Test]
        public void FitInRectangle_ForFullCircle()
        {
            var fitRectangle = new Rectangle(new Point(-6, -9), new Size(19, 13));

            generator.Take(FullCircleAmount)
                     .ToList()
                     .All(p => fitRectangle.Contains(p))
                     .Should()
                     .BeTrue();
        }

        [Test]
        public void ShouldHaveFirst64PointsWithPositiveY()
        {
            generator.Take(64)
                     .All(point => point.Y >= 0)
                     .Should()
                     .BeTrue("upper half of spiral is above y = 0");
        }

        [Test]
        public void YieldZero_AsFirstPoint()
        {
            generator.Next()
                     .Should()
                     .BeEquivalentTo(new Point());
        }
    }
}
