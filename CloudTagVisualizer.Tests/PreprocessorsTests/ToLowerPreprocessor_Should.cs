using System;
using FluentAssertions;
using NUnit.Framework;
using Visualization.Preprocessors;

namespace CloudTagVisualizer.Tests.PreprocessorsTests
{
    [TestFixture]
    public class ToLowerPreprocessor_Should
    {
        private readonly ToLowerPreprocessor preprocessor = new();

        [TestCaseSource(typeof(ToLowerPreprocessorTestCases), nameof(ToLowerPreprocessorTestCases.SameValueCases))]
        public void ShouldReturnSameArray_When(string[] input)
        {
            var result = preprocessor.Preprocess(input);

            result.Should().BeEquivalentTo(input);
        }

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

    internal class ToLowerPreprocessorTestCases
    {
        public static object[] SameValueCases =
        {
            new object[] {Array.Empty<string>()},
            new object[] {new[] {"abc", "def", "qwe"}},
            new object[] {new[] {"123", "456", "789"}}
        };
    }
}