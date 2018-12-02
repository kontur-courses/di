using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using TagCloud.Settings;
using TagCloud.TagCloudVisualization.Analyzer;
using TagCloud.TagCloudVisualization.Layouter;
using TagsCloudVisualization;

namespace TagsCloudVisualization_Tests
{
    class TagCloudLayouter_Should
    {
        private CircularCloudLayouter layout;
        private WordAnalyzer wordAnalyzer;

        [SetUp]
        public void SetUp()
        {
            layout = new CircularCloudLayouter();
            wordAnalyzer = new WordAnalyzer();

        }

        [Test]
        public void GetTags_GetWeightedWords_ReturnCorrectCountOfTags()
        {
            var text
                = "So I said yes to Thomas Clinton and later thought that I had said yes to God and later still realized I had said yes only to Thomas Clinton";
            var weightedWords = wordAnalyzer.WeightWords(wordAnalyzer.SplitWords(text));
            
            var tagLayouter = new TagCloudLayouter(new FontSettings(), new CircularCloudLayouter());
            
            tagLayouter.GetCloudTags(weightedWords).Count.Should().Be(weightedWords.Count);
        }

        [Test]
        public void GetTags_GetWeightedWords_ReturnTagsWithDescendingFontSizes()
        {
            var text
                = "So I said yes to Thomas Clinton and later thought that I had said yes to God and later still realized I had said yes only to Thomas Clinton";
            var weightedWords = wordAnalyzer.WeightWords(wordAnalyzer.SplitWords(text));
            var tagLayouter = new TagCloudLayouter(new FontSettings(), new CircularCloudLayouter());
            
            var tags = tagLayouter.GetCloudTags(weightedWords);
            
            var tagFontsSizes = tags.Select(tag => tag.Font.Size).ToList();
            tagFontsSizes.Should().BeInDescendingOrder();
        }

        [Test]
        public void GetTags_GetWeightedWords_ReturnTagsSortedByWeightedWordsFrequencies()
        {
            var text
                = "So I said yes to Thomas Clinton and later thought that I had said yes to God and later still realized I had said yes only to Thomas Clinton";
            var weightedWords = wordAnalyzer.WeightWords(wordAnalyzer.SplitWords(text));
            
            var tagLayouter = new TagCloudLayouter(new FontSettings(), new CircularCloudLayouter());
            
            var tags = tagLayouter.GetCloudTags(weightedWords);
            tags.Select(tag => tag.Word).Should().ContainInOrder(weightedWords.Select(word => word.Key));        
        }

        [Test]
        public void GetTags_AddSingleWord_ReturnWordWithMaxSize()
        {
            var weightedWords = new Dictionary<String, int> {{"hello", 3}};
            var tagLayouter = new TagCloudLayouter(new FontSettings(), new CircularCloudLayouter());
            
            var tags = tagLayouter.GetCloudTags(weightedWords);
            
            tags.First().Font.SizeInPoints.Should().Be(50);
        }

        [Test]
        public void GetTags_AddThreeWordsWithDescendingFrequensies_ReturnTagsWithCorrectSizes()
        {
            var weightedWords = new Dictionary<String, int> { { "how", 4 }, {"are", 2}, {"you", 1} };
            var tagLayouter = new TagCloudLayouter(new FontSettings(), new CircularCloudLayouter());
            
            var tags = tagLayouter.GetCloudTags(weightedWords);
            
            var sizes = new[] {63, 37, 23};
            tags.Select(tag => tag.Font.SizeInPoints).ShouldAllBeEquivalentTo(sizes);
        }
    }
}
