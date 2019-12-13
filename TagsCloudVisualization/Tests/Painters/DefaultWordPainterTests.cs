using System.Collections.Generic;
using System.Drawing;
using FluentAssertions;
using NUnit.Framework;
using TagsCloudVisualization.Core;
using TagsCloudVisualization.Layouters;
using TagsCloudVisualization.Painters;
using TagsCloudVisualization.WordStatistics;

namespace TagsCloudVisualization.Tests.Painters
{
    [TestFixture]
    public class WordPainterTests
    {
        private readonly WordPainter wordPainter = new DefaultWordPainter(new Palette());

        [Test]
        public void GetPaintedWords_PaintsWords()
        {
            var layoutedWords = new LayoutedWord[3];
            layoutedWords[0] = new LayoutedWord("test", new Rectangle(0, 0, 10, 10));
            layoutedWords[1] = new LayoutedWord("test1", new Rectangle(17, 37, 10, 10));
            layoutedWords[2] = new LayoutedWord("test2", new Rectangle(48, 56, 10, 10));
            var statDict = new Dictionary<WordStatistics.WordStatistics, int>
            {
                [new WordStatistics.WordStatistics(layoutedWords[0], StatisticsType.WordCount)] = 2,
                [new WordStatistics.WordStatistics(layoutedWords[0], StatisticsType.WordCount)] = 4,
                [new WordStatistics.WordStatistics(layoutedWords[0], StatisticsType.WordCount)] = 8
            };
            var text = new AnalyzedLayoutedText(layoutedWords, statDict);

            foreach (var paintedWord in wordPainter.GetPaintedWords(text))
                paintedWord.FontColor.Should().NotBe(default(Color));
        }
    }
}