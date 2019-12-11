using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using TagsCloudGenerator.Core.Spirals;
using TagsCloudGenerator.Infrastructure;

namespace TagsCloudGeneratorTests
{
    [TestFixture]
    public class ArchimedeanSpiralTests
    {
        private ArchimedeanSpiral archimedeanSpiral;

        [SetUp]
        public void SetUp()
        {
            archimedeanSpiral = new ArchimedeanSpiral(1, 0.05f);
        }

        [Test]
        public void GetNextPoint_ShouldBeCloseToOriginOnFirstCall()
        {
            Point.Round(archimedeanSpiral.GetNextPoint()).Should().Be(new Point(0, 0));
        }

        [TestCase(5, TestName = "WhenGet5Points")]
        [TestCase(25, TestName = "WhenGet25Points")]
        [TestCase(50, TestName = "WhenGet50Points")]
        public void GetNextPoint_ShouldReturnDifferentPoints(int count)
        {
            var points = new List<PointF>();
            for (var i = 0; i < count; i++)
                points.Add(archimedeanSpiral.GetNextPoint());
            IsDifferentPoints(points).Should().BeTrue();
        }

        private bool IsDifferentPoints(List<PointF> points)
        {
            return points.Distinct(new PointFComparer()).Count() == points.Count;
        }
    }
}