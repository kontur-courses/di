using FluentAssertions;
using NUnit.Framework;
using TagsCloudVisualization.Default;

namespace TagCloudTests
{
    [TestFixture]
    public class WordSelectorTests
    {
        private WordSelector selector;
        
        [SetUp]
        public void Setup()
        {
            selector = new WordSelector();
        }

        [Test]
        public void Selector_EmptyString_NoWords()
        {
            var words = selector.GetWords("");
            words.Should().BeEmpty();
        }

        [Test]
        public void Selector_WordsShould_BeLowercase()
        {
            var text = "HeLLo wOrld";
            var words = selector.GetWords(text);
            var expected = new[] { "hello", "world" };
            words.Should().BeEquivalentTo(expected);
        }

        [Test]
        public void Selector_UnderLineAndDot_NotSeparated()
        {
            var text = "hel_lo wor.ld";
            var words = selector.GetWords(text);
            var expected = new[] { "hel_lo", "wor.ld"};
            words.Should().BeEquivalentTo(expected);
        }

        [Test]
        public void Selector_NotDigitsOrLetter_Separates()
        {
            var text = "0!1?2:3;4'5";
            var words = selector.GetWords(text);
            var expected = new[] { "0","1","2","3","4","5"};
            words.Should().BeEquivalentTo(expected);
        }
    }
}