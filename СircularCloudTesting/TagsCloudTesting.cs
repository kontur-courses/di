using System;
using System.Drawing;
using Autofac;
using FluentAssertions;
using NUnit.Framework;
using TagsCloudVisualization;
using TagsCloudVisualization.InterfacesForSettings;
using TagsCloudVisualization.TagsCloud;
using TagsCloudVisualization.TagsCloud.CircularCloud;

namespace СircularCloudTesting
{
    [TestFixture]
    public class TagsCloudTesting
    {

        private IContainer container;
        private Palette palette;
        private IImageSettings imageSettings;
        private IWordsSettings wordsSettings;
        private TagsCloudVisualizer visualizer;

        [SetUp]
        public void Init()
        {
            container = new DependencyBuilder().CreateContainer().Build();
            palette = container.Resolve<Palette>();
            palette.BackgroundColor = Color.White;
            palette.WordsColor = Color.Black;
            imageSettings = container.Resolve<IImageSettings>();
            imageSettings.Center = new Point(100, 100);
            imageSettings.ImageSize = new Size(200, 200);
            imageSettings.Font = new Font("Times New Roman", 5, FontStyle.Regular, GraphicsUnit.Pixel);
            wordsSettings = container.Resolve<IWordsSettings>();
            wordsSettings.PathToFile = $"{AppDomain.CurrentDomain.BaseDirectory}/TestingFiles/testText.txt";
            visualizer = container.Resolve<TagsCloudVisualizer>();

        }

        [Test]
        public void TagsCloud_Should_CorrectlyDisplayWordsWithCompression()
        {
            CircularCloudLayouter.IsCompressedCloud = true;
            var image = visualizer.DrawCircularCloud();
            var result = image.WithBitmapData(bmpData => bmpData.GetColorValues());
            var expectedImage = new Bitmap($"{AppDomain.CurrentDomain.BaseDirectory}/TestingFiles/CompressedTagCloud.png");
            var expectedResult = expectedImage.WithBitmapData(bmpData => bmpData.GetColorValues());

            result.Should().BeEquivalentTo(expectedResult);
        }

        [Test]
        public void TagsCloud_Should_CorrectlyDisplayWordsWithoutCompression()
        {
            CircularCloudLayouter.IsCompressedCloud = false;
            var image = visualizer.DrawCircularCloud();
            var result = image.WithBitmapData(bmpData => bmpData.GetColorValues());
            var expectedImage = new Bitmap($"{AppDomain.CurrentDomain.BaseDirectory}/TestingFiles/NormalTagCloud.png");
            var expectedResult = expectedImage.WithBitmapData(bmpData => bmpData.GetColorValues());

            result.Should().BeEquivalentTo(expectedResult);
        }
    }
}