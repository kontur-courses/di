using System.IO;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using TagsCloudContainer.TextParsing.CloudParsing;
using TagsCloudContainer.TextParsing.CloudParsing.ParsingRules;
using TagsCloudContainer.TextParsing.FileWordsParsers;

namespace TagsCloudContainer.Tests.TextParsingTests
{
    [TestFixture]
    public class CloudWordsParser_Test
    {
        private CloudWordsParser parser;
        private CloudWordsParserSettings settings;

        [SetUp]
        public void SetUp()
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), "test.txt");
            var txtParser = new TxtWordParser();
            var rule = new DefaultParsingRule();
            settings = new CloudWordsParserSettings {FileWordsParser = txtParser, Rule = rule, Path = path};
            parser = new CloudWordsParser(() => settings);
        }

        [TearDown]
        public void TearDown()
        {
            if (File.Exists(settings.Path))
                File.Delete(settings.Path);
        }


        [Test]
        public void ParseFrom_Should_ParseFromTxt()
        {
            using (var writer = new StreamWriter(settings.Path))
            {
                writer.WriteLine("i");
                writer.WriteLine("sandwich");
                writer.WriteLine("apple");
                writer.WriteLine("you");
                writer.WriteLine("apple");
            }

            var result = parser.Parse();
            result.Select(w => w.Word).Should().Contain("sandwich");
        }

        [Test]
        public void ParseFrom_Should_CountWordsRight()
        {
            using (var writer = new StreamWriter(settings.Path))
            {
                writer.WriteLine("i");
                writer.WriteLine("sandwich");
                writer.WriteLine("apple");
                writer.WriteLine("you");
                writer.WriteLine("apple");
            }

            var result = parser.Parse();
            result.First(w => w.Word == "apple").Count.Should().Be(2);
        }

        [Test]
        public void ParseFrom_Should_ParseToLowercase_When_DefaultRule()
        {
            using (var writer = new StreamWriter(settings.Path))
            {
                writer.WriteLine("Apple");
                writer.WriteLine("aPPLe");
                writer.WriteLine("apple");
            }

            var result = parser.Parse();
            result.Count().Should().Be(1);
        }

        [Test]
        public void ParseFrom_Should_IgnoreExceptedWords_When_DefaultRule()
        {
            using (var writer = new StreamWriter(settings.Path))
            {
                writer.WriteLine("i");
                writer.WriteLine("sandwich");
                writer.WriteLine("apple");
                writer.WriteLine("you");
                writer.WriteLine("apple");
            }

            var result = parser.Parse();
            result.Count().Should().Be(2);
        }
    }
}