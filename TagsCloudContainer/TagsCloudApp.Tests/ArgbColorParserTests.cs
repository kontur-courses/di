using System;
using System.Drawing;
using FluentAssertions;
using NUnit.Framework;
using TagsCloudApp;

namespace TagsCloud.Tests
{
    public class ArgbColorParserTests
    {
        private ArgbColorParser parser;

        [SetUp]
        public void SetUp()
        {
            parser = new ArgbColorParser();
        }

        [TestCase("sef")]
        [TestCase("1 1 1 1 1")]
        [TestCase("1 1 1")]
        public void Parse_WithIncorrectValue_ThrowsException(string value) =>
            Assert.Throws<ApplicationException>(() => parser.Parse(value));

        [Test]
        [Repeat(25)]
        public void Parse_WithRandomValue_ReturnColor()
        {
            var random = new Random();
            var colorBytes = new byte[4];
            random.NextBytes(colorBytes);
            var value = string.Join(' ', colorBytes);

            parser.Parse(value)
                .Should().Be(Color.FromArgb(colorBytes[0], colorBytes[1], colorBytes[2], colorBytes[3]));
        }
    }
}