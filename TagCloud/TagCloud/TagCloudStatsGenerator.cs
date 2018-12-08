using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;

namespace TagCloud
{
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
