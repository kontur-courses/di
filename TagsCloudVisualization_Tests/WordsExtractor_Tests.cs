using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using TagsCloudVisualization.TextPreprocessing;

namespace TagsCloudVisualization_Tests
{
    public class WordsExtractor_Tests
    {
        [Test]
        public void WordsExtractor_DifferentWords_ShouldReturnRightWordsCount()
        {
            var text = "У РЖД можно арендовать вагоны разного Класса и прицеплять их К поездам в нужных направлениях.";
            var words = new WordsExtractor().GetWords(text);
            words.Count().Should().Be(15);
        }
        
        [Test]
        public void WordsExtractor_TextWithSigns_ShouldReturnWordsWithoutSigns()
        {
            var text = "У РЖД можно арендовать вагоны разного! Класса,.";
            var words = new WordsExtractor().GetWords(text);
            var expectedResult = new List<string>{"У", "РЖД",  "можно", "арендовать", "вагоны", "разного", "Класса"};
            words.Should().BeEquivalentTo(expectedResult);
        }
        
        [Test]
        public void WordsExtractor_WordsWithHyphen_ShouldBeRightProcessed()
        {
            var text = "как-нибудь темно-русый человек";
            var words = new WordsExtractor().GetWords(text);
            var expectedResult = new List<string>{"как-нибудь", "темно-русый",  "человек"};
            words.Should().BeEquivalentTo(expectedResult);
        }
        
        [Test]
        public void WordsExtractor_WordsWithoutSpacesBetween_ShouldReturnWordsSplitBySigns()
        {
            var text = "У РЖД можно арендовать,вагоны разного!Класса,.";
            var words = new WordsExtractor().GetWords(text);
            var expectedResult = new List<string>{"У", "РЖД",  "можно", "арендовать", "вагоны", "разного", "Класса"};
            words.Should().BeEquivalentTo(expectedResult);
        }
        
        [Test]
        public void WordsExtractor_WordsWithSpecialSymbols_ShouldReturnRightResult()
        {
            var text = "\t\tУ РЖД можно\r арендовать,вагоны\n разного!Класса,.";
            var words = new WordsExtractor().GetWords(text);
            var expectedResult = new List<string>{"У", "РЖД",  "можно", "арендовать", "вагоны", "разного", "Класса"};
            words.Should().BeEquivalentTo(expectedResult);
        }
    }
}