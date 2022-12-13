using System;
using System.Drawing;
using FluentAssertions;
using NUnit.Framework;
using TagCloud.PointGenerator;

namespace TagCloudUnitTests
{

    [TestFixture]
    public class ArchimedeanSpiralTests
    {

        [TestCase(-1)]
        [TestCase(0)]
        public void Constructor_ThrowsArgumentException_WhenScaleFactorHasInvalidValue(double compressionRatio)
        {
            Action action = () => new ArchimedeanSpiral(new Point(0, 0), compressionRatio, 1);

            action.Should().Throw<ArgumentException>();
        }

        [TestCase(-1)]
        [TestCase(0)]
        public void Constructor_ThrowsArgumentException_WhenCompressionFactorHasInvalidValue(double scaleFactor)
        {
            Action action = () => new ArchimedeanSpiral(new Point(0, 0), 1, scaleFactor);

            action.Should().Throw<ArgumentException>();
        }

        [TestCase(0, 0, 0, TestName = "Angle is zero")]
        [TestCase(Math.PI / 4, 0, 0, TestName = "Angle is PI/4")]
        [TestCase(Math.PI / 2, 0, 1, TestName = "Angle is PI/2")]
        [TestCase(Math.PI, -3, 0, TestName = "Angle is PI")]
        public void GetNextPoint_ReturnsCorrectPoint_WhenCenterIsZeroAndScaleFactorIsOne(double angleOnRadians, int correctX, int correctY)
        {
            var archimedeanSpiral = new ArchimedeanSpiral(new Point(0, 0));

            TurnSpiralOnAngle(archimedeanSpiral, angleOnRadians);

            archimedeanSpiral.GetNextPoint().Should().BeEquivalentTo(new Point(correctX, correctY));
        }

        [TestCase(0, 0, 0, TestName = "Angle is zero")]
        [TestCase(Math.PI / 4, 1, 1, TestName = "Angle is PI/4")]
        [TestCase(Math.PI / 2, 0, 3, TestName = "Angle is PI/2")]
        [TestCase(Math.PI, -6, 0, TestName = "Angle is PI")]
        public void GetNextPoint_ReturnsCorrectPoint_WhenCenterIsZeroAndScaleFactorIsTwo(double angleOnRadians, int correctX, int correctY)
        {
            var archimedeanSpiral = new ArchimedeanSpiral(new Point(0, 0), 1, 2);

            TurnSpiralOnAngle(archimedeanSpiral, angleOnRadians);

            archimedeanSpiral.GetNextPoint().Should().BeEquivalentTo(new Point(correctX, correctY));
        }


        [TestCase(0, 1, 1, TestName = "Angle is zero")]
        [TestCase(Math.PI / 4, 1, 1, TestName = "Angle is PI/4")]
        [TestCase(Math.PI / 2, 1, 2, TestName = "Angle is PI/2")]
        [TestCase(Math.PI, -2, 1, TestName = "Angle is PI")]
        public void GetNextPoint_ReturnsCorrectPoint_WhenCenterIsNonZeroAndScaleFactorIsOne(double angleOnRadians, int correctX, int correctY)
        {
            var archimedeanSpiral = new ArchimedeanSpiral(new Point(1, 1));

            TurnSpiralOnAngle(archimedeanSpiral, angleOnRadians);

            archimedeanSpiral.GetNextPoint().Should().BeEquivalentTo(new Point(correctX, correctY));
        }

        [TestCase(0, 1, 1, TestName = "Angle is zero")]
        [TestCase(Math.PI / 4, 2, 2, TestName = "Angle is PI/4")]
        [TestCase(Math.PI / 2, 1, 4, TestName = "Angle is PI/2")]
        [TestCase(Math.PI, -5, 1, TestName = "Angle is PI")]
        public void GetNextPoint_ReturnsCorrectPoint_WhenCenterIsNonZeroAndScaleFactorIsTwo(double angleOnRadians, int correctX, int correctY)
        {
            var archimedeanSpiral = new ArchimedeanSpiral(new Point(1, 1), 1, 2);

            TurnSpiralOnAngle(archimedeanSpiral, angleOnRadians);

            archimedeanSpiral.GetNextPoint().Should().BeEquivalentTo(new Point(correctX, correctY));
        }

        private void TurnSpiralOnAngle(ArchimedeanSpiral archimedeanSpiral, double angleOnRadians)
        {
            int stepsCount = (int)(Math.Abs(angleOnRadians) * 180 / Math.PI);

            for (int i = 0; i < stepsCount; i++)
                archimedeanSpiral.GetNextPoint();
        }
    }
}
