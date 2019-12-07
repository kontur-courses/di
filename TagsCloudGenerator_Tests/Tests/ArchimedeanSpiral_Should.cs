using System;
using System.Collections.Generic;
using System.Drawing;
using FluentAssertions;
using NUnit.Framework;
using TagsCloudGenerator.ShapeGenerator;

namespace TagsCloudGenerator_Tests
{
    [TestFixture]
    internal class ArchimedeanSpiral_Should
    {
        private static Point center = new Point(0, 0);
        private ArchimedeanShape shape;

        [SetUp]
        public void SetupSpiral() => 
            shape = new ArchimedeanShape(center, 7 / (2 * Math.PI));
        
        [Test]
        public void GetSpiralPoints_FirstPointOnSpiral_ReturnCenterPoint()
        {
            shape.GetNextSpiralPoint().Should().Be(shape.Center);
        }

        [TestCase(1)]
        [TestCase(10)]
        [TestCase(50)]
        [TestCase(100000)]
        public void GetSpiralPoints_MultiplePointsCreate_NotBeRepeated(int countPoints)
        {
            var points = new HashSet<Point>(shape.GetNextSpiralPoints(countPoints));

            points.Count.Should().Be(countPoints);
        }

        [TestCase(100, true, TestName = "On Clockwise direction")]
        [TestCase(100, false, TestName = "On Counterclockwise direction")]
        public void GetSpiralPoints_MultiplePointsCreate_SpiralChangeOnCertainDirection(int countPoints, bool isClockwise)
        {
            var testSpiral = new ArchimedeanShape(center, (isClockwise ? 1 : -1) / (2 * Math.PI));
            var spiralPoints = testSpiral.GetNextSpiralPoints(countPoints);


            for (var i = 1; i < spiralPoints.Count; i++)
            {
                var previousPoint = spiralPoints[i - 1];
                var currentPoint = spiralPoints[i];

                var pseudoscalarMultiply = (shape.Center.X - currentPoint.X) * (previousPoint.Y - currentPoint.Y) -
                                           (shape.Center.Y - currentPoint.Y) * (previousPoint.X - currentPoint.X);

                if (isClockwise)
                    pseudoscalarMultiply.Should().BeLessOrEqualTo(0);
                else
                    pseudoscalarMultiply.Should().BeGreaterOrEqualTo(0);
            }
        }
    }
}