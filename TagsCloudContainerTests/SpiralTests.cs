using FluentAssertions;
using NUnit.Framework;
using System;
using System.Drawing;
using TagsCloudContainer;

namespace TagsCloudContainerTests
{
    internal class SpiralTests
    {
        [TestCase(0)]
        [TestCase(-10)]
        public void Should_Throw_WhenCreatedWithIncorrectCoefficient(int spiralCoef)
        {
            FluentActions.Invoking(
                () => Spiral.Create(Point.Empty, spiralCoef))
                .Should().Throw<ArgumentException>();
        }

        [Test]
        public void Should_StartAtCenter()
        {
            var center = new Point(500, 500);
            var spiral = Spiral.Create(center, 1, Math.PI / 90);

            center.Should().BeEquivalentTo(spiral.GetNext());
        }

        [Test]
        public void Should_IncreaseDistanceFromCenter()
        {
            var center = new Point(500, 500);
            var spiral = Spiral.Create(center, 1, Math.PI / 90);

            var spiralPoint = spiral.GetNext();
            var distance = center.GetDistance(spiralPoint);

            for (var i = 1; i <= 100; i++)
            {
                spiralPoint = spiral.GetNext();
                if (i % 25 == 0)
                {
                    var newDistance = center.GetDistance(spiralPoint);
                    newDistance.Should().BeGreaterThan(distance);
                    distance = newDistance;
                }
            }
        }
    }
}
