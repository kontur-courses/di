using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using TagsCloudTextProcessing.Splitters;

namespace TagsCloudTextProcessing.Tests
{
    [TestFixture]
    public class TextSplitterTests
    {
        [Test]
        public void SplitText_Should_SplitTextWithDefaultWordsAndNumbersPattern()
        {
            var textSplitter = new TextSplitter();
            
            var words = textSplitter.SplitText("cat. \"cat?\" %cat1!@");
            
            words.Should().BeEquivalentTo("cat", "cat", "cat1");
        }
        
        [TestCase(@"[0-9]+", "23!cat2342c.at34c at600",new []{"!cat", "c.at", "c at"},TestName = "Split by numbers")]
        [TestCase(@"\s+", "c.at? c3at cat*",new []{"c.at?", "c3at", "cat*"},TestName = "Split by spaces")]
        public void SplitText_Should_SplitTextWithCustomPattern( string pattern, string text,string[] expected)
        {
            var textSplitter = new TextSplitter(pattern);
            
            var words = textSplitter.SplitText(text);

            words.Should().BeEquivalentTo(expected);
        }
        
        [Test]
        public void SplitText_ShouldRemoveEmptyWords()
        {
            var textPreparer = new TextSplitter();
            var words = textPreparer.SplitText("!?");
            words.Count().Should().Be(0);
        }
    }
}