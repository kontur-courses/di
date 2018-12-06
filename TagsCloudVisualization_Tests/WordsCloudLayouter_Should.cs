using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using TagsCloudVisualization;

namespace TagsCloudVisualization_Tests
{
    [TestFixture]
    public class WordsCloudLayouter_Should
    {
        private WordsCloudLayouter wordsCloudLayouter;

        [SetUp]
        public void SetUp()
        {
            var center = new Point(0, 0);
            var spiral = new Spiral(center, 0.0005, 0);
            var layouter = new CircularCloudLayouter(spiral);
            wordsCloudLayouter = new WordsCloudLayouter(layouter);
        }

        [Test]
        public void LayWords_AndReturnWordEntity()
        {
            var mama = new SizedWord("mama", new Font(FontFamily.GenericSansSerif, 10), new Size(10, 5));
            var ma = new SizedWord("ma", new Font(FontFamily.GenericSansSerif, 5), new Size(5, 5));
            var frequencyWords = new List<SizedWord>{mama, ma};
            var words = wordsCloudLayouter.LayWords(frequencyWords);
            words.ToList().Should().HaveCount(2);
        }
    }
}