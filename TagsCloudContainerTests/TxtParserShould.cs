using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.Design;
using FakeItEasy;
using FluentAssertions;
using NUnit.Framework;
using TagsCloudVisualization.Infrastructure;
using TagsCloudVisualization.Infrastructure.Parsers;
using TagsCloudVisualization.Settings;

namespace TagsCloudContainerTests
{
    public class TxtParserShould
    {
        public const string FileName = "READ_TEST.txt";
        private ICurrentTextFileProvider fileProvider;
        private ParserSettings settings;
        private FileStream fileStream;

        [SetUp]
        public void SetUp()
        {
            settings = new ParserSettings();
            fileStream = File.Create(FileName);
            fileProvider = A.Fake<ICurrentTextFileProvider>();
            A.CallTo(() => fileProvider.Path).Returns(Path.GetFullPath(FileName));
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
            var words = @"привет
привет";
            var buffer = ParserHelper.Encodings[encoding].GetBytes(words);
            fileStream.Write(buffer,0,buffer.Length);
            fileStream.Dispose();
            settings.Encoding = encoding;
            settings.TextType = TextType.OneWordOneLine;
            var parser = new TxtParser(settings, fileProvider);

            var result = parser.WordParse().ToArray();

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
            var buffer = ParserHelper.Encodings[encoding].GetBytes(words);
            fileStream.Write(buffer, 0, buffer.Length);
            fileStream.Dispose();
            settings.Encoding = encoding;
            settings.TextType = TextType.LiteraryText;
            var parser = new TxtParser(settings, fileProvider);

            var result = parser.WordParse().ToArray();

            result.Should().BeEquivalentTo(expected);
        }



    }
}
