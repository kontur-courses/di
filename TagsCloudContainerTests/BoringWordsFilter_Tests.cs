using System.IO;
using FluentAssertions;
using NUnit.Framework;
using TagsCloudContainer.Settings;
using TagsCloudContainer.WordFilters;

namespace TagsCloudContainerTests
{
    public class BoringWordsFilter_Tests
    {
        private BoringWordsFilter filter;
        
        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            const string boringWordsPath = "BoringWords.txt";
            var settings = new AppSettings()
            {
                BoringWordsPath = boringWordsPath
            };

            filter = new BoringWordsFilter(settings);
            var boringWords = new[] { "they", "we", "are" };
            File.WriteAllText(boringWordsPath, string.Join("\n", boringWords));
        }

        [Test]
        public void Filter_FiltersWordsCorrectly()
        {
            var words = new[] { "they", "me", "he", "she", "was", "are", "we" };
            var expectedFilteredWords = new[] { "me", "he", "she", "was" };

            var filteredWords = filter.Filter(words);

            filteredWords.Should().BeEquivalentTo(expectedFilteredWords);
        }
    }
}