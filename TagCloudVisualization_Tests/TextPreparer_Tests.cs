using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using TagsCloudVisualization.TextPreprocessing;

namespace TagsCloudVisualization.Tests
{
    public class TextPreparer_Tests
    {
        [Test]
        public void TextPreparer_DifferentWords_ShouldReturnRightWordsCount()
        {
            var text = "У РЖД можно арендовать вагоны разного Класса и прицеплять их К поездам в нужных направлениях.";
            var words = new WordsProvider().GetWords(text);
            words.Count().Should().Be(15);
        }
        
        [Test]
        public void TextPreparer_TextWithSigns_ShouldReturnWordsWithoutSigns()
        {
            var text = "У РЖД можно арендовать вагоны разного! Класса,.";
            var words = new WordsProvider().GetWords(text);
            var expectedResult = new List<string>{"У", "РЖД",  "можно", "арендовать", "вагоны", "разного", "Класса"};
            words.Should().BeEquivalentTo(expectedResult);
        }
        
        [Test]
        public void TextPreparer_WordsWithHyphen_ShouldBeRightProcessed()
        {
            var text = "как-нибудь темно-русый человек";
            var words = new WordsProvider().GetWords(text);
            var expectedResult = new List<string>{"как-нибудь", "темно-русый",  "человек"};
            words.Should().BeEquivalentTo(expectedResult);
        }
        
        [Test]
        public void TextPreparer_WordsWithoutSpacesBetween_ShouldReturnWordsSplitBySigns()
        {
            var text = "У РЖД можно арендовать,вагоны разного!Класса,.";
            var words = new WordsProvider().GetWords(text);
            var expectedResult = new List<string>{"У", "РЖД",  "можно", "арендовать", "вагоны", "разного", "Класса"};
            words.Should().BeEquivalentTo(expectedResult);
        }
        
        [Test]
        public void TextPreparer_WordsWithSpecialSymbols_ShouldReturnRightResult()
        {
            var text = "\t\tУ РЖД можно\r арендовать,вагоны\n разного!Класса,.";
            var words = new WordsProvider().GetWords(text);
            var expectedResult = new List<string>{"У", "РЖД",  "можно", "арендовать", "вагоны", "разного", "Класса"};
            words.Should().BeEquivalentTo(expectedResult);
        }
    }
}