using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using TagsCloudVisualization.Common.Settings;
using TagsCloudVisualization.Common.Tags;
using TagsCloudVisualization.Common.TextAnalyzers;

namespace TagsCloudVisualization.Tests
{
    [TestFixture]
    public class TagBuilderTests
    {
        private TagBuilder tagBuilder;
        private TagStyleSettings tagStyleSettings;
        private List<WordStatistic> wordStatistics;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            tagStyleSettings = new TagStyleSettings(
                new[] {Color.Red, Color.Green, Color.Blue},
                new[] {"Arial", "Cambria", "Comic Sans MS"}, 
                25,
                10);
            
            tagBuilder = new TagBuilder(tagStyleSettings);
            
            wordStatistics = new List<WordStatistic>
            {
                new WordStatistic("Сталин", 1),
                new WordStatistic("Черчилль", 2),
                new WordStatistic("Рузвельт", 3),
                new WordStatistic("Гитлер", 4)
            };
        }
        
        [Test]
        public void GetTags_ShouldReturnAllTags()
        {
            var actualTags = tagBuilder.GetTags(wordStatistics).ToList();

            actualTags.Count().Should().Be(wordStatistics.Count());
        }

        [Test]
        public void GetTags_ShouldCorrectlyStyleTags()
        {
            var actualTags = tagBuilder.GetTags(wordStatistics).ToList();
            var expectedColors = tagStyleSettings.ForegroundColors.Count();
            var expectedFontFamilies = tagStyleSettings.FontFamilies.Count();
            var expectedFontSizes = wordStatistics.Select(stat => stat.Count).Distinct().Count();

            var actualColors = actualTags.Select(tag => tag.Style.ForegroundColor).Distinct().Count();
            var actualFontFamilies = actualTags.Select(tag => tag.Style.Font.FontFamily).Distinct().Count();
            var actualFontSizes = actualTags.Select(tag => tag.Style.Font.Size).Distinct().Count();
            
            actualColors.Should().Be(expectedColors);
            actualFontFamilies.Should().Be(expectedFontFamilies);
            actualFontSizes.Should().Be(expectedFontSizes);
        }
    }
}