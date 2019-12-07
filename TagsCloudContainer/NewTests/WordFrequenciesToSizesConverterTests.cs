using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;
using TagsCloudContainer.WordProcessor;
using TagsCloudContainer.WordsToSizesConverter;

namespace TagsCloudContainer.NewTests
{
    class WordFrequenciesToSizesConverterTests
    {
        private readonly WordFrequenciesToSizesConverter converter = new WordFrequenciesToSizesConverter();

        [Test]
        public void ConvertToSizes_ShouldIncreaseSizeInProportionToFrequency()
        {
            var wordFrequencies = new List<WordWithCount>
            {
                new WordWithCount("a", 1),
                new WordWithCount("b", 2)
            };

            var sizes = converter.ConvertToSizes(wordFrequencies);

            var expected = new List<Size>
            {
                new Size(16, 20),
                new Size(32, 40)
            };
            sizes.Should().BeEquivalentTo(expected);
        }

        [Test]
        public void ConvertToSizes_ShouldIncreaseSizeInProportionToWordLength()
        {
            var wordFrequencies = new List<WordWithCount>
            {
                new WordWithCount("w", 1),
                new WordWithCount("word", 1)
            };

            var sizes = converter.ConvertToSizes(wordFrequencies);

            var expected = new List<Size>
            {
                new Size(16, 20),
                new Size(64, 20)
            };
            sizes.Should().BeEquivalentTo(expected);
        }
    }
}
