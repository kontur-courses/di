using System;
using System.Drawing;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using TagCloud;
using TagCloud.RectanglesLayouter.PointsGenerator;

namespace TagCloudTests
{
    [TestFixture]
    public class SpiralPointsGeneratorTests
    {
        private const int DistanceBetweenPoints = 1;
        private const double AngleIncrement = 1;
        private SpiralPointsGenerator pointsGenerator;

        [TestCase(0, TestName = "DistanceIsZero")]
        [TestCase(-1, TestName = "DistanceIsNegative")]
        [TestCase(DistanceBetweenPoints, 0, TestName = "AngleIncrementIsZero")]
        [TestCase(DistanceBetweenPoints, -1, TestName = "AngleIncrementIsNegative")]
        [TestCase(0, 0, TestName = "BothAreZero")]
        [TestCase(-1, -1, TestName = "BothAreNegative")]
        public void Constructor_OnInvalidArguments_ThrowsArgumentException(int distanceBetweenPoints, double angleIncrement = AngleIncrement)
        {
            Action action = () => new SpiralPointsGenerator(distanceBetweenPoints, angleIncrement);

            action.Should().Throw<ArgumentException>();
        }

        [Test]
        public void GetPoints_OnFirstGenerating_ReturnsZeroPoint()
        {
            pointsGenerator = new SpiralPointsGenerator(DistanceBetweenPoints, AngleIncrement);

            var firstPoint = pointsGenerator.GetPoints().First();

            firstPoint.Should().Be(new Point());
        }

        [Test]
        public void GetPoints_AfterFirstGenerating_ReturnsNotZeroPoint()
        {
            pointsGenerator = new SpiralPointsGenerator(DistanceBetweenPoints, AngleIncrement);

            var secondPoint = pointsGenerator.GetPoints().Take(2).Last();

            secondPoint.Should().NotBe(new Point());
        }

        [Test]
        public void GetPoints_Always_ReturnsNonRepeatingPoints()
        {
            pointsGenerator = new SpiralPointsGenerator(DistanceBetweenPoints, AngleIncrement);

            var pointsToCheck = pointsGenerator.GetPoints().Take(1000).ToArray();

            for (var i = 0; i < pointsToCheck.Length; i++)
            {
                for (var j = i + 1; j < pointsToCheck.Length; j++)
                    pointsToCheck[i].Should().NotBe(pointsToCheck[j]);
            }
        }

        [Test]
        public void GetPoints_Always_ReturnsPointsWithEquallyIncreasingRadius()
        {
            const int maximumRadiusDifference = 2;
            pointsGenerator = new SpiralPointsGenerator(DistanceBetweenPoints, AngleIncrement);

            var pointsToCheck = pointsGenerator.GetPoints().Take(100).ToArray();
            var previousPoint = pointsToCheck.First();

            foreach (var point in pointsToCheck.Skip(1))
            {
                var currentRadius = GetRadius(point);
                var previousRadius = GetRadius(previousPoint);
                var radiusDifference = currentRadius - previousRadius;
                radiusDifference.Should().BeLessThan(maximumRadiusDifference);
                previousPoint = point;
            }
        }

        [Test]
        public void GetPoints_Always_ReturnsPointsWithEquallyIncreasingAngle()
        {
            const double maximumAngleDifference = 1.5;
            pointsGenerator = new SpiralPointsGenerator(DistanceBetweenPoints, AngleIncrement);

            var pointsToCheck = pointsGenerator.GetPoints().Take(4).ToArray();
            var previousPoint = pointsToCheck.First();

            foreach (var point in pointsToCheck.Skip(1))
            {
                var currentAngle = GetAngle(point);
                var previousAngle = GetAngle(previousPoint);
                var angleDifference = currentAngle - previousAngle;
                angleDifference.Should().BeLessThan(maximumAngleDifference);
                previousPoint = point;
            }
        }

        [Test]
        public void GetPoints_OnFirstFivePoints_ReturnsConstantPoints()
        {
            var expectedPoints = new[]
                {new Point(0, 0), new Point(1, 1), new Point(-1, 2), new Point(-3, 0), new Point(-3, -3)};
            pointsGenerator = new SpiralPointsGenerator(DistanceBetweenPoints, AngleIncrement);

            var actualPoints = pointsGenerator.GetPoints().Take(expectedPoints.Length).ToArray();

            for (var i = 0; i < actualPoints.Length; i++)
                actualPoints[i].Should().Be(expectedPoints[i]);
        }

        private double GetRadius(Point point) => Math.Sqrt(point.X * point.X + point.Y * point.Y);

        private double GetAngle(Point point) => Math.Atan2(point.Y, point.X);
    }
}