using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using TagsCloud.Factory;
using TagsCloud.ImageProcessing.Config;
using TagsCloud.Layouter;
using TagsCloud.TagsCloudProcessing;
using TagsCloud.TagsCloudProcessing.TegsGenerators;
using TagsCloud.TextProcessing.Converters;
using TagsCloud.TextProcessing.TextFilters;
using TagsCloud.TextProcessing.WordsConfig;

namespace TagsCloudTest
{
    public class TagsCloudCreatorTests
    {
        private TagsCloudCreator tagsCloudCreator;
        private WordConfig wordsConfig;
        private ImageConfig imageConfig;
        private ServiceProvider container = ContainerBuilder.BuildContainer();

        private string textPath;
        private string imagePath;
        private string expectedImagePath;

        [SetUp]
        public void SetUp()
        {
            var path = TestContext.CurrentContext.TestDirectory;

            textPath = Path.Combine(path, "Resources", "text.txt");
            imagePath = Path.Combine(path, "Resources", "test.png");
            expectedImagePath = Path.Combine(path, "Resources", "expected.png");

            tagsCloudCreator = container.GetService<TagsCloudCreator>();

            wordsConfig = container.GetService<WordConfig>();
            ConfigureWordsConfig(textPath);

            imageConfig = container.GetService<ImageConfig>();
            ConfigureImageConfig(imagePath);
        }

        private void ConfigureWordsConfig(string path)
        {
            wordsConfig.Color = Color.Red;
            wordsConfig.ConvertersNames = container.GetService<IConvertersApplier>().GetConverterNames().ToArray();
            wordsConfig.FilerNames = container.GetService<IFiltersApplier>().GetFilerNames().ToArray();
            wordsConfig.FontName = new Font(FontFamily.GenericMonospace, 20);
            wordsConfig.LayoutName = container.GetService<IServiceFactory<IRectanglesLayouter>>()
                .GetServiceNames().First();
            wordsConfig.TagGeneratorName = container.GetService<IServiceFactory<ITagsGenerator>>()
                .GetServiceNames().First();
            wordsConfig.Path = path;
        }

        private void ConfigureImageConfig(string path)
        {
            imageConfig.ImageSize = new Size(1000, 1000);
            imageConfig.Path = path;
        }

        [Test]
        public void TagsCloudFunctionalTest()
        {
            tagsCloudCreator.CreateCloud(textPath, imagePath);

            using var image = Image.FromFile(imagePath);
            var current = File.ReadAllBytes(imagePath);
            var expected = File.ReadAllBytes(expectedImagePath);

            image.Should().NotBeNull();
            current.Should().BeEquivalentTo(expected);
        }
    }
}
