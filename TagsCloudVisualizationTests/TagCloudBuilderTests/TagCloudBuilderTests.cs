using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using TagsCloudVisualization;
using TagsCloudVisualization.AppSettings;
using TagsCloudVisualization.Canvases;
using TagsCloudVisualization.FormAction;
using TagsCloudVisualization.PointsGenerators;
using TagsCloudVisualization.TagCloudBuilders;
using TagsCloudVisualization.TagCloudLayouter;
using TagsCloudVisualization.Tags;

namespace TagsCloudVisualizationTests.TagCloudBuilderTests
{
    public class TagCloudBuilderTests
    {
        [SetUp]
        public void SetUp()
        {
            var canvas = new Canvas();
            fontSettings = new FontSettings();
            new MainForm(new IFormAction[0], new ImageSettings {Width = 100, Height = 100}, canvas);
            sut = new TagCloudBuilder(
                new CircularCloudLayouter(
                    new ArchimedesSpiral(new SpiralParams(), canvas)), fontSettings);
        }

        private ITagCloudBuilder sut;
        private FontSettings fontSettings;

        [Test]
        public void Build_ReturnEmptyList_WhenWordsFreqEmpty()
        {
            var wordsFrequency = new Dictionary<string, int>();
            
            var result = sut.Build(wordsFrequency);

            result.Should().Equal(new List<Tag>());
        }
        
        [Test]
        public void Build_ReturnLocatedTag_WhenWordsFreqContain1Pair()
        {
            var wordsFrequency = new Dictionary<string, int>{{"hello", 1}};
            
            var result = sut.Build(wordsFrequency);

            result.Count.Should().Be(wordsFrequency.Count);
        }
        
        [Test]
        public void Build_ResultContainCorrectParameters()
        {
            var wordsFrequency = new Dictionary<string, int>{{"hello", 1}};
            
            var result = sut.Build(wordsFrequency);

            result.First().Font.Should().Be(fontSettings.Font);
            result.First().Text.Should().Be("hello");
        }
    }
}