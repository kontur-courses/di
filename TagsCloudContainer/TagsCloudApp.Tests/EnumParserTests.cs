using System;
using System.Reflection;
using FluentAssertions;
using NUnit.Framework;
using TagsCloudApp.Parsers;
using TagsCloudContainer.Preprocessing;

namespace TagsCloud.Tests
{
    public class EnumParserTests
    {
        private EnumParser parser;

        [SetUp]
        public void SetUp()
        {
            parser = new EnumParser();
        }

        [TestCase("A", ExpectedResult = SpeechPart.A)]
        [TestCase("ADV", ExpectedResult = SpeechPart.ADV)]
        public SpeechPart Parse_ReturnCorrectValue(string value) =>
            parser.Parse<SpeechPart>(value);

        [TestCase("all")]
        [TestCase("ALL")]
        public void Parse_IgnoreCase(string value)
        {
            parser.Parse<MemberTypes>(value)
                .Should().Be(MemberTypes.All);
        }

        [Test]
        public void Parse_ThrowsException_WithIncorrectValue() =>
            Assert.Throws<ApplicationException>(() => parser.Parse<SpeechPart>("QWE"));
    }
}