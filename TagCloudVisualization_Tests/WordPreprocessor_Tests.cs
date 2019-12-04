using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using TagsCloudVisualization;

namespace TagCloudVisualization_Tests
{
    public class WordPreprocessor_Tests
    {
        [Test]
        public void TextPreparer_DifferentRegistry_ShouldReturnLowerCaseWords()
        {
            var words = new List<string>{"У", "РЖД",  "можно", "ареНдовать", "вагонЫ", "разного", "Класса"};
            var preprocessedWords = new WordPreprocessor(words, new List<ITextFilter>()).GetPreprocessedWords();
            var preprocessedText = string.Join("", preprocessedWords);
            var upperSymbolsCount = preprocessedText.Count(char.IsUpper);
            upperSymbolsCount.Should().Be(0);
        }
    }
}