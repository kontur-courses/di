using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using TagsCloudVisualization.WordsToTagTransformers;

namespace TagsCloudVisualizationTests.WordsToTagTransformersTest
{
    public class WordsToTagTransformerTest
    {
        [Test]
        public void Transform_ShouldReturnTagsWithSizesDependentOnCount()
        {
            var words = new[]
            {
                "one",
                "two",
                "two",
                "three",
                "three",
                "three"
            };
            var transformer = new WordsToTagTransformer();
            var tags = transformer.Transform(words).ToList();

            tags.Should().HaveCount(3);
            tags
                .Select(x => x.Count)
                .Should()
                .BeInAscendingOrder()
                .And
                .OnlyHaveUniqueItems();
        }
    }
}