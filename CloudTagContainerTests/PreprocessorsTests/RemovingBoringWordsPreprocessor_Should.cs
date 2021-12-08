using System;
using CloudTagContainer;
using FluentAssertions;
using NUnit.Framework;

namespace CloudTagContainerTests.PreprocessorsTests
{
    [TestFixture]
    public class RemovingBoringWordsPreprocessor_Should
    {
        [TestCase(0, TestName = "When min word length is zero")]
        [TestCase(-1, TestName = "When min word length is negative")]
        public void Throw_When(int minWordLength)
        {
            Action action = () => new RemovingBoringWordsPreprocessor(minWordLength);

            action.Should().Throw<ArgumentException>();
        }

        [TestCase(1, new string[0], TestName = "When empty array given")]
        [TestCase(1, new[] {"abc"}, TestName = "When one word with allowed length given")]
        [TestCase(3, new[] {"aaa", "bbb", "ccc"}, TestName = "When all values has minimal allowed length")]
        [TestCase(1, new[] {"abc", "def", "qwe"}, TestName = "When multiplied word with allowed length given")]
        public void ReturnSameArray_When(int minWordLength, string[] input)
        {
            var preprocessor = new RemovingBoringWordsPreprocessor(minWordLength);
            var result = preprocessor.Preprocess(input);

            result.Should().BeEquivalentTo(input);
        }

        [TestCase(3,
            new[] {"a", "ab"},
            new string[0],
            TestName = "When all values does not match allowed length")]
        [TestCase(3,
            new[] {"a", "abcdef", "ab"},
            new[] {"abcdef"},
            TestName = "When some values does not match allowed length")]
        public void ReturnTrimmedArray_When(int minWordLength, string[] input, string[] expected)
        {
            var preprocessor = new RemovingBoringWordsPreprocessor(minWordLength);

            var result = preprocessor.Preprocess(input);

            result.Should().BeEquivalentTo(expected);
        }
    }
}