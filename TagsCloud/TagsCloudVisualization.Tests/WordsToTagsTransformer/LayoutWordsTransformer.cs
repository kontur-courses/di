using System;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using TagsCloudVisualization.WordsToTagsTransformers;

namespace TagsCloudVisualization.Tests.WordsToTagsTransformer
{
    [TestFixture]
    public class LayoutWordsTransformerTests
    {
        private LayoutWordsTransformer _transformer;

        [SetUp]
        public void Setup()
        {
            _transformer = new LayoutWordsTransformer();
        }

        [Test]
        public void Transform_ShouldReturnEmptyTags_WhenGetEmptyCollection()
        {
            var words = Array.Empty<string>();

            var tags = _transformer.Transform(words);

            tags.Should().BeEmpty();
        }

        [Test]
        public void Transform_ShouldReturnTagsWithSizesDependentOnCount()
        {
            var words = new[]
            {
                "small",
                "medium",
                "medium",
                "big",
                "big",
                "big"
            };

            var tags = _transformer.Transform(words).ToArray();

            tags.Should().HaveCount(3);
            tags
                .Select(x => x.Weight)
                .Should()
                .BeInAscendingOrder()
                .And
                .OnlyHaveUniqueItems();
        }
    }
}