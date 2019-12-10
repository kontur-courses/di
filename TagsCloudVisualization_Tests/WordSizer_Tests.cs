using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using TagsCloudVisualization.WordSizing;

namespace TagsCloudVisualization_Tests
{
    public class WordSizer_Tests
    {
        [Test]
        public void FrequencyWordSizer_UniqueWords_ShouldReturnResultWithEqualSizes()
        {
            var words = new List<string> {"один", "два", "три", "четыре", "пять"};
            var sizedWords = new FrequencyWordSizer().GetSizedWords(words);
            var size = sizedWords.First().Size;
            sizedWords.All(word => word.Size == size).Should().BeTrue();
        }

        [Test]
        public void FrequencyWordSizer_RepeatingWord_ShouldBeGreaterThanUniqueWord()
        {
            var uniqueWord = "один";
            var repeatingWord = "два";
            var words = new List<string> {uniqueWord, repeatingWord, repeatingWord};
            var sizedWords = new FrequencyWordSizer().GetSizedWords(words);
            sizedWords.First(word => word.Value == repeatingWord).Size.Should()
                .BeGreaterThan(sizedWords.First(word => word.Value == uniqueWord).Size);
        }
    }
}