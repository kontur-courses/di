using System;
using System.Drawing;
using System.IO;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using TagsCloudVisualization;
using TagsCloudVisualizationTests.TestingLibrary;

namespace TagsCloudVisualizationTests.Tests
{
    public class ArchimedeanSpiralTests
    {
        [TestCase(0, 0)]
        [TestCase(1, 2)]
        [TestCase(-1, -2)]
        public void GetPoint_WithoutAngle_InCenter(int x, int y)
        {
            var center = new Point(x, y);
            var spiral = new ArchimedeanSpiral(center);

            var actualPoint = spiral.GetPoint(0);

            actualPoint.Should().Be(center);
        }

        [TestCase(0)]
        [TestCase(-1)]
        public void Constructor_Radius_ShouldBePositive(int radius)
        {
            Assert.Throws<ArgumentException>(
                () =>
                    new ArchimedeanSpiral(new Point(0, 0), radius));
        }

        [Test]
        public void GetPoint_Degree_CantBeNegative()
        {
            var spiral = new ArchimedeanSpiral();
            Assert.Throws<ArgumentException>(
                () =>
                    spiral.GetPoint(-1));
        }

        [Test]
        public void GetPoint_Every90DegreePeriod_YProportionalToRadius() =>
            AssertProportionAlongAngle(90, period => -(0.5 + period * 2), point => point.Y);

        [Test]
        public void GetPoint_Every270DegreePeriod_YProportionalToRadius() =>
            AssertProportionAlongAngle(270, period => 1.5 + period * 2, point => point.Y);

        [Test]
        public void GetPoint_Every0DegreePeriod_XProportionalToRadius() =>
            AssertProportionAlongAngle(0, period => period * 2, point => point.X);

        [Test]
        public void GetPoint_Every180DegreePeriod_XProportionalToRadius() =>
            AssertProportionAlongAngle(180, period => -(1 + period * 2), point => point.X);

        [Test]
        [Explicit]
        public void GetPoint_SaveToBitmap()
        {
            const int periods = 100;
            const double radius = 0.1f;

            var spiral = new ArchimedeanSpiral(radius: radius);
            var points = Enumerable.Range(0, 360 * periods).Select(spiral.GetPoint);
            var visualizer = new PointsVisualizer(points);

            var savePath = Path.Combine(Directory.GetCurrentDirectory(), "ArchimedeanSpiral.GetPoint.bmp");
            new VisualOutput(visualizer).SaveToBitmap(savePath);
            TestContext.WriteLine($"Saved to '{savePath}'");
        }

        private static void AssertProportionAlongAngle(
            int angleInDegrees,
            Func<int, double> getPeriodProportion,
            Func<Point, int> selectCoordinateToAssert)
        {
            const int radius = 10;
            var spiral = new ArchimedeanSpiral(new Point(0, 0), radius);

            for (var period = 0; period < 100; period++)
            {
                var expectedProportion = getPeriodProportion(period);
                var expected = expectedProportion * Math.PI * radius;
                var degree = angleInDegrees + 360 * period;

                var actual = spiral.GetPoint(degree);

                ((double)selectCoordinateToAssert(actual)).Should().BeApproximately(
                    expected,
                    1,
                    $"period is '{period}', and proportion should be '{expectedProportion}'");
            }
        }
    }
}