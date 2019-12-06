using System;
using System.Drawing;
using FluentAssertions;
using NUnit.Framework;

namespace TagsCloudContainer.Visualization.Tests
{
    [TestFixture]
    public class ArchimedeanSpiral_Should
    {
        [TestCase(0, 0)]
        [TestCase(500, 400)]
        public void ReturnCenterPointFirst_WithZeroA(int centerX, int centerY)
        {
            var center = new Point(centerX, centerY);
            var archimedeanSpiral = new ArchimedeanSpiral(center, a: 0);
            archimedeanSpiral.GetNextPoint().Should().BeEquivalentTo(center);
        }

        [TestCase(5000, 5000, 2, 4, Math.PI / 6, 5, TestName = "On 5 steps")]
        [TestCase(5000, 5000, 2, 4, Math.PI / 6, 100, TestName = "On 100 steps")]
        [TestCase(5000, 5000, 2, 4, Math.PI / 180, 720, TestName = "On 720 steps")]
        public void ReturnCorrectPoints_OnNextSteps(int centerX, int centerY, double a, double b, double step,
            int stepsNumber)
        {
            var center = new Point(centerX, centerY);
            var archimedeanSpiral = new ArchimedeanSpiral(center, step, a, b);
            for (var i = 0; i < stepsNumber; i++)
            {
                var nextPoint = archimedeanSpiral.GetNextPoint();
                var pointRadius = GetDistance(center, nextPoint);
                var pointPhi = step * i;
                pointRadius.Should().BeApproximately(GetArchimedeanSpiralRadius(a, b, pointPhi), 1);
            }
        }

        [TestCase(5000, 5000, 2, 2, Math.PI * 2, TestName = "On full circle")]
        [TestCase(5000, 5000, 2, 2, Math.PI, TestName = "On half circle")]
        [TestCase(5000, 5000, 2, 2, -Math.PI, TestName = "On opposite direction")]
        public void ReturnPointWithGreaterRadius_OnNextStep(int centerX, int centerY, double a, double b, double step)
        {
            var center = new Point(centerX, centerY);
            var archimedeanSpiral = new ArchimedeanSpiral(center, step, a, b);
            var firstPoint = archimedeanSpiral.GetNextPoint();
            var secondPoint = archimedeanSpiral.GetNextPoint();
            var firstRadius = GetDistance(center, firstPoint);
            var secondRadius = GetDistance(center, secondPoint);
            secondRadius.Should().BeGreaterThan(firstRadius);
        }

        private static double GetDistance(Point firstPoint, Point secondPoint)
        {
            return Math.Sqrt((firstPoint.X - secondPoint.X) * (firstPoint.X - secondPoint.X) +
                             (firstPoint.Y - secondPoint.Y) * (firstPoint.Y - secondPoint.Y));
        }

        private static double GetArchimedeanSpiralRadius(double a, double b, double phi)
        {
            return a + b * phi;
        }
    }
}