using System.Drawing;
using System.IO;
using FluentAssertions;
using NUnit.Framework;

namespace TagCloud
{
    public class TagCloudVisualizationTests
    {
        [Test]
        public void SaveAsBitmap_TagCloudInFile_Success()
        {
            var cloudLayouter = new CircularCloudLayouter(new Point(50, -50));
            var tempBmpFile = "temp.bmp";

            File.Delete(tempBmpFile);

            File.Exists(tempBmpFile).Should().BeFalse($"file {tempBmpFile} must be deleted");

            for (int i = 400; i > 1; i -= 2)
                cloudLayouter.PutNextRectangle(new Size(i, i / 2));
            TagCloudVisualization.SaveAsBitmap(cloudLayouter.GetTagCloud(), tempBmpFile);

            File.Exists(tempBmpFile).Should().BeTrue($"file {tempBmpFile} must be generated");
        }
    }
}
