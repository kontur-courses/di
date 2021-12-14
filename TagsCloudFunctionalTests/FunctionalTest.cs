using System.Drawing;
using System.IO;
using Autofac;
using CloudImageSaver;
using FluentAssertions;
using NUnit.Framework;

using TagsCloudVisualization;
using TagsCloudVisualization.Settings;

namespace TagsCloudFunctionalTests
{
    public class TagsCloudTest
    {
        [TestCase("txt", TestName = "txt")]
        [TestCase("docx", TestName = "doc")]
        public void Should_ReadWords_From(string extension)
        {
            var settings = GenerateDefaultSettings(extension);

            var builder = new ContainerBuilder();
            builder.RegisterModule(new TagsCloudModule(settings));
            var container = builder.Build();
            var imageSaver = new ImageSaver(settings.Saver.Directory, settings.Saver.ImageName);

            var visualizer = container.Resolve<Visualizer>();
            var actualImage = visualizer.Visualize();
            
            imageSaver.Save(actualImage);
            var expectedImage = Image.FromFile("TestImage.png");
            actualImage.HorizontalResolution.Should().BeApproximately(expectedImage.HorizontalResolution, 0.1F);
            actualImage.VerticalResolution.Should().BeApproximately(expectedImage.VerticalResolution, 0.1F);
            actualImage.Should().BeEquivalentTo(expectedImage, 
                options => options
                    .Excluding(image => image.Flags)
                    .Excluding(image => image.Palette.Flags)
                    .Excluding(image => image.PropertyIdList)
                    .Excluding(image => image.PropertyItems)
                    .Excluding(image => image.HorizontalResolution)
                    .Excluding(image => image.VerticalResolution)
                    .Excluding(image => image.RawFormat));
        }

        private GeneralSettings GenerateDefaultSettings(string extension)
        {
            var font = new FontSettings(100, "Arial");
            var drawer = new DrawerSettings(Color.Red);
            var reader = new ReaderSettings(Path.Combine(Directory.GetCurrentDirectory(), $"text.{extension}"));
            var saver = new SaverSetting(Directory.GetCurrentDirectory(), "TestIm1.png");
            var processor = new WordsPreprocessorSettings(new[] { "a", "b" });
            return new GeneralSettings(font, saver, processor, reader, drawer, new Point(0, 0));
        }
    }
}