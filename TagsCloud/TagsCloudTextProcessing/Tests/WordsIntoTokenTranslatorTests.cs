using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using TagsCloudTextProcessing.WordsIntoTokensTranslators;

namespace TagsCloudTextProcessing.Tests
{
    [TestFixture]
    public class WordsIntoTokenTranslatorTests
    {
        [Test]
        public void TranslateIntoTokens_Should_CountWordsOccurrences()
        {
            var words = new[] {"cat", "cat", "dog", "dog", "dog"};
            
            var tokens =  new WordsIntoTokenTranslator().TranslateIntoTokens(words);

            tokens
                .Should()
                .BeEquivalentTo(new Token("cat", 2), new Token("dog", 3));
        }
        
        [Test]
        public void TranslateIntoTokens_Should_Not_ContainRepeatedWords()
        {
            var words = new[] {"words", "words", "words"};
            
            var tokens =  new WordsIntoTokenTranslator().TranslateIntoTokens(words);

            tokens.Select(f => f.Word).Should().BeEquivalentTo("words");
        }
    }
}