using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;

namespace TagCloud
{
    internal class TagCloudStatsGenerator : ITagCloudStatsGenerator
    {
        

        public List<WordInfo> GenerateStats(IEnumerable<string> words)
        {
            return words.GroupBy(w => w, StringComparer.InvariantCulture)
                        .Select(g => new WordInfo(g.Key, g.Count())).ToList();
        }
    }

    [TestFixture]
    public class TagCloudStatsGenerator_Should
    {
        private ITagCloudStatsGenerator generator;

        [SetUp]
        public void SetUp()
        {
            generator = new TagCloudStatsGenerator();
        }

        [Test]
        public void ReturnEmptyList_WhenNoWordsGiven()
        {
            generator.GenerateStats(Array.Empty<string>())
                     .Should()
                     .BeEmpty();
        }
    }
}
