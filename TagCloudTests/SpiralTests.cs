using System;
using FakeItEasy;
using FluentAssertions;
using NUnit.Framework;
using TagCloud.Algorithm.SpiralBasedLayouter;
using TagCloud.Infrastructure;

namespace TagCloudTests
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
            var config = A.Fake<PictureConfig>();
            config.Parameters = A.Fake<LayouterParameters>();
            config.Parameters.Parameter = parameter;
            config.Parameters.StepInDegrees = 30;
            var spiral = new Spiral(config);

            for (var i = 0; i < expectedPointShift; i++)
                spiral.GetNextPoint();
            var pointFromSpiral = spiral.GetNextPoint();

            pointFromSpiral.Should().Be(expectedPoint);
        }
    }
}