using System.Drawing;
using System.IO;
using Autofac;
using NUnit.Framework;
using TagsCloudCLI;
using TagsCloudVisualization;
using TagsCloudVisualization.Settings;

namespace TagsCloudVisualizationTests.FunctionalTest
{
    public class TagsCloudTest
    {
        [TestCase("txt", TestName = "txt")]
        public void Should_ReadWords_From(string extension)
        {
            var settings = GenerateDefaultSettings(extension);

            var builder = new ContainerBuilder();
            builder.RegisterModule(new TagsCloudModule(settings));
            var container = builder.Build();
            var imageSaver = new ImageSaver(settings.Saver.Directory, settings.Saver.ImageName);

            var visualizer = container.Resolve<Visualizer>();
            var image = visualizer.Visualize();
            
            imageSaver.Save(image);
        }

        private GeneralSettings GenerateDefaultSettings(string extension)
        {
            var font = new FontSettings(100, "Arial");
            var drawer = new DrawerSettings(Color.Red);
            var reader = new ReaderSettings(Path.Combine(Directory.GetCurrentDirectory(), $"text.{extension}"));
            var saver = new SaverSetting(Directory.GetCurrentDirectory(), "TestImage.png");
            var processor = new WordsPreprocessorSettings(new[] { "a", "b" });
            return new GeneralSettings(font, saver, processor, reader, drawer, new Point(0, 0));
        }
    }
}