using System;
using System.Collections.Generic;
using System.Drawing;
using FluentAssertions;
using NUnit.Framework;
using TagCloud;
using TagsCloudVisualization;

namespace TagCloudTests
{
    [TestFixture]
    public class WordsForCloudGenerator_Should
    {
        private WordsForCloudGenerator wordsForCloudGenerator;
        private const string FontName = "Arial";
        private const int MaxFontSize = 10;
        private static readonly Point Center = new Point(500, 500);
        private Color[] colors = new[] {Color.Black};

        [SetUp]
        public void Setup()
        {
            wordsForCloudGenerator = new WordsForCloudGenerator(FontName,
                                                                MaxFontSize,
                                                                new CircularCloudLayouter(new SpiralPoints(Center)),
                                                                new ColorGenerator(colors));
        }

        [Test]
        public void NotRepeated_OnRepeatedWords()
        {
            wordsForCloudGenerator.Generate(new List<string> {"w", "w", "e"}).Count.Should().Be(2);
        }

        [Test]
        public void MostCommonWord_OnFirstPlace()
        {
            wordsForCloudGenerator.Generate(new List<string> {"w", "w", "e", "e", "e", "c"})[0].Word.Should().Be("e");
        }

        [Test]
        public void FirstWord_OnCenter()
        {
            var wordForCloud = wordsForCloudGenerator.Generate(new List<string> {"w", "w", "e", "e", "e", "c"})[0];
            wordForCloud.WordSize.Location.Should()
                        .Be(new Point(Center.X - wordForCloud.WordSize.Width / 2,
                                      Center.Y - wordForCloud.WordSize.Height / 2));
        }

        [Test]
        public void HaveDescendingOrder()
        {
            var wordsForCloud = wordsForCloudGenerator.Generate(new List<string> {"e", "w", "w"});
            wordsForCloud[0].Font.Size.Should().BeGreaterThan(wordsForCloud[1].Font.Size);
        }
    }
}