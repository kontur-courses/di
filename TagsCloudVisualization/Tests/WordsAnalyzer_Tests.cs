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
        private List<string> stopList;
        private WordsAnalyzer wordsAnalyzer;

        [SetUp]
        public void SetUp()
        {
            Directory.SetCurrentDirectory(AppDomain.CurrentDomain.BaseDirectory);
            mockBoringWord = new Mock<IBoringWordDeterminer>();
            mockReader = new Mock<IReader>();
            wordsAnalyzer = new WordsAnalyzer(mockBoringWord.Object, mockReader.Object, 50);
            input = new List<string>() {"Where", "iS", "my", "Mind", "Where", "Is"};
            stopList = new List<string>() {"is", "my", "the"};
        }

        [Test]
        public void GetWordFrequency_ReturnCorrectPairs()
        {
            mockBoringWord.Setup(x => x.IsBoringWord(It.IsAny<string>()))
                .Returns(false);
            mockReader.Setup(x => x.ReadWords())
                .Returns(input);
            var expected = new Dictionary<string, int>()
            {
                {"where", 3},
                {"is", 3},
                {"my", 1},
                {"mind", 1}
            };
            var actual = wordsAnalyzer.GetWordsFrequensy();
            actual.ShouldBeEquivalentTo(expected);
        }

        [Test]
        public void GetWordsFrequency_IgnoreWordsFromStopList()
        {
            
            mockBoringWord.Setup(x => x.IsBoringWord(It.IsAny<string>()))
                .Returns((string s) => stopList.Contains(s));
            mockReader.Setup(x => x.ReadWords())
                .Returns(input);
            var expected = new Dictionary<string, int>()
            {
                {"where", 2},
                {"mind", 1}
            };
            var actual = wordsAnalyzer.GetWordsFrequensy();
            
            actual.ShouldBeEquivalentTo(expected);
        }

        [Test]
        public void GetWordFrequency_IgnoreWordsShortetThanMinLength()
        {
            mockBoringWord.Setup(x => x.IsBoringWord(It.IsAny<string>()))
                .Returns(false);
            mockReader.Setup(x => x.ReadWords())
                .Returns(input);
            var expected = new Dictionary<string, int>()
            {
                {"where", 2},
                {"mind", 1}
            };
            var minLength = 3;
            var actual = new WordsAnalyzer(
                mockBoringWord.Object, mockReader.Object, 50, minLength).GetWordsFrequensy();
            actual.ShouldBeEquivalentTo(expected);    
        }

        [Test]
        public void GetWordFrequency_ReturnSpecificCountOfWordFrequencePairs()
        {
            var input1 = new List<string>()
            {
                "Hello", "darkness,", "my", "old", "friend", "I've", "come", "to", "talk", "with", "you", "again",
                "friends","will","be","friends",
                "Hello", "hello", "hello", "how", "low", "Hello", "hello", "hello", "how", "low" 
            };
            
            mockBoringWord.Setup(x => x.IsBoringWord(It.IsAny<string>()))
                .Returns(false);
            mockReader.Setup(x => x.ReadWords())
                .Returns(input1);
            var count = 4;
            var actual = new WordsAnalyzer(
                mockBoringWord.Object, mockReader.Object, count).GetWordsFrequensy();
            var expected = new Dictionary<string, int>()
            {
                {"hello", 7},
                {"friend", 3},
                {"how", 2},
                {"low", 2}
            };
            actual.ShouldBeEquivalentTo(expected);
        }
    }
}