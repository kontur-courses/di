using System;
using System.Collections.Generic;
using System.IO;
using FluentAssertions;
using Moq;
using NUnit.Framework;

namespace TagsCloudVisualization
{
    [TestFixture]
    public class WordsAnalyzer_Mock
    {
        private Mock<IBoringWordDeterminer> mockBoringWord;
        private Mock<IReader> mockReader;
        private List<string> input;

        [SetUp]
        public void SetUp()
        {
            Directory.SetCurrentDirectory(AppDomain.CurrentDomain.BaseDirectory);
            mockBoringWord = new Mock<IBoringWordDeterminer>();
            mockReader = new Mock<IReader>();
            input = new List<string>() { "Where", "iS", "my", "Mind" };
        }

        [Test]
        public void SimpleMockTest()
        {
            mockBoringWord.Setup(x => x.IsBoringWord(It.IsAny<string>()))
                .Returns(false);
            mockReader.Setup(x => x.ReadWords())
                .Returns(input);
            var expected = new Dictionary<string, int>()
            {
                {"where", 1},
                {"is", 1},
                {"my", 1},
                {"mind", 1}
            };
            var actual = new WordsAnalyzer(
                mockBoringWord.Object, mockReader.Object, 50, 0).GetWordsFrequensy();
            actual.ShouldBeEquivalentTo(expected);
        }

        [Test]
        public void MockTest_WithBoringList()
        {
            var stopWords = new List<string>() {"is", "my"};
            mockBoringWord.Setup(x => x.IsBoringWord(It.IsAny<string>()))
                .Returns((string s) => stopWords.Contains(s));
            mockReader.Setup(x => x.ReadWords())
                .Returns(input);
            var expected = new Dictionary<string, int>()
            {
                {"where", 1},
                {"mind", 1}
            };
            var actual = new WordsAnalyzer(
                mockBoringWord.Object, mockReader.Object, 50, 0).GetWordsFrequensy();
            
            actual.ShouldBeEquivalentTo(expected);
        }
    }
}