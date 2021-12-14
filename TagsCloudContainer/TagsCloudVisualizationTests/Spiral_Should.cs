using System;
using System.Drawing;
using NUnit.Framework;
using FluentAssertions;
using TagsCloudVisualization.PointPlacers;

namespace TagsCloudVisualizationTests
{
    [TestFixture]
    public class Spiral_Should
    {
        private const float Epsilon = 1e-5f;
        private const float DefaultRadiusIncreaseValue = 0.01f;
        private const float DefaultAngleIncreaseValue = 0.01f;

        [Test]
        [TestCaseSource(typeof(Generators), nameof(Generators.CenterGenerator))]
        public void StartPoint_EqualsCenter_WithCustomCenter(Point center)
        {
            var spiral = new Spiral(center);

            spiral.CurrentPoint.X.Should().BeApproximately(center.X, Epsilon);
            spiral.CurrentPoint.Y.Should().BeApproximately(center.Y, Epsilon);
        }

        [Test]
        [TestCaseSource(typeof(Generators), nameof(Generators.CenterGenerator))]
        public void IncreaseSize_ChangingPoint_WithDefaultValues(Point center)
        {
            var spiral = new Spiral(center);

            spiral.GetNextPoint();

            var currentRadius = DefaultRadiusIncreaseValue;
            var currentAngle = DefaultAngleIncreaseValue;

            var expectedX = CountX(currentRadius, currentAngle, center.X);
            var expectedY = CountY(currentRadius, currentAngle, center.Y);

            spiral.CurrentPoint.X.Should().BeApproximately(expectedX, Epsilon);
            spiral.CurrentPoint.Y.Should().BeApproximately(expectedY, Epsilon);
        }

        [Test]
        [TestCaseSource(typeof(Generators), nameof(Generators.CenterGenerator))]
        public void IncreaseSize_ChangingPoint_WithCustomValues(Point center)
        {
            var spiral = new Spiral(center);

            spiral.IncreaseSize(1, 1);

            var currentRadius = 1;
            var currentAngle = 1;

            var expectedX = CountX(currentRadius, currentAngle, center.X);
            var expectedY = CountY(currentRadius, currentAngle, center.Y);

            spiral.CurrentPoint.X.Should().BeApproximately(expectedX, Epsilon);
            spiral.CurrentPoint.Y.Should().BeApproximately(expectedY, Epsilon);
        }

        [Test]
        [TestCaseSource(typeof(Generators), nameof(Generators.CenterGenerator))]
        public void IncreaseSize_ChangingPointRepeatedly_WithDefaultValues(Point center)
        {
            var spiral = new Spiral(center);

            spiral.GetNextPoint();
            spiral.GetNextPoint();
            spiral.GetNextPoint();

            var currentRadius = DefaultRadiusIncreaseValue * 3;
            var currentAngle = DefaultAngleIncreaseValue * 3;

            var expectedX = CountX(currentRadius, currentAngle, center.X);
            var expectedY = CountY(currentRadius, currentAngle, center.Y);

            spiral.CurrentPoint.X.Should().BeApproximately(expectedX, Epsilon);
            spiral.CurrentPoint.Y.Should().BeApproximately(expectedY, Epsilon);
        }

        [Test]
        [TestCaseSource(typeof(Generators), nameof(Generators.CenterGenerator))]
        public void IncreaseSize_ChangingPointRepeatedly_WithCustomValues(Point center)
        {
            var spiral = new Spiral(center);

            spiral.IncreaseSize(1, 2);
            spiral.IncreaseSize(3, 4);
            spiral.IncreaseSize(5, 6);

            var currentRadius = 1 + 3 + 5;
            var currentAngle = 2 + 4 + 6;

            var expectedX = CountX(currentRadius, currentAngle, center.X);
            var expectedY = CountY(currentRadius, currentAngle, center.Y);

            spiral.CurrentPoint.X.Should().BeApproximately(expectedX, Epsilon);
            spiral.CurrentPoint.Y.Should().BeApproximately(expectedY, Epsilon);
        }

        private static float CountX(float radius, float angle, float currentX)
        {
            return (float) Math.Cos(angle) * radius + currentX;
        }

        private static float CountY(float radius, float angle, float currentY)
        {
            return (float) Math.Sin(angle) * radius + currentY;
        }
    }
}