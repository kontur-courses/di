using System;
using CloudTagContainer;
using FluentAssertions;
using NUnit.Framework;

namespace CloudTagContainerTests.PreprocessorsTests
{
    [TestFixture]
    public class ToLowerPreprocessor_Should
    {
        private readonly ToLowerPreprocessor preprocessor = new();
        
        //Не понял причину, но почему-то здесь, если убрать "int _", то компилороваться не будет
        [TestCase(1, new string[0], TestName = "When empty array given")]
        [TestCase(1, new[] {"abc"}, TestName = "When one lower word given")]
        [TestCase(1, new[] {"abc", "def", "qwe"}, TestName = "When one lower word given")]
        [TestCase(1, new[] {"123", "456", "789"}, TestName = "When numbers given")]
        public void ShouldReturnSameArray_When(int _, string[] input)
        {
            var result = preprocessor.Preprocess(input);

            result.Should().BeEquivalentTo(input);
        }

        [TestCase(new[] {"A"}, new[] {"a"}, TestName = "When one word given")]
        [TestCase(new[] {"aBCDe"}, new[] {"abcde"}, TestName = "When not the whole word was is upper case")]
        [TestCase(new[] {"abc", "aBc", "ABC"},
            new[] {"abc", "abc", "abc"},
            TestName = "When not the whole word was in upper case")]
        public void ReturnLowerValues_When(string[] rawWords, string[] expected)
        {
            var result = preprocessor.Preprocess(rawWords);

            result.Should().BeEquivalentTo(expected);
        }
    }
}