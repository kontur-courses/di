using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;
using TagsCloudVisualization.Infrastructure.Algorithm.Curves;

namespace TagsCloudContainerTests
{
    public class SpiralShould
    {
        [Test]
        public void ThrowException_IncorrectDistanceBetweenLoops()
        {
            Action action = () => new Spiral(0, Point.Empty);
            action.Should().Throw<ArgumentException>();
        }
        [Test]
        public void ThrowException_IncorrectIncrement()
        {
            Action action = () => new Spiral(1, Point.Empty, 0);
            action.Should().Throw<ArgumentException>();
        }


        [TestCase(0, 0)]
        [TestCase(-1000, -1000)]
        [TestCase(1000, 1000)]
        public void ReturnCenter_MustMatchEstablished(int expectedCenterX, int expectedCenterY)
        {
            var expectedCenter = new Point(expectedCenterX, expectedCenterY);
            var spiral = new Spiral(1, expectedCenter);

            spiral.Center.Should().Be(expectedCenter);
        }


        [Test]
        public void ReturnDistanceBetweenLoops_EqualDistanceBetweenLoops()
        {
            var center = new Point(0, 0);
            var spiral = new Spiral(
                1,
                center,
                (float)Math.PI * 2);


            var previsionPoint = spiral.First();
            foreach (var point in spiral.Skip(1).Take(10))
            {
                var distanceBetweenLoops =
                    Math.Sqrt(Math.Pow(previsionPoint.X - point.X, 2)
                              + Math.Pow(previsionPoint.Y - point.Y, 2));
                previsionPoint = point;
                distanceBetweenLoops.Should().BeInRange(1 - 1e-4, 1 + 1e-4);
            }
        }
    }
}
