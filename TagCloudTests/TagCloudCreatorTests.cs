using System.Drawing;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using TagCloud.CloudLayouters;
using TagCloud.PointGenerators;
using TagCloud.Tags;
using TagCloud.TagCloudCreators;
using TagCloud.TagCloudVisualizations;

namespace TagCloudTests
{
    public class TagCloudCreatorTests
    {
        [TestCase(0,0)]
        [TestCase(-5, 10)]
        public void Create_MustCreateTagCloudByCircularCloudLayouter_Success(int centerX, int centerY)
        {
            var center = new Point(centerX, centerY);
            var rectangleSizes = Enumerable.Range(2, 40).
                Select(width => new Size(width, width / 2)).Reverse().ToArray();

            var tagCloud = new TagCloud.TagCloud(center);
            var circularCloudLayouter = new CircularCloudLayouter(() => new SpiralPointGenerator(center));
            foreach (var rectangleSize in rectangleSizes)
                tagCloud.Layouts.Add(new Layout(circularCloudLayouter.PutNextRectangle(rectangleSize)));

            var cloudLayouter = new CircularCloudLayouter(() => new SpiralPointGenerator(center));
            var layoutTagCloudCreator = new LayoutTagCloudCreator(cloudLayouter, rectangleSizes);
            var settings = new TagCloudVisualizationSettings();
            
            tagCloud.Should().Be(layoutTagCloudCreator.GenerateTagCloud(settings));
        }
    }
}
