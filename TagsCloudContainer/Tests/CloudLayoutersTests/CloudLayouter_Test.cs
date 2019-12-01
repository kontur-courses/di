using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using TagsCloudContainer.CloudLayouters;
using TagsCloudContainer.CloudLayouters.CircularCloudLayouter;
using TagsCloudContainer.TextParsing.CloudParsing;

namespace TagsCloudContainer.Tests.CloudLayoutersTests
{
    [TestFixture]
    public class CloudLayouter_Test
    {
        private CloudLayouterSettings settings;
        private CloudLayouter layouter;
        [SetUp]
        public void SetUp()
        {
            settings = new CloudLayouterSettings();
            settings.Center = new Point(0, 0);
            settings.Algorithm = new CircularCloudLayouter(settings.Center);
            settings.RectangleSquareMultiplier = 100;
            layouter = new CloudLayouter(settings);
        }

        [Test]
        public void GetWords_Should_ReturnDescendingOrderedCollection()
        {
            var firstWord = new CloudWord("apple");
            firstWord.AddCount();
            firstWord.AddCount();
            var secondWord = new CloudWord("cider");
            secondWord.AddCount();
            var words = new List<CloudWord> {secondWord, firstWord};
            var visualizationWords = layouter.GetWords(words);
            visualizationWords.First().Word.Should().Be("apple");
        }
        
        [Test]
        public void GetWords_Should_ReturnWordsWithDescendingSizes()
        {
            var firstWord = new CloudWord("apple");
            firstWord.AddCount();
            firstWord.AddCount();
            var secondWord = new CloudWord("cider");
            secondWord.AddCount();
            var words = new List<CloudWord> {secondWord, firstWord};
            var visualizationWords = layouter.GetWords(words);
            var firstSize = visualizationWords.First().Rectangle.Size;
            var secondSize = visualizationWords.Last().Rectangle.Size;
            var firstSquare = firstSize.Height * firstSize.Width;
            var secondSquare = secondSize.Height * secondSize.Width;
            firstSquare.Should().BeGreaterThan(secondSquare);
        }
    }
}