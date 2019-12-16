using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using TagsCloudVisualization.Spirals;

namespace TagsCloudVisualization.Tests
{
    [TestFixture(typeof(ArchimedeanSpiral))]
    [TestFixture(typeof(RectangularSpiral))]
    internal class Spiral_should<Spiral> where Spiral : ISpiral, new()
    {
        ISpiral spiral;

        [SetUp]
        public void ArchimedeanSetUp() => spiral = new Spiral();

        [Test]
        public void GetPoints_ShouldBeReturnFirstPosition_InZeroZero()
        {
            spiral.GetPoints().First().Should().Be(new Point(0, 0));
        }

        [TestCase(10)]
        [TestCase(100)]
        [TestCase(1000)]
        public void GetPoints_ShouldBeReturnPoints_NotRepeated(int countPoints)
        {
            var spiralPoints = spiral.GetPoints().Take(countPoints).ToList();

            var points = new HashSet<Point>(spiralPoints);

            points.Count.Should().Be(countPoints);
        }

        [Test]
        public void GetPoints_ShouldBeReturnPoints_OnСlockwiseDirections()
        {
            var spiralPoints = spiral.GetPoints().Take(1000).ToList();

            var center = new Point(0, 0);
            var previousPoint = spiralPoints[0];
            var curentPoint = spiralPoints[0];

            for (int i = 1; i < spiralPoints.Count; i++)
            {
                previousPoint = curentPoint;
                curentPoint = spiralPoints[i];

                var pseudoScalarProduct = (center.X - curentPoint.X) * (previousPoint.Y - curentPoint.Y) -
                    (center.Y - curentPoint.Y) * (previousPoint.X - curentPoint.X); // Я не знаю как правильно это называется на английском))

                if (pseudoScalarProduct > 0) Assert.Fail($"Previous point: {previousPoint} and curent point: {curentPoint} not clockwise");
            }
        }
    }
}
