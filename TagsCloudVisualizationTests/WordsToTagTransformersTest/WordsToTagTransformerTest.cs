using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using iTextSharp.text;
using NUnit.Framework;
using TagsCloudVisualization;
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

            var expected = new List<Tag> { new(3, "three"), new(2, "two"), new(1, "one") };
            tags.Should().BeEquivalentTo(expected);
        }
    }
}