using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using TagsCloud.ImageProcessing.Config;
using TagsCloud.Layouter.Factory;
using TagsCloud.TagsCloudProcessing;
using TagsCloud.TagsCloudProcessing.TagsGeneratorFactory;
using TagsCloud.TextProcessing.Converters;
using TagsCloud.TextProcessing.TextFilters;
using TagsCloud.TextProcessing.WordConfig;

namespace TagsCloudTest
{
    public class FunctionalTests
    {
        private TagsCloudCreator tagsCloudCreator;
        private IWordsConfig wordsConfig;
        private IImageConfig imageConfig;
        private ServiceProvider container = ContainerBuilder.BuildContainer();

        private string textPath;
        private string imagePath;

        [SetUp]
        public void SetUp()
        {
            var path = Assembly.GetExecutingAssembly().Location;
            var s = new DirectoryInfo(path).Parent.Parent.Parent.FullName;
            textPath = Path.Combine(s, "Resources", "text.txt");
            imagePath = Path.Combine(s, "Resources", "test.png");

            tagsCloudCreator = container.GetService<TagsCloudCreator>();

            wordsConfig = container.GetService<IWordsConfig>();
            ConfigureWordsConfig(textPath);

            imageConfig = container.GetService<IImageConfig>();
            ConfigureImageConfig(imagePath);
        }

        private void ConfigureWordsConfig(string path)
        {
            wordsConfig.Color = Color.Red;
            wordsConfig.ConvertersNames = container.GetService<IConvertersApplier>().GetConverterNames().ToArray();
            wordsConfig.FilerNames = container.GetService<IFiltersApplier>().GetFilerNames().ToArray();
            wordsConfig.FontName = new Font(FontFamily.GenericMonospace, 20);
            wordsConfig.LayoutName = container.GetService<IRectanglesLayoutersFactory>().GetLayouterNames().First();
            wordsConfig.TagGeneratorName = container.GetService<ITagsGeneratorFactory>().GetGeneratorNames().First();
            wordsConfig.Path = path;
        }

        private void ConfigureImageConfig(string path)
        {
            imageConfig.ImageSize = new Size(1000, 1000);
            imageConfig.Path = path;
        }

        [Test]
        public void FunctionalTest()
        {
            tagsCloudCreator.CreateCloud(textPath, imagePath);
            using var image = Image.FromFile(imagePath);
            image.Should().NotBeNull();
        }
    }
}
