using System;
using System.Collections.Generic;
using FluentAssertions;
using NUnit.Framework;
using TagCloud.Implementations;
using Moq;
using TagCloud;
using TagCloud.Interfaces;

namespace TagCloudMockTests
{
    [TestFixture]
    public class MockTests
    {
        [Test]
        public void WordProcessor_ExcludBorringWords()
        {
            var shellMock = new Mock<IMystemShell>();
            shellMock.Setup(s => s.GetInterestingWords(It.IsAny<string>()))
                .Returns(new List<string> { "я{я=PR", "не{не=PART", "мама{мама=V" });

            var processor = new WordProcessor(new string[0], shellMock.Object);

            processor.GetFrequencyDictionary("").GetValueOrThrow().Keys.Count.ShouldBeEquivalentTo(1);
        }

        [Test]
        public void WordProcessor_ExcludBadWords()
        {
            var shellMock = new Mock<IMystemShell>();
            shellMock.Setup(s => s.GetInterestingWords(It.IsAny<string>())).Returns(new List<string> {"мама{", "мыла{", "раму{"});
            
            var processor = new WordProcessor(new []{"раму"}, shellMock.Object);

            processor.GetFrequencyDictionary("").GetValueOrThrow().ContainsKey("раму").Should().BeFalse();
        }
    }
}
