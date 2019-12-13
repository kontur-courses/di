using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using TagsCloudVisualization.Core;
using TagsCloudVisualization.Layouters;
using TagsCloudVisualization.Layouters.CloudLayouters;
using TagsCloudVisualization.WordStatistics;

namespace TagsCloudVisualization.Tests
{
    [TestFixture]
    public class WordCountSizeChooserTests
    {
        [SetUp]
        public void Setup()
        {
            cloudLayouter = new CircularCloudLayouter(new Point(0, 0));
            sizeChooser = new WordCountSizeChooser();
            var stat = new Dictionary<WordStatistics.WordStatistics, int>();
            var counter = 1;
            foreach (var word in words)
                stat[new WordStatistics.WordStatistics(word, StatisticsType.WordCount)] = counter++;
            analyzedText = new AnalyzedText(words, stat);
        }

        private ICloudLayouter cloudLayouter;
        private IWordSizeChooser sizeChooser;
        private AnalyzedText analyzedText;

        private readonly Word[] words =
        {
            new Word("test"),
            new Word("Test2"),
            new Word("test3"),
            new Word("test4")
        };

        [Test]
        public void GetLayoutedWords_ReturnDifferentSizes_OnDifferentWordFrequency()
        {
            var wordLayouter = new WordLayouter(new CloudLayouterConfiguration(() => cloudLayouter), sizeChooser);
            var previousHeight = -1;
            foreach (var layoutedWord in wordLayouter.GetLayoutedText(analyzedText).Words
                .OrderBy(x => x.Position.Height))
            {
                layoutedWord.Position.Height.Should().BeGreaterThan(previousHeight);
                previousHeight = layoutedWord.Position.Height;
            }
        }
    }
}