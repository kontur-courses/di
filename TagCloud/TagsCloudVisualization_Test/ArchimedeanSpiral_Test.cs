using NUnit.Framework;
using System.Drawing;
using System.Linq;
using FluentAssertions;
using TagCloud;


namespace TagsCloudVisualization_Test
{
    class ArchimedeanSpiralTest
    {

        [Test]
        public void Should_SetCenter_InConstructor_ByConstructorParameter()
        {
            var center = new Point(-10, 10);
            var spiral = new ArchimedeanSpiral(center);
            var point = spiral.GetDiscretePoints().First();
            point.Should().Be(center);
        }

        [Test]
        public void Should_SetCenter_InEmptyConstructor_AsEmptyPoint()
        {
            var spiral = new ArchimedeanSpiral();
            var point = spiral.GetDiscretePoints().First();
            point.Should().Be(Point.Empty);
        }
    }
}
