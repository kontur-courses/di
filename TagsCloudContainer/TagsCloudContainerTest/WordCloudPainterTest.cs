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
    public class WordCloudPainterTest
    {
        private IWordCloudCreator cloudCreator;
        private Graphics fakeGraphic;
        private ImageSettings fakeSettings;
        
        [SetUp]
        public void InitializeServices()
        {
            cloudCreator = A.Fake<IWordCloudCreator>();
            fakeGraphic = Graphics.FromImage(new Bitmap(1, 1));
            fakeSettings = new ImageSettings
                (
                    new Size(1, 1),
                    FontFamily.GenericSansSerif,
                    Color.Black,
                    Color.White
                );
        }

        [Test]
        public void CheckCallGetWordCloud()
        {
            var cloudPainter = new WordCloudPainter(cloudCreator);

            cloudPainter.PaintWords(fakeSettings);

            A.CallTo(() => cloudCreator.GetWordCloud(fakeGraphic, fakeSettings))
                .WithAnyArguments()
                .MustHaveHappenedOnceExactly();
        }
    }
}
