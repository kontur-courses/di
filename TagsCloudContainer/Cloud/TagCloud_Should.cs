using System.Drawing;
using FluentAssertions;
using NUnit.Framework;
using TagsCloudContainer.Words;
using TagsCloudVisualization;

namespace TagsCloudContainer.Cloud
{
    [TestFixture]
    public class SuperTightCloudLayouter_Should
    {
        [Test]
        public void Constructor_Should()
        {
            var words = new string[] {"3", "3", "3", "2", "2", "1",};
            var wordPreprocessing = new WordPreprocessing(words);
            var wordAnalizer = new WordAnalizer(wordPreprocessing);
            var cloudLayouter = new SuperTightCloudLayouter(new Point(500, 500));

            var tagCloud = new TagCloud(cloudLayouter, wordAnalizer);

            tagCloud.Tags[0].Word.Should().Be("3");
            tagCloud.Tags[1].Word.Should().Be("2");
            tagCloud.Tags[2].Word.Should().Be("1");
            tagCloud.Tags.Length.Should().Be(3);
        }
    }
}
