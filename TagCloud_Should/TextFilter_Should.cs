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
        public void ShouldRemoveBannedWords()
        {
            var blacklistSettings = new BlacklistSettings
            {
                FilesWithBannedWords = new HashSet<string>()
            };
            var textParser = new TextParser(textProvider);
            var blacklistMaker = new BlacklistMaker(blacklistSettings, textParser)
            {
                BlackList = {"blacklistWord"}
            };
            var textFilter = new TextFilter(textParser, blacklistMaker);
            textFilter.FilterWords().Contains("blacklistWord").Should().BeFalse();
        }

        [Test]
        public void ShouldRemoveSmallWords()
        {
            var blacklistSettings = new BlacklistSettings
            {
                FilesWithBannedWords = new HashSet<string>()
            };
            var textParser = new TextParser(textProvider);
            var blacklistMaker = new BlacklistMaker(blacklistSettings, textParser);
            var textFilter = new TextFilter(textParser, blacklistMaker);
            textFilter.FilterWords().Contains("b").Should().BeFalse();
        }

        [Test]
        public void ShouldNotRemoveNotBannedWords()
        {
            var blacklistSettings = new BlacklistSettings
            {
                FilesWithBannedWords = new HashSet<string>()
            };
            var textParser = new TextParser(textProvider);
            var blacklistMaker = new BlacklistMaker(blacklistSettings, textParser)
            {
                BlackList = {"blacklistWord", "word1", "word2"}
            };
            var textFilter = new TextFilter(textParser, blacklistMaker);
            textFilter.FilterWords().Should().Contain("word3");
        }
    }
}