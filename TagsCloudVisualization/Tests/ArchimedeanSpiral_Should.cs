using System;
using System.Collections.Generic;
using System.Drawing;
using FluentAssertions;
using NUnit.Framework;

namespace TagsCloudVisualization
{
    [TestFixture]
    public class ArchimedeanSpiral_Should
    {
        private static Point center = new Point(0, 0);
        private ArchimedeanSpiral spiral;

        [SetUp]
        public void SetupSpiral() => 
            spiral = new ArchimedeanSpiral(center, 7 / (2 * Math.PI));
        
        [Test]
        public void GetSpiralPoints_FirstPointOnSpiral_ReturnCenterPoint()
        {
            spiral.GetNextSpiralPoint().Should().Be(spiral.Center);
        }

        [TestCase(1)]
        [TestCase(10)]
        [TestCase(50)]
        [TestCase(100000)]
        public void GetSpiralPoints_MultiplePointsCreate_NotBeRepeated(int countPoints)
        {
            var points = new HashSet<Point>(spiral.GetNextSpiralPoints(countPoints));

            points.Count.Should().Be(countPoints);
        }

        [TestCase(100, true, TestName = "On Clockwise direction")]
        [TestCase(100, false, TestName = "On Counterclockwise direction")]
        public void GetSpiralPoints_MultiplePointsCreate_SpiralChangeOnCertainDirection(int countPoints, bool isClockwise)
        {
            var testSpiral = new ArchimedeanSpiral(center, (isClockwise ? 1 : -1) / (2 * Math.PI));
            var spiralPoints = testSpiral.GetNextSpiralPoints(countPoints);


            for (var i = 1; i < spiralPoints.Count; i++)
            {
                var previousPoint = spiralPoints[i - 1];
                var currentPoint = spiralPoints[i];

                var pseudoscalarMultiply = (spiral.Center.X - currentPoint.X) * (previousPoint.Y - currentPoint.Y) -
                                           (spiral.Center.Y - currentPoint.Y) * (previousPoint.X - currentPoint.X);

                if (isClockwise)
                    pseudoscalarMultiply.Should().BeLessOrEqualTo(0);
                else
                    pseudoscalarMultiply.Should().BeGreaterOrEqualTo(0);
            }
        }
    }
}