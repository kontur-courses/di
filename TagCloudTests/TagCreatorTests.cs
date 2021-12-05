using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using TagCloud.Creators;

namespace TagCloudTests
{
    public class TagCreatorTests
    {
        private ITagCreator tagCreator;

        [SetUp]
        public void SetUp()
        {
            tagCreator = new TagCreator(new Font("Arial", 8));
        }

        [Test]
        public void Create_ShouldCreateTags()
        {
            var words = new Dictionary<string, int>{{"a", 5}, {"b", 3}, {"c", 1}};

            var tags = tagCreator.Create(words);

            var expectedSize = new Size(57, 61);
            var tag = tags.First(t => t.Size == expectedSize);
            tag.Size.Should().Be(expectedSize);
            tag.Frequency.Should().Be(5);
        }

        [Test]
        public void Create_ShouldReturnsNoTags_WhenNoWords()
        {
            var words = new Dictionary<string, int>();

            var tags = tagCreator.Create(words);

            tags.Count().Should().Be(0);
        }
    }
}
