using System.Linq;
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
            var expected = new[] { "hello", "world" };
            var words = selector.GetWords(text);
            words.Should().BeEquivalentTo(expected);
        }

        [TestCase("qwerty")]
        [TestCase("123")]
        [TestCase("a12")]
        [TestCase("a1a")]
        [TestCase("-5")]
        [TestCase(".net")]
        [TestCase("e-maxx.ru")]
        [TestCase("12_13_14")]
        [TestCase("don't")]
        public void Selector_Should_NotSeparate(string text)
        {
            var words = selector.GetWords(text);
            words.Should().HaveCount(1).And.Contain(text);
        }

        [TestCase("abc!123", new[]{"abc", "123"})]
        [TestCase("abc abc", new []{"abc", "abc"})]
        [TestCase("abc:123", new []{"abc", "123"})]
        [TestCase("abc.", new []{"abc"})]
        public void Selector_Should_Separate(string text, string[] expectedWords)
        {
            var words = selector.GetWords(text);
            words.Should().BeEquivalentTo(expectedWords);
        }

        [Test]
        public void Selector_Ignore_EndDot()
        {
            var text = "abc. . .net5.0 something.";
            var expected = new[] { "abc",".net5.0","something"};
            var words = selector.GetWords(text);
            words.Should().BeEquivalentTo(expected);
        }
    }
}