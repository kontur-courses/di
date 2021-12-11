using FluentAssertions;
using NUnit.Framework;
using System;
using System.IO;
using System.Linq;
using TagsCloudContainer.Interfaces;

namespace TagsCloudContainerTests
{
    internal abstract class ParserTests
    {
        private string textsFolder = Path.GetFullPath(@"..\..\..\texts");
        protected IParser parser;
        protected string format;

        [Test]
        public void Should_Throw_OnNonExistingFile()
        {
            var path = Path.Combine(textsFolder, $"amogus.{format}");

            Assert.Throws<ArgumentException>(() => parser.Parse(path).ToArray());
        }

        [Test]
        public void Should_ParseCorrectly()
        {
            var path = Path.Combine(textsFolder, $"parserTest.{format}");

            var result = parser.Parse(path);
            var expected = new[] { "this", "Is", " parser", "test " };

            result.Should().BeEquivalentTo(expected);
        }
    }
}
