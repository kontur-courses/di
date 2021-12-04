using System;
using System.Drawing;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using TagsCloudVisualization.CloudLayouter;
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
            _transformer = new LayoutWordsTransformer(new MockedLayouter());
        }

        [Test]
        public void Transform_ShouldReturnEmptyTags_WhenGetEmptyCollection()
        {
            var words = Array.Empty<WordCount>();

            var tags = _transformer.Transform(words);

            tags.Should().BeEmpty();
        }

        [Test]
        public void Transform_ShouldReturnTagsWithSizesDependentOnCount()
        {
            var words = new[]
            {
                new WordCount("small", 1),
                new WordCount("medium", 2),
                new WordCount("big", 3)
            };

            var tags = _transformer.Transform(words).ToArray();

            tags.Should().HaveCount(3);
            tags
                .Select(x => SquareCalculator.CalculateRectangleSquare(x.Rectangle.Size))
                .Should()
                .BeInAscendingOrder()
                .And
                .OnlyHaveUniqueItems();
        }

        private class MockedLayouter : ILayouter
        {
            public Rectangle PutNextRectangle(Size rectangleSize) => new(Point.Empty, rectangleSize);
        }
    }
}