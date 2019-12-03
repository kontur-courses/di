using System.Collections.Generic;
using FluentAssertions;
using NUnit.Framework;
using TagCloud.TextFilter;

namespace TagCloud_Should
{
    [TestFixture]
    public class TextFilter_Should
    {
        [Test]
        public void TestFilter_WithEmptyBlacklist_ShouldReturnSameWords()
        {
            var textFilter = new TextFilter(new BlacklistMaker(new TextFilterSettings(), new BlacklistSettings
            {
                FilesWithBannedWords = new HashSet<string>()
            }), new TextFilterSettings());
            var allWords = new Dictionary<string, int>
            {
                {"word1", 5},
                {"word2", 7},
                {"word3", 9},
                {"word4", 4}
            };
            textFilter.FilterWords(allWords).Count.Should().Be(4);
        }

        [Test]
        public void TextFilter_ShouldRemoveBannedWords()
        {
            var textFilter = new TextFilter(new BlacklistMaker(new TextFilterSettings(), new BlacklistSettings
            {
                FilesWithBannedWords = new HashSet<string>()
            })
            {
                BlackList = {"bannedWord1", "bannedWord2"}
            }, new TextFilterSettings());
            var allWords = new Dictionary<string, int>
            {
                {"word1", 5},
                {"bannedWord1", 5},
                {"word2", 7},
                {"bannedWord2", 4},
                {"word3", 9},
                {"word4", 4}
            };
            textFilter.FilterWords(allWords).Count.Should().Be(4);
        }

        [Test]
        public void TextFilter_ShouldRemoveSmallWords()
        {
            var textFilter = new TextFilter(
                new BlacklistMaker(new TextFilterSettings(),
                    new BlacklistSettings {FilesWithBannedWords = new HashSet<string>()}), new TextFilterSettings());
            var allWords = new Dictionary<string, int>
            {
                {"a", 5},
                {"b", 5},
                {"c", 5},
                {"normalWord1", 5},
                {"normalWord2", 5},
                {"ef", 5},
                {"", 5},
            };
            textFilter.FilterWords(allWords).Count.Should().Be(2);
        }
    }
}