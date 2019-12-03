using NUnit.Framework;
using FluentAssertions;
using TagCloud;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace TagCloudTests
{
    [TestFixture]
    public class DefaultExtracterTests
    {
        public class ExtractWordTokensShould
        {
            private DefaultExtracter defaultExtracter;
            private DefaultParser defaultParser;

            [SetUp]
            public void BaseSetUp()
            {
                defaultParser = new DefaultParser();
                defaultExtracter = new DefaultExtracter(defaultParser);
            }

            [Test]
            public void ThrowArgumentNullException_OnNullText()
            {
                Action action = () => defaultExtracter.ExtractWordTokens(null);
                action.Should().Throw<ArgumentNullException>();
            }

            [TestCase("", ExpectedResult = 0)]
            [TestCase("\r\n", ExpectedResult = 0)]
            [TestCase("\r\n\r\n\r\n", ExpectedResult = 0)]
            [TestCase("Foo", ExpectedResult = 1)]
            [TestCase("Foo\r\n", ExpectedResult = 1)]
            [TestCase("Foo\r\nFoo", ExpectedResult = 1)]
            [TestCase("Foo\r\nBar", ExpectedResult = 2)]
            public int ReturnCorrectNumberOfWordTokens(string text)
            {
                return defaultExtracter.ExtractWordTokens(text).Count;
            }

            [Test]
            public void ParseCorrectly_WithDefaultParser()
            {
                defaultExtracter.ExtractWordTokens("TeXtPaRsEr")
                    .Select(token => token.Value)
                    .Take(1)
                .Should().BeEquivalentTo("textparser");
            }

            private class CustomParser : IParser
            {
                public string Parse(string word) => word.ToUpper();
            }

            [Test]
            public void ParseCorrectly_WithCustomParser()
            {
                var parser = new CustomParser();
                var extracter = new DefaultExtracter(parser);
                extracter.ExtractWordTokens("TeXtPaRsEr")
                    .Select(token => token.Value)
                    .Take(1)
                .Should().BeEquivalentTo("TEXTPARSER");
            }

            [Test]
            public void CountWordsCorrectly()
            {
                var text = "";
                var word = "tibet";
                for (int i = 0; i < word.Length; ++i)
                    text += word.Substring(0, i) +
                        word[i].ToString().ToUpper() +
                        word.Substring(i + 1) +
                        "\r\n";
                defaultExtracter.ExtractWordTokens(text)
                    .Select(token => token.Count)
                    .First()
                .Should().Be(5);
            }

            [Test]
            public void ExtractFast()
            {
                var sb = new StringBuilder();
                var word = "Mississippi";
                for (int i = 0; i < 1000000; ++i)
                    sb.Append(word + "\r\n");
                var text = sb.ToString();
                var stopwatch = new Stopwatch();
                stopwatch.Start();
                var wordTokens = defaultExtracter.ExtractWordTokens(text);
                stopwatch.Stop();
                stopwatch.ElapsedMilliseconds.Should().BeLessThan(1000);
            }
        }
    }
}
