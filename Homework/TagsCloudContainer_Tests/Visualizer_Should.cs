using System;
using System.Collections.Generic;
using System.Drawing;
using FakeItEasy;
using FluentAssertions;
using NUnit.Framework;
using TagsCloudContainer.Layouter;
using TagsCloudContainer.Visualizer;
using TagsCloudContainer.Visualizer.ColorGenerators;
using TagsCloudContainer.Visualizer.VisualizerSettings;

namespace TagsCloudContainer_Tests
{
    [TestFixture]
    public class Visualizer_Should
    {
        [SetUp]
        public void SetUp()
        {
            layouter = A.Fake<ICloudLayouter>();
            settings = A.Fake<IVisualizerSettings>();
            colorGenerator = A.Fake<IColorGenerator>();
            A.CallTo(() => settings.ImageSize).Returns(new Size(1920, 1080));
            A.CallTo(() => settings.Font).Returns(new Font(FontFamily.GenericMonospace, 20));
            A.CallTo(() => settings.WordsColorGenerator).Returns(colorGenerator);
            A.CallTo(() => colorGenerator.GetColors(A<int>.Ignored)).Returns(FakeColors(freqDict.Count));
            sut = new Visualizer(settings, layouter);
        }

        private Visualizer sut;

        private readonly Dictionary<string, int> freqDict = new()
        {
            {"а", 1}
        };

        private Stack<Color> testingColors;
        private ICloudLayouter layouter;
        private IVisualizerSettings settings;
        private IColorGenerator colorGenerator;

        [Test]
        public void CreateImage_WithSetSize()
        {
            A.CallTo(() => settings.ImageSize).Returns(new Size(1920, 1080));
            sut.Visualize(freqDict)
                .Should().Match<Bitmap>(b => b.Size == settings.ImageSize);
        }

        [Test]
        public void Throw_WhenLayouterPutWordOutsideImage()
        {
            A.CallTo(() => layouter.PutNextRectangle(A<Size>.Ignored))
                .Returns(new Rectangle(new Point(settings.ImageSize), new Size(5, 5)));
            Assert.Throws<Exception>(() => sut.Visualize(freqDict));
        }

        [Test]
        public void Throw_WhenEmptySize()
        {
            A.CallTo(() => settings.ImageSize).Returns(new Size());
            A.CallTo(() => layouter.PutNextRectangle(A<Size>.Ignored)).Returns(new Rectangle(0, 0, 2, 2));
            Assert.Throws<ArgumentException>(() => new Visualizer(settings, layouter));
        }

        private Stack<Color> FakeColors(int count)
        {
            var faked = new Stack<Color>();
            for (var i = 0; i < count; i++)
                faked.Push(Color.Aqua);
            return faked;
        }
    }
}