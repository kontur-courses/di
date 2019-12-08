using System.Collections.Generic;
using FluentAssertions;
using NUnit.Framework;
using TagCloud.TextFilter;
using TagCloud.TextProvider;

namespace TagCloud_Should
{
    [TestFixture]
    public class TextFilter_Should
    {
        private readonly ITextProvider textProvider = new UnitTestsTextProvider();

        [Test]
        public void WithEmptyBlacklist_ShouldReturnSameWords()
        {
            var blacklistSettings = new BlacklistSettings
            {
                FilesWithBannedWords = new HashSet<string>()
            };
            var blacklistMaker = new BlacklistMaker(blacklistSettings, textProvider)
            {
                WordMinLength = 0
            };
            ;
            var textFilter = new TextFilter(textProvider, blacklistMaker);
            textFilter.FilterWords().Should().BeEquivalentTo(textProvider.GetAllWords());
        }

        [Test]
        public void ShouldRemoveBannedWords()
        {
            var blacklistSettings = new BlacklistSettings
            {
                FilesWithBannedWords = new HashSet<string>()
            };
            var blacklistMaker = new BlacklistMaker(blacklistSettings, textProvider)
            {
                BlackList = {"blacklistWord"}
            };
            var textFilter = new TextFilter(textProvider, blacklistMaker);
            textFilter.FilterWords().Contains("blacklistWord").Should().BeFalse();
        }

        [Test]
        public void ShouldRemoveSmallWords()
        {
            var blacklistSettings = new BlacklistSettings
            {
                FilesWithBannedWords = new HashSet<string>()
            };
            var blacklistMaker = new BlacklistMaker(blacklistSettings, textProvider);
            var textFilter = new TextFilter(textProvider, blacklistMaker);
            textFilter.FilterWords().Contains("b").Should().BeFalse();
        }

        [Test]
        public void ShouldNotRemoveNotBannedWords()
        {
            var blacklistSettings = new BlacklistSettings
            {
                FilesWithBannedWords = new HashSet<string>()
            };
            var blacklistMaker = new BlacklistMaker(blacklistSettings, textProvider)
            {
                BlackList = {"blacklistWord", "word1", "word2"}
            };
            var textFilter = new TextFilter(textProvider, blacklistMaker);
            textFilter.FilterWords().Should().Contain("word3");
        }
    }
}