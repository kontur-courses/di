using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using TagsCloudContainer.Layout;

namespace TagsCloudContainer.Tests
{
    public class FrequencyLinearFontSizeSelectorTests
    {
        private FrequencyLinearFontSizeSelector selector;

        [SetUp]
        public void SetUp()
        {
            selector = new FrequencyLinearFontSizeSelector(new FontSizeRange(101, 1));
        }

        [Test]
        public void GetFontSizeMap_WithTwoValues_ReturnMaxAndMin()
        {
            var words = RepeatWords(("a", 2), ("b", 1));

            selector.GetFontSizes(words)
                .Should().BeEquivalentTo(new List<(string, int)>
                    {
                        ("a", 101),
                        ("b", 1)
                    }
                );
        }

        [Test]
        public void GetFontSizeMap_WithThreeValue()
        {
            var words = RepeatWords(("a", 3), ("b", 2), ("c", 1));
            selector.GetFontSizes(words)
                .Should().BeEquivalentTo(new List<(string, int)>
                {
                    ("a", 101), ("b", 51), ("c", 1)
                });
        }

        [Test]
        public void GetFontSizeMap_WithMinFrequencyEqualsToMaxFrequency_ReturnMiddleSize()
        {
            var words = RepeatWords(("a", 1), ("b", 1));
            selector.GetFontSizes(words)
                .Should().BeEquivalentTo(new List<(string, int)>
                {
                    ("a", 51), ("b", 51)
                });
        }

        [Test]
        public void GetFontSizeMap_WithMaxFontEqualsMinFont_ReturnEqualFontForAllFrequencies()
        {
            var words = RepeatWords(("a", 1), ("b", 1));
            var equalSelector = new FrequencyLinearFontSizeSelector(new FontSizeRange(100, 100));

            equalSelector.GetFontSizes(words)
                .Should().BeEquivalentTo(new List<(string, int)>
                {
                    ("a", 100), ("b", 100)
                });
        }

        [Test]
        public void GetFontSizeMap_WithMaxFontLessThanMinFont_ReturnReversedFontSize()
        {
            var words = RepeatWords(("a", 2), ("b", 1));
            var reversedSelector = new FrequencyLinearFontSizeSelector(new FontSizeRange(1, 101));

            reversedSelector.GetFontSizes(words)
                .Should().BeEquivalentTo(new List<(string, int)>
                {
                    ("a", 1), ("b", 101)
                });
        }


        private static IEnumerable<string> RepeatWords(params (string Word, int Count)[] words)
        {
            var repeatedWords = new List<string>();
            foreach (var (word, count) in words)
                repeatedWords.AddRange(Enumerable.Repeat(word, count));

            return repeatedWords;
        }
    }
}