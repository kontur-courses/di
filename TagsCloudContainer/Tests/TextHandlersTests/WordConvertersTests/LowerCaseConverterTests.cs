using FluentAssertions;
using NUnit.Framework;
using TagsCloudContainer.Core.TextHandler.WordConverters;

namespace TagsCloudContainer.Tests.TextHandlersTests.WordConvertersTests
{
    [TestFixture]
    class LowerCaseConverterTests
    {
        [TestCase("UPPER", "upper", TestName = "WhenInputWordIsUppercase")]
        [TestCase("MiXeD", "mixed", TestName = "WhenInputWordIsMixed")]
        [TestCase("lower", "lower", TestName = "WhenInputWordIsLowercase")]
        public void Convert_ReturnsLowercaseWord(string word, string expected)
        {
            var converter = new LowerCaseConverter();
            converter.Convert(word).Should().Be(expected);
        }
    }
}