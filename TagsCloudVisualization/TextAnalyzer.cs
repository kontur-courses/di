using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using FluentAssertions;
using Moq;
using NHunspell;
using NUnit.Framework;

namespace TagsCloudVisualization
{
    public class FileAnalyzer:IFileAnalyzer
    {
        private readonly int count;
        private readonly int minLength;
        private readonly IBoringWordDeterminer boringWordDeterminer;

        public FileAnalyzer( 
            IBoringWordDeterminer boringWordDeterminer,
            int count, 
            int minLength = 0)
        {
            this.count = count;
            this.minLength = minLength;
            this.boringWordDeterminer = boringWordDeterminer;
        }

        public Dictionary<string, int> GetWordsFrequensy(IEnumerable<string> input)
        {
            using (var hunspell = new Hunspell("dictionaries/en_US.aff", "dictionaries/en_US.dic"))
            {
                return input
                    .Select(x =>
                    {
                        var word = x.ToLower();
                        var stems = hunspell.Stem(word);
                        return stems.Any() ? stems[0] : word;
                    })
                    .Where(word => !boringWordDeterminer.IsBoringWord(word))
                    .Where(word => word.Length > minLength)
                    .GroupBy(word => word)
                    .OrderByDescending(x => x.Count())
                    .Take(count)
                    .ToDictionary(x => x.Key, x => x.Count());
            }
        }
        
    }

    [TestFixture]
    public class TextAnalyzer_Mock
    {
        [Test]
        
        public void SimpleMockTest()
        {
            Directory.SetCurrentDirectory(AppDomain.CurrentDomain.BaseDirectory);
            var input = new List<string>(){"What", "is", "the", "reason"};
            var stopWords = new List<string>(){"is", "the", "are"};
            var mock = new Mock<IBoringWordDeterminer>();
            mock.Setup(x => x.IsBoringWord(It.IsAny<string>()))
                .Returns((string s) => stopWords.Contains(s));
            var expected = new Dictionary<string, int>()
            {
                {"what",1},
                {"reason",1}
            };
            var actual = new FileAnalyzer(mock.Object, 50, 0).GetWordsFrequensy(input);
            actual.ShouldBeEquivalentTo(expected);
        }
    }
    
    [TestFixture]
    public class FileAnalyzer_Should
    {
        [Test]        
        public void DoSomething_WhenSomething()
        {
            using (var hunspell = new Hunspell(
                TestContext.CurrentContext.TestDirectory+"\\en_US.aff", 
                TestContext.CurrentContext.TestDirectory+"\\en_US.dic"))
            {
                var stems = hunspell.Stem("decompressed");
                var actualWord = stems[0];
                var expected = "compress";
                actualWord.Should().Be(expected);
            }
        }
    }
}