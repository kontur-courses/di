using System;
using NUnit.Framework;
using TagsCloudApp;
using TagsCloudContainer.Preprocessing;

namespace TagsCloud.Tests
{
    public class SpeechPartParserTests
    {
        private SpeechPartParser parser;

        [SetUp]
        public void SetUp()
        {
            parser = new SpeechPartParser();
        }

        [TestCase("A", ExpectedResult = SpeechPart.A)]
        [TestCase("ADV", ExpectedResult = SpeechPart.ADV)]
        public SpeechPart Parse_ReturnSpeechPart_WithCorrectValue(string value) =>
            parser.Parse(value);

        [Test]
        public void Parse_ThrowsException_WithIncorrectValue() =>
            Assert.Throws<ApplicationException>(() => parser.Parse("QWE"));
    }
}