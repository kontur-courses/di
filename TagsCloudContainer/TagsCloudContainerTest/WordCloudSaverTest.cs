using System.Drawing;
using NUnit.Framework;
using FakeItEasy;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TagsCloudContainer;
using System.IO;

namespace TagsCloudContainerTest
{
    public class WordCloudSaverTest
    {
        private IWordCloudPainter cloudPainter;
        private ImageSettings fakeSettings;

        [SetUp]
        public void InitializeService()
        {
            cloudPainter = A.Fake<IWordCloudPainter>();
            fakeSettings = new ImageSettings(new Size(),FontFamily.GenericSansSerif,Color.Black, Color.White);
        }

        [Test]
        public void CheckCallsPaintWords()
        {
            A.CallTo(() => cloudPainter.PaintWords(fakeSettings)).WithAnyArguments().Returns(new Bitmap(1, 1));
            var cloudSaver = new WordCloudSaver(cloudPainter);

            cloudSaver.SaveCloud("", "", fakeSettings, ImageFormats.png);

            A.CallTo(() => cloudPainter.PaintWords(fakeSettings)).MustHaveHappenedOnceExactly();
        }

        [Test]
        public void CheckSavingImage()
        {
            var dirPath = @"..\..\..\Files";
            var imageName = "test";
            A.CallTo(() => cloudPainter.PaintWords(fakeSettings)).WithAnyArguments().Returns(new Bitmap(1, 1));
            var cloudSaver = new WordCloudSaver(cloudPainter);

            cloudSaver.SaveCloud(dirPath, imageName, fakeSettings, ImageFormats.png);

            File.Exists($"{dirPath}\\{imageName}.png").Should().BeTrue();
        }
    }
}
