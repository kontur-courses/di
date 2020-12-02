using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using TagsCloud.ImageProcessing.Config;
using TagsCloud.TagsCloudProcessing;
using TagsCloud.TextProcessing.WordConfig;
using System.Drawing;
using TagsCloud.TextProcessing.Converters;
using System.Linq;
using TagsCloud.TextProcessing.TextFilters;
using TagsCloud.Layouter.Factory;
using TagsCloud.TagsCloudProcessing.TagsGeneratorFactory;
using System.IO;
using FluentAssertions;
using System.Reflection;

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
