using System;
using System.Drawing;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using TagsCloudVisualization;
using TagsCloudVisualizationTest.Builders;
using static FluentAssertions.FluentActions;


namespace TagsCloudVisualizationTest
{
    [TestFixture]
    public class PointSpiral_Should
    {
        [TestCase(-1, -1)]
        [TestCase(-1, 0)]
        [TestCase(-1, 1)]
        [TestCase(0, -1)]
        [TestCase(0, 0)]
        [TestCase(0, 1)]
        [TestCase(1, -1)]
        [TestCase(1, 0)]
        [TestCase(1, 1)]
        [TestCase(0, 0, 2, 1)]
        [TestCase(0, 0, 1, 2)]
        public void NotThrowAnyException_OnAnyCenterPointAndPositiveDensityParameterAndDegreesParameter(int x, int y, int degreesParameter=1, float densityParameter=1f)
        {
            var builder = PointSpiralBuilder.APointSpiral()
                .WithCenter(new Point(x, y))
                .WithDensityParameter(densityParameter)
                .WithDegreesDelta(degreesParameter);

            Invoking(() => builder.Build()).Should().NotThrow($"X = {x}; " +
                                                              $"Y = {y}, " +
                                                              $"degreesParameter = {degreesParameter}; " +
                                                              $"densityParameter = {densityParameter}");
        }
        
        [TestCase(0, 0, 0, 1)]
        [TestCase(0, 0, 1, 0)]
        [TestCase(0, 0, -1, 1)]
        [TestCase(0, 0, 1, -1)]
        [TestCase(0, 0, -1, -1)]
        public void NotThrowAnyException_OnNonPositiveDensityParameterOrDegreesParameter(int x, int y, int degreesParameter=1, float densityParameter=1f)
        {
            var builder = PointSpiralBuilder.APointSpiral()
                .WithCenter(new Point(x, y))
                .WithDensityParameter(densityParameter)
                .WithDegreesDelta(degreesParameter);

            Invoking(() => builder.Build()).Should().Throw<ArgumentException>($"X = {x}; " +
                                                                              $"Y = {y}, " +
                                                                              $"degreesParameter = {degreesParameter}; " +
                                                                              $"densityParameter = {densityParameter}");
        }

        [TestCase(-1, -1)]
        [TestCase(-1, 0)]
        [TestCase(-1, 1)]
        [TestCase(0, -1)]
        [TestCase(0, 0)]
        [TestCase(0, 1)]
        [TestCase(1, -1)]
        [TestCase(1, 0)]
        [TestCase(1, 1)]
        public void PlaceFirstPointOnMiddle(int x, int y)
        {
            var expected = new Point(x, y);
            var actual = PointSpiralBuilder
                .APointSpiral()
                .WithCenter(new Point(x, y))
                .Build()
                .GetPoints()
                .First();
            
            actual.Should().Be(expected, $"X = {x}; Y = {y}");
        }
        
        [Test]
        [Repeat(100)]
        public void AutoTest_IInfinityPointsEnumerable()
        {
            var pointSpiral = PointSpiralBuilder
                .APointSpiral()
                .WithCenter(Point.Empty)
                .WithDegreesDelta(25)
                .WithDensityParameter(10)
                .Build() as IInfinityPointsEnumerable;
            
            var lastRadius = -1d;
            var lastPointDifferent = 0d;
            var lastPoint = Point.Empty;

            var pointCount = 10000;
            
            foreach (var point in pointSpiral.GetPoints())
            {
                var radius = point.MetricTo(new Point(0, 0));
                var pointDifferent = point.MetricTo(lastPoint);

                radius.Should().BeGreaterThan(lastRadius, $"on {100000 - pointCount} try");
                pointDifferent.Should().BeGreaterOrEqualTo(lastPointDifferent - 1d, $"on {100000 - pointCount} try");

                lastPoint = point;
                lastRadius = radius;
                lastPointDifferent = pointDifferent;
                if (pointCount-- < 0) break;
            }
        }
    }
}