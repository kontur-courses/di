using FluentAssertions;
using NUnit.Framework;
using System;
using System.IO;
using System.Linq;
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
        public void OneTimeSetUp()
        {
            textsFolder = Path.GetFullPath(@"..\..\..\texts");
            parser = new TxtParser();            
        }

        [SetUp]
        public void SetUp()
        {
            composer = new TagComposer(SettingsProvider.GetSettings());
        }

        [Test]
        public void Should_Throw_OnNoTags()
        {
            var path = Path.Combine(textsFolder, "testNoTags.txt");
            var parsed = parser.Parse(path);

            Assert.Throws<ArgumentException>(() => composer.ComposeTags(parsed).ToArray());
        }

        [Test]
        public void Should_ComposeTagsCorrectly()
        {
            var path = Path.Combine(textsFolder, "test.txt");
            var parsed = parser.Parse(path);

            var result = composer.ComposeTags(parsed);
            var expected = new [] { 
                new Tag(0.375, "music", WordType.Default),
                new Tag(0.250, "guitar", WordType.Default),
                new Tag(0.125, "piano", WordType.Default),
                new Tag(0.125, "string", WordType.Default),
                new Tag(0.125, "banjo", WordType.Default)
            };

            result.Should().BeEquivalentTo(expected);
        }
    }
}
