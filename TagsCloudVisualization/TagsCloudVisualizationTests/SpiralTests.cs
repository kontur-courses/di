using System;
using System.Drawing;
using FluentAssertions;
using NUnit.Framework;
using TagsCloudVisualization.Spirals;

namespace TagsCloudVisualization.TagsCloudVisualizationTests
{
    [TestFixture]
    internal class SpiralTests
    {
        [Test]
        public void SpiralInitialization_ThrowsArgumentException_OnZeroAngleStep()
        {
            var action = () => new Spiral(new Point(4, 4), 0);
            action.Should().Throw<ArgumentException>()
                .WithMessage("Angle step should not be zero");
        }

        [TestCase(0.1,
            new[] { 400, 401, 401, 400, 400 },
            new[] { 400, 400, 401, 401, 402 },
            TestName = "AngleStep is positive")]
        [TestCase(-0.1,
            new[] { 400, 399, 399, 400, 400 },
            new[] { 400, 400, 401, 401, 402 },
            TestName = "AngleStep is negative")]
        public void GetPoints_ShouldReturnSpiralPoints_OnCorrectAngleStep(
            double angleStep, int[] x, int[] y)
        {
            var expectedPoints = new Point[x.Length];

            for (var i = 0; i < x.Length; i++)
            {
                expectedPoints[i] = new Point(x[i], y[i]);
            }

            var spiral = new Spiral(new Point(400, 400), angleStep);
            spiral.GetPoints(x.Length).Should().BeEquivalentTo(expectedPoints);
        }
    }
}
