using FluentAssertions;
using NUnit.Framework;
using System;
using System.IO;
using System.Linq;
using TagsCloudContainer;
using TagsCloudContainer.Interfaces;

namespace TagsCloudContainerTests
{
    internal class DocParserTests
    {
        private string textsFolder;
        private IParser parser;

        [OneTimeSetUp]
        public void SetUp()
        {
            textsFolder = Path.GetFullPath(@"..\..\..\texts");
            parser = new DocParser();
        }

        [Test]
        public void Should_Throw_OnNonExistingFile()
        {
            var path = Path.Combine(textsFolder, "amogus.docx");

            Assert.Throws<ArgumentException>(() => parser.Parse(path).ToArray());
        }

        [Test]
        public void Should_ParseCorrectly()
        {
            var path = Path.Combine(textsFolder, "parserTest.docx");

            var result = parser.Parse(path);
            var expected = new [] { "this", "Is", "parser", "test" };

            result.Should().BeEquivalentTo(expected);
        }
    }
}
