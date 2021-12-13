using FluentAssertions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using TagsCloudVisualizationDI;
using TagsCloudVisualizationDI.Saving;
using TagsCloudVisualizationDI.TextAnalization.Visualization;

namespace TagsCloudVisualizationDITests
{
    [TestFixture]
    public class VisualisationTests
    {
        [Test]
        public void ShouldNotThrowExcOnVisualisation()
        {
            var savePath = Path.GetDirectoryName(typeof(Program).Assembly.Location) + "\\image";
            var saver = new DefaultSaver(savePath, ImageFormat.Png);
            var visualization = new DefaultVisualization(new SolidBrush(Color.AliceBlue), new Font("times", 15),
                new Size(10, 10), 10);
            Action act = () => visualization.DrawAndSaveImage(new List<RectangleWithWord>(), saver.GetSavePath(), ImageFormat.Png);
            act.Should().NotThrow();
        }

        [Test]
        public void ShouldThrowWneInvalidPath()
        {
            var visualization = new DefaultVisualization(new SolidBrush(Color.AliceBlue), new Font("times", 15),
                new Size(10, 10), 10);
            Action act = () => visualization.DrawAndSaveImage(new List<RectangleWithWord>(), "", ImageFormat.Png);
            act.Should().Throw<FileNotFoundException>();
        }

        [Test]
        public void ShouldCorrectlyCreateImage()
        {
            var savePath = Path.GetDirectoryName(typeof(Program).Assembly.Location) + "\\image";
            var saver = new DefaultSaver(savePath, ImageFormat.Png);
            var visualization =
                new DefaultVisualization(new SolidBrush(Color.AliceBlue), new Font("times", 15),
                    new Size(10, 10), 10);
            visualization.DrawAndSaveImage(new List<RectangleWithWord>(), saver.GetSavePath(), ImageFormat.Png);
            File.Exists(saver.GetSavePath()).Should().BeTrue();
        }
    }
}
