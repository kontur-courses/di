using System;
using System.Drawing;
using FluentAssertions;
using NUnit.Framework;
using TagCloud.PointGenerators;

namespace TagCloudTests
{
    class SpiralPointGeneratorTests
    {
        [TestCase(0,0, TestName = "center in zero")]
        [TestCase(3, 5, TestName = "non-zero center")]
        public void SpiralPointGenerator_GetNextPoint_FirstPointCreatedInCenter_When(int centerX, int centerY)
        {
            var center = new Point(centerX, centerY);
            var spiralPointGenerator = new SpiralPointGenerator(center);

            var firstPoint = spiralPointGenerator.GetNextPoint();

            firstPoint.Should().BeEquivalentTo(center);
        }

        [TestCase(0, 0, TestName = "center in zero")]
        [TestCase(3, 5, TestName = "non-zero center")]
        public void SpiralPointGenerator_GetNextPoint_NewPointsAreMovingAwayFromCenter_When(int centerX, int centerY)
        {
            var center = new Point(centerX, centerY);
            var spiralPointGenerator = new SpiralPointGenerator(center);
            var firstPoint = spiralPointGenerator.GetNextPoint();
            var previousPoint = spiralPointGenerator.GetNextPoint();
            var sinceSpiralStepIsSmallAndPointsAreRoundedCheckWithStep = 200;
            
            for (int i = 1; i < 1000; i++)
            {
                var currentPoint = spiralPointGenerator.GetNextPoint();
                if (i % sinceSpiralStepIsSmallAndPointsAreRoundedCheckWithStep == 0)
                {
                    var currentDistanceToCenter = GetDistance(firstPoint, currentPoint);
                    var previousDistanceToCenter = GetDistance(firstPoint, previousPoint);
                    currentDistanceToCenter.Should().BeGreaterThan(previousDistanceToCenter);
                    previousPoint = currentPoint;
                }
            }
        }

        private double GetDistance(Point a, Point b)
        {
            var diffAB = Point.Subtract(a, new Size(b));
            return Math.Pow(diffAB.X * diffAB.X + diffAB.Y * diffAB.Y, 0.5);
        }
    }
}
