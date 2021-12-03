using FluentAssertions;
using NUnit.Framework;
using System.IO;
using TagsCloudContainer;
using TagsCloudContainer.Infrastructure;
using TagsCloudContainer.Interfaces;

namespace TagsCloudContainerTests
{
    internal class TagComposerTests
    {
        private string textsFolder;
        private IParser parser;
        private ITagComposer composer;

        [OneTimeSetUp]
        public void SetUp()
        {
            textsFolder = Path.GetFullPath(@"..\..\..\texts");
            parser = new TxtParser();
            composer = new TagComposer();
        }

        [Test]
        public void Should_ComposeTagsCorrectly()
        {
            var path = Path.Combine(textsFolder, "test.txt");
            var parsed = parser.Parse(path);

            var result = composer.ComposeTags(parsed);
            var expected = new [] { 
                new Tag(0.375, "music"),
                new Tag(0.250, "guitar"),
                new Tag(0.125, "piano"),
                new Tag(0.125, "string"),
                new Tag(0.125, "banjo")
            };

            result.Should().BeEquivalentTo(expected);
        }
    }
}
