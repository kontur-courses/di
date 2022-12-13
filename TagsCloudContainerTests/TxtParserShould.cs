using System;
using System.IO;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using TagsCloudVisualization.Infrastructure.Parsers;
using TagsCloudVisualization.Settings;

namespace TagsCloudContainerTests
{
    public class TxtParserShould
    {
        public const string FileName = "READ_TEST.txt";
        private FileStream fileStream;
        private ParserSettings settings;

        [SetUp]
        public void SetUp()
        {
            settings = new ParserSettings();
            fileStream = File.Create(FileName);
        }

        [TearDown]
        public void TearDown()
        {
            fileStream.Dispose();
            File.Delete(FileName);
        }


        [TestCase(EncodingEnum.Utf8)]
        [TestCase(EncodingEnum.Utf32)]
        [TestCase(EncodingEnum.Unicode)]
        public void ReadOneWordOneLineText(EncodingEnum encoding)
        {
            var words = string.Join(Environment.NewLine, "привет", "привет");
            var buffer = new TxtParserHelper().Encodings[encoding].GetBytes(words);
            fileStream.Write(buffer, 0, buffer.Length);
            fileStream.Close();
            settings.Encoding = encoding;
            settings.TextType = TextType.OneWordOneLine;
            var parser = new TxtParser(settings);

            var result = parser.WordParse(Path.GetFullPath(FileName)).ToArray();

            result.Should().BeEquivalentTo(words.Split(Environment.NewLine));
        }

        [TestCase(EncodingEnum.Utf8)]
        [TestCase(EncodingEnum.Utf32)]
        [TestCase(EncodingEnum.Unicode)]
        public void ReadLiteraryText(EncodingEnum encoding)
        {
            var words = @"- Скажи-ка, дядя, ведь не даром
            Москва
            ";
            var expected = new[] { "Скажи", "ка", "дядя", "ведь", "не", "даром", "Москва" };
            var buffer = new TxtParserHelper().Encodings[encoding].GetBytes(words);
            fileStream.Write(buffer, 0, buffer.Length);
            fileStream.Close();
            settings.Encoding = encoding;
            settings.TextType = TextType.LiteraryText;
            var parser = new TxtParser(settings);

            var result = parser.WordParse(FileName).ToArray();

            result.Should().BeEquivalentTo(expected);
        }
    }
}