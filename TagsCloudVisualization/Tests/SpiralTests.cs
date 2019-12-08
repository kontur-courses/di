using FluentAssertions;
using NUnit.Framework;
using System;

namespace TagsCloudVisualization.Tests
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
            var spiral = new Spiral(parameter, 30);

            for (var i = 0; i < expectedPointShift; i++)
                spiral.GetNextPoint();
            var pointFromSpiral = spiral.GetNextPoint();

            pointFromSpiral.Should().Be(expectedPoint);
        }
    }
}