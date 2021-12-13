using FluentAssertions;
using NUnit.Framework;
using System;
using System.Drawing.Imaging;
using TagsCloudVisualizationDI.Saving;

namespace TagsCloudVisualizationDITests
{
    [TestFixture]
    public class SaverTests
    {
        [Test]
        public void ShouldGetCorrectSaveWay()
        {
            var savePath = "C:\\savePath\testfile";
            var saver = new DefaultSaver(savePath, ImageFormat.Png);
            var expectedAnswer = "C:\\savePath\testfile.Png";
            saver.GetSavePath().Should().Be(expectedAnswer);
        }

        [Test]
        public void ShouldNotThrowExcWhenGetWay()
        {
            var savePath = "C:\\savePath\testfile";
            var saver = new DefaultSaver(savePath, ImageFormat.Png);
            Action act = () => saver.GetSavePath();
            act.Should().NotThrow();
        }
    }
}
