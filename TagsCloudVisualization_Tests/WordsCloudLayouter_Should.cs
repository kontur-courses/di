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
            var spiral = new Spiral(0.0005, 0);
            var layouter = new CircularCloudLayouter(new LayouterSettings(center, spiral));
            wordsCloudLayouter = new WordsCloudLayouter(layouter, new FontSettings(new FontFamily("Times New Roman"), FontStyle.Bold));
        }

        [Test]
        public void Radius_BeZeroAfterCreation() => wordsCloudLayouter.Radius.Should().Be(0);


        [Test]
        public void LayWords_AndReturnWordEntity()
        {
            var frequencyWords = new List<(string, int)>{("mama",5), ("papa", 8)};
            var words = wordsCloudLayouter.LayWords(frequencyWords);
            words.ToList().Should().HaveCount(2);
        }
    }
}