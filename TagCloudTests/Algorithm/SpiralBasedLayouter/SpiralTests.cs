using System;
using FluentAssertions;
using NUnit.Framework;
using TagCloud.Algorithm.SpiralBasedLayouter;
using TagCloud.Infrastructure;

namespace TagCloudTests.Algorithm.SpiralBasedLayouter
{
    [TestFixture]
    public class SpiralTests
    {
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        public void GetNextPoint_ShouldContainPointsFromSpiral_OnCorrespondingInput(int expectedPointShift)
        {
            const int parameter = 2;
            const float stepInRadians = (float)(Math.PI / 6);
            var expectedPoint = GeometryUtils.ConvertPolarToIntegerCartesian(
                parameter * stepInRadians * expectedPointShift, 
                stepInRadians * expectedPointShift);
            var config = new PictureConfig
            {
                Parameters = new LayouterParameters { Parameter = parameter, StepInDegrees = 30 }
            };

            var spiral = new ArchimedeanSpiral(config);

            for (var i = 0; i < expectedPointShift; i++)
                spiral.GetNextPoint();
            var pointFromSpiral = spiral.GetNextPoint();

            pointFromSpiral.Should().Be(expectedPoint);
        }
    }
}