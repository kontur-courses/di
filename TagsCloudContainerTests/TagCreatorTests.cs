using FluentAssertions;
using NUnit.Framework;
using System;
using TagsCloudContainer;
using TagsCloudContainer.Infrastructure;
using TagsCloudContainer.Interfaces;

namespace TagsCloudContainerTests
{
    internal class TagCreatorTests
    {
        private string[] words;
        private ITagCreator tagCreator;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            words = new[] { "music", "guitar", "string", "music" };
        }

        [SetUp]
        public void SetUp()
        {
            tagCreator = new TagCreator();
        }

        [Test]
        public void Should_Throw_OnNoTags()
        {
            Assert.Throws<ArgumentException>(() => tagCreator.CreateTags(new string[] {}));
        }

        [Test]
        public void Should_ComposeTagsCorrectly()
        {
            var result = tagCreator.CreateTags(words);
            var expected = new [] { 
                new Tag(0.5, "music", WordType.Default),
                new Tag(0.25, "guitar", WordType.Default),
                new Tag(0.25, "string", WordType.Default)
            };

            result.Should().BeEquivalentTo(expected);
        }
    }
}
