using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;

namespace TagsCloudVisualization
{
    [TestFixture]
    public class RoundSpiralGenerator_Should
    {
        [SetUp]
        public void SetUp()
        {
            generator = new RoundSpiralGenerator(new Point(), 2);
            generatorEnumerator = generator.GetEnumerator();
            generatorEnumerator.MoveNext();
        }

        private const int K = 2;
        private const int FullCircleAmount = 64 * 2;
        private IEnumerator<Point> generatorEnumerator;
        private RoundSpiralGenerator generator;

        [Test]
        public void FitInRectangle_ForFullCircle()
        {
            var fitRectangle = new Rectangle(new Point(-6,-9),new Size(19,13) );
           
            generator.Take(FullCircleAmount)
                     .Should()
                     .OnlyContain(point => fitRectangle.Contains(point.X, point.Y));
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
            generatorEnumerator.Current.Should()
                               .BeEquivalentTo(new Point());
        }
    }
}