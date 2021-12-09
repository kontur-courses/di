using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using NUnit.Framework;
using TagsCloudApp.Parsers;

namespace TagsCloud.Tests
{
    public class ImageFormatParserTest
    {
        private ImageFormatParser parser;

        [SetUp]
        public void SetUp()
        {
            parser = new ImageFormatParser();
        }

        [Test]
        public void Parse_ThrowsException_WithIncorrectValue() =>
            Assert.Throws<ApplicationException>(() => parser.Parse("esf"));

        [TestCaseSource(nameof(ParseCases))]
        public ImageFormat Parse_ReturnFormat_WithCorrectValue(string value) =>
            parser.Parse(value);

        public static IEnumerable<TestCaseData> ParseCases()
        {
            yield return new TestCaseData("bmp") {ExpectedResult = ImageFormat.Bmp, TestName = "Lowercase name"};
            yield return new TestCaseData("PNG") {ExpectedResult = ImageFormat.Png, TestName = "Uppercase name"};
        }
    }
}