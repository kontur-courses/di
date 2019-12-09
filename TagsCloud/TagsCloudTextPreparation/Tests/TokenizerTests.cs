using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using TagsCloudTextPreparation.Tokenizers;

namespace TagsCloudTextPreparation.Tests
{
    [TestFixture]
    public class TokenizerTests
    {
        [Test]
        public void GetPreparedText_Should_CountWordsOccurrences()
        {
            var words = new[] {"cat", "cat", "dog", "dog", "dog"};
            
            var tokens =  new Tokenizer().Tokenize(words);

            tokens
                .Should()
                .BeEquivalentTo(new Token("cat", 2), new Token("dog", 3));
        }
        
        [Test]
        public void GetPreparedText_Should_Not_ContainRepeatedWords()
        {
            var words = new[] {"words", "words", "words"};
            
            var tokens =  new Tokenizer().Tokenize(words);

            tokens.Select(f => f.Word).Should().BeEquivalentTo("words");
        }
    }
}