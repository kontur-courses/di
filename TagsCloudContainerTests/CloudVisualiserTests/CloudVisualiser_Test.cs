using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using FluentAssertions;
using NUnit.Framework;
using TagsCloudContainer.CloudVisualizers;
using TagsCloudContainer.CloudVisualizers.BitmapMakers;

namespace TagsCloudContainer.Tests.CloudVisualiserTests
{
    [TestFixture]
    public class CloudVisualiser_Test
    {
        private CloudVisualizer visualizer;
        private CloudVisualizerSettings settings;

        [SetUp]
        public void SetUp()
        {
            settings = new CloudVisualizerSettings
            {
                Width = 1280, Height = 720, Palette = new Palette(), BitmapMaker = new DefaultBitmapMaker()
            };
            visualizer = new CloudVisualizer(() => settings);
        }

        [Test]
        public void GetBitmap_Should_ReturnBitmap()
        {
            var firstLocation = new Point(0, 0);
            var firstSize = GetWordRectangleSize("apple", 2);
            var firstWord = new CloudVisualizationWord(new Rectangle(firstLocation, firstSize), "apple");
            var secondLocation = new Point(firstSize.Width, 0);
            var secondSize = GetWordRectangleSize("cider", 1);
            var secondWord = new CloudVisualizationWord(new Rectangle(secondLocation, secondSize), "as");
            var words = new List<CloudVisualizationWord> {secondWord, firstWord};

            visualizer.GetBitmap(words).Should().NotBeNull();
        }

        private Size GetWordRectangleSize(string word, int count)
        {
            var length = word.Length;
            var ratio = count * 100 / length;
            var width = count * 100 - ratio;
            var height = ratio;
            return new Size(width, height);
        }
    }
}