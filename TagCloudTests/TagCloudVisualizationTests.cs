using System.Drawing;
using System.IO;
using FluentAssertions;
using NUnit.Framework;
using TagCloud.PointGenerators;
using TagCloud.TagCloudVisualizations;

namespace TagCloud
{
    public class TagCloudVisualizationTests
    {
        [Test]
        public void SaveAsBitmap_TagCloudInFile_Success()
        {
            var center = new Point(50, -50);
            var cloudLayouter = new CircularCloudLayouter(new SpiralPointGenerator(center));
            var tempBmpFile = "temp.bmp";

            File.Delete(tempBmpFile);

            File.Exists(tempBmpFile).Should().BeFalse($"file {tempBmpFile} must be deleted");

            for (int i = 400; i > 1; i -= 2)
                cloudLayouter.PutNextRectangle(new Size(i, i / 2));
            var visualization = new TagCloudBitmapVisualization(cloudLayouter.GetTagCloud());
            visualization.Save(tempBmpFile);

            File.Exists(tempBmpFile).Should().BeTrue($"file {tempBmpFile} must be generated");
        }
    }
}
