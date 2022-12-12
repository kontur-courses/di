using System;
using System.Drawing;
using FluentAssertions;
using NUnit.Framework;
using TagsCloudContainer.Layouter;

namespace TagsCloudContainerTests
{
    [TestFixture]
    public class SpiralTest
    {
        [TestCase(0, 0, 0)]
        [TestCase(0, 0, -5)]
        public void Creation_ZeroStep_ThrowArgumentException(int x, int y, double step)
        {
            Action action = () => new Spiral(new Point(x, y), step);
            action.Should().Throw<ArgumentException>().WithMessage("Step must not be less than or equal to 0");
        }
        
        [TestCase(0, 0, 1)]
        [TestCase(5, 5, 10)]
        [TestCase(100, 100, 50)]
        public void Creation_CorrectParameters_ShouldNotFail(int x, int y, double step)
        {
            Action action = () => new Spiral(new Point(x, y), step);
            action.Should().NotThrow<ArgumentException>();
        }

        [Test]
        public void NextPoint_ShouldGetDifferentPoint()
        {
            var spiral = new Spiral(new Point(0, 0), 10);
            var point = spiral.NextPoint();
            spiral.NextPoint().Should().NotBe(point);
        }
    }
}