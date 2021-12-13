using System;
using Visualization;
using FluentAssertions;
using NUnit.Framework;

namespace CloudTagContainerTests.PreprocessorsTests
{
    [TestFixture]
    public class ToLowerPreprocessor_Should
    {
        private readonly ToLowerPreprocessor preprocessor = new();

        [TestCaseSource(typeof(TestCases), nameof(TestCases.SameValueCases))]
        public void ShouldReturnSameArray_When(string[] input)
        {
            var result = preprocessor.Preprocess(input);

            result.Should().BeEquivalentTo(input);
        }

        [TestCase(new[] {"A"}, new[] {"a"}, TestName = "When one word given")]
        [TestCase(new[] {"aBCDe"}, new[] {"abcde"}, TestName = "When not the whole word was is upper case")]
        [TestCase(new[] {"abc", "aBc", "ABC"},
            new[] {"abc", "abc", "abc"},
            TestName = "Word repeated")]
        public void ReturnLowerValues_When(string[] rawWords, string[] expected)
        {
            var result = preprocessor.Preprocess(rawWords);

            result.Should().BeEquivalentTo(expected);
        }
    }

    internal class TestCases
    {
        public static object[] SameValueCases =
        {
            new object[] {Array.Empty<string>()},
            new object[] {new[] {"abc"}},
            new object[] {new[] {"abc", "def", "qwe"}},
            new object[] {new[] {"123", "456", "789"}}
        };
    }
}