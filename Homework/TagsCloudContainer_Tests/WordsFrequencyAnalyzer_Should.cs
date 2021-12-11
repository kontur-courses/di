using System;
using FluentAssertions;
using NUnit.Framework;
using TagsCloudContainer.WordsFrequencyAnalyzers;

namespace TagsCloudContainer_Tests
{
    [TestFixture]
    public class WordsFrequencyAnalyzer_Should
    {
        private readonly IWordsFrequencyAnalyzer sut = new WordsFrequencyAnalyzer();

        [Test]
        public void ReturnEmptyDict_WhenEmptyInput()
        {
            var input = Array.Empty<string>();
            sut.GetWordsFrequency(input).Should().BeEmpty();
        }

        [Test]
        public void ReturnCorrectDict_WhenNoRepeats()
        {
            var input = new[]
            {
                "привет",
                "мир",
                "война"
            };
            var result = sut.GetWordsFrequency(input);
            result.Should()
                .HaveCount(3)
                .And
                .OnlyContain(kv => kv.Value == 1);
        }

        [Test]
        public void ReturnCorrectDict_WhenWordsRepeat()
        {
            var input = new[]
            {
                "привет",
                "привет",
                "мир"
            };
            var result = sut.GetWordsFrequency(input);
            result.Should()
                .HaveCount(2)
                .And
                .Contain(kv => kv.Key == "привет" && kv.Value == 2);
        }
    }
}