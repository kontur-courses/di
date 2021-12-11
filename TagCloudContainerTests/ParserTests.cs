using System.Collections.Generic;
using FluentAssertions;
using NUnit.Framework;
using TagsCloudContainer;
using TagsCloudContainer.Common;

namespace TagCloudContainerTests
{
    [TestFixture]
    public class ParserShould
    {
        private WordsCountParser parser = new WordsCountParser();

        [Test]
        public void ReturnEmptyTags_WhenGetEmptyString()
        {
            parser.Parse("").Should().BeEmpty();
        }

        [Test]
        public void FindAllUniqueTags()
        {
            var expected = new List<SimpleTag>
            {
                new SimpleTag("one", 1),
                new SimpleTag("two", 1),
                new SimpleTag("three", 1)
            };
            var actual = parser.Parse(TagsWriterHelper.GetTagsToString(expected));
            actual.Should().BeEquivalentTo(expected);
        }

        [Test]
        public void FindAllRepeatedTags()
        {
            var expected = new List<SimpleTag>
            {
                new SimpleTag("one", 1),
                new SimpleTag("two", 2),
                new SimpleTag("three", 3)
            };
            var actual = parser.Parse(TagsWriterHelper.GetTagsToString(expected));
            actual.Should().BeEquivalentTo(expected);
        }
    }
}