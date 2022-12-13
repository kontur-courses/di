using NUnit.Framework;
using FluentAssertions;
using TagCloud.WordColoring;
using System;

namespace TagCloudUnitTests
{
    [TestFixture]
    internal class WordColoringProviderTests
    {
        [Test]
        public void GetWordColoringByName_ReturnsCorrectWordColoring()
        {
            var actualColoring = new WordColoringProvider().GetWordColoringByName("Gradient");

            actualColoring.Should().BeEquivalentTo(new GradientColoring());
        }

        [Test]
        public void GetWordColoringByName_ThrowsArgumentException_WhenNameIsInvalid()
        {
            Action action = () => new WordColoringProvider().GetWordColoringByName("bld bla bla");

            action.Should().Throw<ArgumentException>();
        }
    }
}
