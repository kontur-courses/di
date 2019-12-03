using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using TagCloud.CloudLayouter;

namespace TagCloud_Should
{
    public class ArchimedeanSpiral_Should
    {
        private ArchimedeanSpiral spiral;

        [SetUp]
        public void SetNewSpiral()
        {
            spiral = new ArchimedeanSpiral(new SpiralSettings());
        }

        [Test]
        public void ArchimedeanSpiral_GetCurrentX_ShouldReturnZeroAtStart()
        {
            spiral.GetNewPointLazy().GetEnumerator().Current.X.Should().Be(0);
        }

        [Test]
        public void ArchimedeanSpiral_GetCurrentY_ShouldReturnZeroAtStart()
        {
            spiral.GetNewPointLazy().GetEnumerator().Current.Y.Should().Be(0);
        }

        [Test]
        public void ArchimedeanSpiral_GetCurrentX_ShouldReturnCorrectX()
        {
            spiral.GetNewPointLazy().First().X.Should().Be(7);
        }

        [Test]
        public void ArchimedeanSpiral_GetCurrentY_ShouldReturnCorrectY()
        {
            spiral.GetNewPointLazy().First().Y.Should().Be(3);
        }
    }
}