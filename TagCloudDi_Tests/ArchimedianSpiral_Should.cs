using System.Drawing;
using FluentAssertions;
using NUnit.Framework;
using TagCloudDi;
using TagCloudDi.Layouter;

namespace TagCloudDi_Tests
{
    [TestFixture]
    public class ArchimedeanSpiral_Should
    {
        private readonly Settings settings = new() { SpiralScale = 1 };
        
        [TestCaseSource(typeof(TestDataArchimedeanSpiral), nameof(TestDataArchimedeanSpiral.Different_CenterPoints))]
        public void ReturnCenterPoint_WhenFirstTime_GetNextPoint(Point point)
        {
            var spiral = new ArchimedeanSpiral(point, settings);
            spiral.GetNextPoint().Should().BeEquivalentTo(point);
        }

        [TestCaseSource(typeof(TestDataArchimedeanSpiral),
            nameof(TestDataArchimedeanSpiral.DifferentIterationsAdded_ExpectedPoints))]
        public void ReturnsCorrectPoint_When(int iterations, Point expectedPoint)
        {
            var spiral = new ArchimedeanSpiral(new Point(), settings);
            for (var i = 0; i < iterations; i++)
                spiral.GetNextPoint();

            spiral.GetNextPoint().Should().BeEquivalentTo(expectedPoint);
        }
    }
}
