using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using TagCloud.TextFilter;
using TagCloud.TextProvider;

namespace TagCloud_Should
{
    public class FrequencyDictionaryMaker_Should
    {
        private readonly ITextProvider textProvider = new UnitTestsTextProvider();
        private FrequencyDictionaryMaker frequencyDictionaryMaker;

        [SetUp]
        public void SetUp()
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
            frequencyDictionaryMaker = new FrequencyDictionaryMaker(textFilter);
        }

        [Test]
        public void ShouldNotBeNull()
        {
            frequencyDictionaryMaker
                .MakeFrequencyDictionary()
                .Should().NotBeNull();
        }

        [Test]
        public void ShouldNotContainZeroFrequency()
        {
            frequencyDictionaryMaker
                .MakeFrequencyDictionary()
                .Select(w => w.Value)
                .Should().NotContain(0);
        }
    }
}