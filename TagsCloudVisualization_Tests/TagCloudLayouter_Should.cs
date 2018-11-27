using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using TagsCloudVisualization;

namespace TagsCloudVisualization_Tests
{
    class TagCloudLayouter_Should
    {
        private CircularCloudLayouter layout;
        private WordAnalyzer analyzer;

        [SetUp]
        public void SetUp()
        {
            layout = new CircularCloudLayouter();
            analyzer = new WordAnalyzer();

        }

        [Test]
        public void GetTags_GetWeightedWords_ReturnCorrectCountOfTags()
        {
            var text
                = "So I said yes to Thomas Clinton and later thought that I had said yes to God and later still realized I had said yes only to Thomas Clinton";
            var weightedWords = analyzer.WeightWords(analyzer.TextAnalyzer(text));
            var tagLayouter = new TagCloudLayouter(layout, weightedWords);
            tagLayouter.GetTags().Count.Should().Be(weightedWords.Count);
        }

        [Test]
        public void GetTags_GetWeightedWords_ReturnTagsWithDescendingFontSizes()
        {
            var text
                = "So I said yes to Thomas Clinton and later thought that I had said yes to God and later still realized I had said yes only to Thomas Clinton";
            var weightedWords = analyzer.WeightWords(analyzer.TextAnalyzer(text));
            var tagLayouter = new TagCloudLayouter(layout, weightedWords);
            var tags = tagLayouter.GetTags();
            var tagFontsSizes = tags.Select(tag => tag.Font.Size).ToList();
            tagFontsSizes.Should().BeInDescendingOrder();
        }

        [Test]
        public void GetTags_GetWeightedWords_ReturnTagsSortedByWeightedWordsFrequencies()
        {
            var text
                = "So I said yes to Thomas Clinton and later thought that I had said yes to God and later still realized I had said yes only to Thomas Clinton";
            var weightedWords = analyzer.WeightWords(analyzer.TextAnalyzer(text));
            var tagLayouter = new TagCloudLayouter(layout, weightedWords);
            var tags = tagLayouter.GetTags();
            tags.Select(tag => tag.Word).Should().ContainInOrder(weightedWords.Select(word => word.Key));        
        }

        [Test]
        public void GetTags_AddSingleWord_ReturnWordWithMaxSize()
        {
            var weightedWords = new Dictionary<String, int> {{"hello", 3}};
            var tagLayouter = new TagCloudLayouter(layout, weightedWords);
            var tags = tagLayouter.GetTags();
            tags.First().Font.SizeInPoints.Should().Be(50);
        }

        [Test]
        public void GetTags_AddThreeWordsWithDescendingFrequensies_ReturnTagsWithCorrectSizes()
        {
            var weightedWords = new Dictionary<String, int> { { "how", 4 }, {"are", 2}, {"you", 1} };
            var tagLayouter = new TagCloudLayouter(layout, weightedWords);
            var tags = tagLayouter.GetTags();
            var sizes = new[] {40, 20, 10};
            tags.Select(tag => tag.Font.SizeInPoints).ShouldAllBeEquivalentTo(sizes);
        }
    }
}
