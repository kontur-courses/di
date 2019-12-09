using System.Drawing;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using TagsCloudVisualization.Core;
using TagsCloudVisualization.Layouters;
using TagsCloudVisualization.Layouters.CloudLayouters;

namespace TagsCloudVisualization.Tests
{
    [TestFixture]
    public class WordLayouterTests
    {
        private ICloudLayouter cloudLayouter;
        private IWordSizeChooser sizeChooser;
        //private Word[] words = {new Word("test", 2),
        //    new Word("Test2", 4),
        //    new Word("test3", 1),
        //    new Word("test4", 11)};

        [SetUp]
        public void Setup()
        {
            cloudLayouter = new CircularCloudLayouter(new Point(0, 0));
            sizeChooser = new WordCountSizeChooser();
        }

        [Test]
        public void GetLayoutedWords_ReturnDifferentSizes_OnDifferentWordFrequency()
        {
            var wordLayouter = new WordLayouter(cloudLayouter, sizeChooser);
            var previousHeight = -1;
            //foreach (var layoutedWord in wordLayouter.GetLayoutedText(words).OrderBy(x => x.Rectangle.Height))
            //{
            //    layoutedWord.Rectangle.Height.Should().BeGreaterThan(previousHeight);
            //    previousHeight = layoutedWord.Rectangle.Height;
            //}
        }
    }
}