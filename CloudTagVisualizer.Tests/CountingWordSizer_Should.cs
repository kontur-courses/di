using System;
using System.Collections.Generic;
using System.Drawing;
using FluentAssertions;
using NUnit.Framework;
using Visualization;

namespace CloudTagVisualizer.Tests
{
    [TestFixture]
    public class CountingWordSizer_Should
    {
        private readonly CountingWordSizer sizer = new();

        [TestCase(0, new[] {"abc"}, TestName = "When font size is zero")]
        [TestCase(-1, new[] {"abc"}, TestName = "When font size is negative")]
        [TestCase(1, null, TestName = "When input words array is null")]
        [TestCase(1, new[] {(string) null}, TestName = "When words contains null")]
        public void Throw_When(float fontSize, string[] words)
        {
            Action action = () => sizer.Convert(words, fontSize);
            action.Should().Throw<ArgumentException>();
        }

        [Test]
        public void ReturnEmpty_WhenNoWordsGiven()
        {
            var words = Array.Empty<string>();
            var fontSize = 20;

            var result = sizer.Convert(words, fontSize);

            result.Should().BeEmpty();
        }


        [Test]
        public void ReturnSizeWithWidthEqualToWordLength()
        {
            var words = new[] {"a", "ab", "abc", "abcdef"};
            var fontSize = 20;

            var expected = new List<SizedWord>()
            {
                new("a", new Size(1 * fontSize, fontSize)),
                new("ab", new Size(2 * fontSize, fontSize)),
                new("abc", new Size(3 * fontSize, fontSize)),
                new("abcdef", new Size(6 * fontSize, fontSize))
            };

            var result = sizer.Convert(words, fontSize);

            result.Should().BeEquivalentTo(
                expected,
                config => config.WithoutStrictOrdering());
        }

        [Test]
        public void ScaleSizeHeightDueToTheCountWord()
        {
            var words = new[] {"a", "a", "a", "abc", "abc", "abcdef"};
            var fontSize = 20;

            var expected = new List<SizedWord>()
            {
                new("a", new Size(3 * fontSize, 3 * fontSize)),
                new("abc", new Size(3 * 2 * fontSize, 2 * fontSize)),
                new("abcdef", new Size(6 * fontSize, fontSize))
            };

            var result = sizer.Convert(words, fontSize);

            result.Should().BeEquivalentTo(
                expected,
                config => config.WithoutStrictOrdering());
        }
    }
}