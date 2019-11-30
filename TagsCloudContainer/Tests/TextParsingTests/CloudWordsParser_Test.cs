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
        private ICloudWordParsingRule rule;
        private string path;
        [SetUp]
        public void SetUp()
        {
            parser = new CloudWordsParser();
            rule = new DefaultParsingRule();
        }

        [TearDown]
        public void TearDown()
        {
            if(File.Exists(path))
                File.Delete(path);
        }


        [Test]
        public void ParseFrom_Should_ParseFromTxt()
        {
            path = Path.Combine(Directory.GetCurrentDirectory(), "test.txt");
            using (var writer = new StreamWriter(path))
            {
                writer.WriteLine("i");
                writer.WriteLine("sandwich");
                writer.WriteLine("apple");
                writer.WriteLine("you");
                writer.WriteLine("apple");
            }
            var txtParser = new TxtWordParser();
            var result = parser.ParseFrom(txtParser, path, rule);
            result.Select(w => w.Word).Should().Contain("sandwich");
        }
        
        [Test]
        public void ParseFrom_Should_CountWordsRight()
        {
            path = Path.Combine(Directory.GetCurrentDirectory(), "test.txt");
            using (var writer = new StreamWriter(path))
            {
                writer.WriteLine("i");
                writer.WriteLine("sandwich");
                writer.WriteLine("apple");
                writer.WriteLine("you");
                writer.WriteLine("apple");
            }
            var txtParser = new TxtWordParser();
            var result = parser.ParseFrom(txtParser, path, rule);
            result.First(w => w.Word == "apple").Count.Should().Be(2);
        }
        
        [Test]
        public void ParseFrom_Should_ParseToLowercase_When_DefaultRule()
        {
            path = Path.Combine(Directory.GetCurrentDirectory(), "test.txt");
            using (var writer = new StreamWriter(path))
            {
                writer.WriteLine("Apple");
                writer.WriteLine("aPPLe");
                writer.WriteLine("apple");
            }
            var txtParser = new TxtWordParser();
            var result = parser.ParseFrom(txtParser, path, rule);
            result.Count().Should().Be(1);
        }
        
        [Test]
        public void ParseFrom_Should_IgnoreExceptedWords_When_DefaultRule()
        {
            path = Path.Combine(Directory.GetCurrentDirectory(), "test.txt");
            using (var writer = new StreamWriter(path))
            {
                writer.WriteLine("i");
                writer.WriteLine("sandwich");
                writer.WriteLine("apple");
                writer.WriteLine("you");
                writer.WriteLine("apple");
            }
            var txtParser = new TxtWordParser();
            var result = parser.ParseFrom(txtParser, path, rule);
            result.Count().Should().Be(2);
        }

    }
}