using System.Drawing;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using TagCloud.CloudLayouter.CircularLayouter;

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
        public void GetNewPointLazy_ShouldReturnZeroZeroAtStart()
        {
            spiral.GetNewPointLazy().GetEnumerator().Current.Should().BeEquivalentTo(new Point(0, 0));
        }

        [Test]
        public void GetNewPointLazy_ShouldReturnCorrectPoint()
        {
            spiral.GetNewPointLazy().First().Should().BeEquivalentTo(new Point(7, 3));
        }
    }
}