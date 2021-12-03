using FluentAssertions;
using NUnit.Framework;
using System.IO;
using TagsCloudContainer;
using TagsCloudContainer.Interfaces;

namespace TagsCloudContainerTests
{
    internal class TxtParserTests
    {
        private string textsFolder;
        private IParser parser;

        [OneTimeSetUp]
        public void SetUp()
        {
            textsFolder = Path.GetFullPath(@"..\..\..\texts");
            parser = new TxtParser();
        }

        [Test]
        public void Should_ParseCorrectly()
        {
            var path = Path.Combine(textsFolder, "parserTest.txt");

            var result = parser.Parse(path);
            var expected = new [] { "this", "Is", " parser", "test " };

            result.Should().BeEquivalentTo(expected);
        }
    }
}
