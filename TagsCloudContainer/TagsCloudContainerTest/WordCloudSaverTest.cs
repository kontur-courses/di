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

namespace TagsCloudContainerTest
{
    public class WordCloudSaverTest
    {
        private IWordCloudPainter cloudPainter;
        private IImageSaver saver;
        private ImageSettings fakeSettings;

        [SetUp]
        public void InitializeService()
        {
            cloudPainter = A.Fake<IWordCloudPainter>();
            saver = A.Fake<IImageSaver>();
            fakeSettings = new ImageSettings(new Size(),FontFamily.GenericSansSerif,Color.Black, Color.White);
        }

        [Test]
        public void CheckCallsPaintWords()
        {
            var cloudSaver = new WordCloudSaver(cloudPainter, saver);

            cloudSaver.SaveCloud("", fakeSettings);

            A.CallTo(() => cloudPainter.PaintWords(fakeSettings)).MustHaveHappenedOnceExactly();
        }

        [Test]
        public void CheckCallsSave()
        {
            var image = new Bitmap(1, 1);
            A.CallTo(() => cloudPainter.PaintWords(fakeSettings)).WithAnyArguments().Returns(image);
            var cloudSaver = new WordCloudSaver(cloudPainter, saver);

            cloudSaver.SaveCloud("", fakeSettings);

            A.CallTo(() => saver.Save(image, "")).MustHaveHappenedOnceExactly();
        }
    }
}
