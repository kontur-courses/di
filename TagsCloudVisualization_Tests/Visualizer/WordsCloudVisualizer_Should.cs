using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using NSubstitute;
using NUnit.Framework;
using TagsCloudVisualization.Infrastructure;
using TagsCloudVisualization.Layouter;
using TagsCloudVisualization.Visualizer;

namespace TagsCloudVisualization_Tests.Visualizer
{
    [TestFixture]
    public class WordsCloudVisualizer_Should
    {
        private WordsCloudVisualizer visualizer;
        private IWordsCloudBuilder cloudBuilder;
        private Palette palette;
        private Size pictureSize;
        [SetUp]
        public void SetUp()
        {
            cloudBuilder = Substitute.For<IWordsCloudBuilder>();
            palette = new Palette(Color.DimGray, Brushes.AliceBlue);
            pictureSize = new Size(800, 800);
            visualizer = new WordsCloudVisualizer(cloudBuilder, palette, pictureSize);
        }

        [Test]
        public void Draw_WithGivenSize()
        {
            visualizer.Draw().Size.Should().Be(pictureSize);
        }
    }
}
