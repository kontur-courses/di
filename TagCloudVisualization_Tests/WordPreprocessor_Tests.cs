using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using TagsCloudVisualization.TextFilters;
using TagsCloudVisualization.TextPreprocessing;
using TagsCloudVisualization.WordConverters;

namespace TagsCloudVisualization_Tests
{
    public class WordPreprocessor_Tests
    {
        [Test]
        public void TextPreparer_DifferentRegistry_ShouldReturnLowerCaseWords()
        {
            var words = new List<string>{"У", "РЖД",  "можно", "ареНдовать", "вагонЫ", "разного", "Класса"};
            var preprocessedWords = new WordPreprocessor(new List<ITextFilter>(),
                new List<IWordConverter>() {new LowerCaseWordConverter()}).GetPreprocessedWords(words);
            var preprocessedText = string.Join(string.Empty, preprocessedWords);
            var upperSymbolsCount = preprocessedText.Count(char.IsUpper);
            upperSymbolsCount.Should().Be(0);
        }
    }
}