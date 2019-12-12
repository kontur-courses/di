using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using TagsCloudTextProcessing.Tokenizers;

namespace TagsCloudTextProcessing.Tests
{
    [TestFixture]
    public class TokenizerTests
    {
        [Test]
        public void Tokenize_Should_SplitTextWithDefaultWordsAndNumbersPattern()
        {
            var textSplitter = new Tokenizer();
            
            var words = textSplitter.Tokenize("cat. \"cat?\" %cat1!@");
            
            words.Should().BeEquivalentTo("cat", "cat", "cat1");
        }
        
        [TestCase(@"[0-9]+", "23!cat2342c.at34c at600",new []{"!cat", "c.at", "c at"},TestName = "Split by numbers")]
        [TestCase(@"\s+", "c.at? c3at cat*",new []{"c.at?", "c3at", "cat*"},TestName = "Split by spaces")]
        public void Tokenize_Should_SplitTextWithCustomPattern( string pattern, string text,string[] expected)
        {
            var textSplitter = new Tokenizer(pattern);
            
            var words = textSplitter.Tokenize(text);

            words.Should().BeEquivalentTo(expected);
        }
        
        [Test]
        public void Tokenize_Should_RemoveEmptyWords()
        {
            var textPreparer = new Tokenizer();
            var words = textPreparer.Tokenize("!?");
            words.Count().Should().Be(0);
        }
    }
}