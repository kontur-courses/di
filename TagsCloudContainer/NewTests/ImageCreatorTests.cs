using System;
using System.Collections.Generic;
using System.IO;
using Autofac;
using TagsCloudContainer.ImageCreator;
using TagsCloudContainer.Reader;
using TagsCloudContainer.WordProcessor;
using TagsCloudContainer.WordsToSizesConverter;
using TagsCloudContainer.Layouter;
using TagsCloudContainer.RectanglesTransformer;
using TagsCloudContainer.Visualizer;
using TagsCloudContainer.ImageSaver;
using TagsCloudContainer.ImageSizeCalculator;
using FluentAssertions;
using NUnit.Framework;
using FakeItEasy;
using TagsCloudContainer.UI;

namespace TagsCloudContainer.NewTests
{
    class ImageCreatorTests
    {
        private IImageCreator imageCreator;
        private ITextReader reader;
        private IWordProcessor wordProcessor;
        private readonly IInitialSettings settings = new InitialSettings { OutputFilePath = "Test_image.png" };

        private static IContainer GetContainer(ContainerBuilder builder)
        {
            builder.RegisterType<WordFrequenciesToSizesConverter>().As<IWordFrequenciesToSizesConverter>().SingleInstance();
            builder.RegisterType<DefaultLayouterSettings>().As<ILayouterSettings>().SingleInstance();
            builder.RegisterType<CircularCloudLayouter>().As<ILayouter>().SingleInstance();
            builder.RegisterType<DefaultImageSizeCalculator>().As<IImageSizeCalculator>();
            builder.RegisterType<CenterRectanglesShifter>().As<IRectanglesTransformer>().SingleInstance();
            builder.RegisterType<DefaultVisualizerSettings>().As<IVisualizerSettings>().SingleInstance();
            builder.RegisterType<TagCloudVisualizer>().As<IVisualizer>().SingleInstance();
            builder.RegisterType<ImageSaver.ImageSaver>().As<IImageSaver>().SingleInstance();
            builder.RegisterType<ImageCreator.ImageCreator>().As<IImageCreator>().SingleInstance();
            return builder.Build();
        }

        [SetUp]
        public void MakeCreator()
        {
            reader = A.Fake<ITextReader>();
            var words = new List<string> {"Привет", "Земля", "и", "Земля"};
            A.CallTo(() => reader.ReadWords(null))
                .WithAnyArguments()
                .Returns(words);

            wordProcessor = A.Fake<IWordProcessor>();
            var wordsWithCounts = new List<WordWithCount>
            {
                new WordWithCount("земля", 2),
                new WordWithCount("привет", 1)
            };
            A.CallTo(() => wordProcessor.ProcessWords(words))
                .WithAnyArguments()
                .Returns(wordsWithCounts);

            var builder = new ContainerBuilder();
            builder.RegisterInstance(reader).As<ITextReader>();
            builder.RegisterInstance(wordProcessor).As<IWordProcessor>();
            var container = GetContainer(builder);
            imageCreator = container.Resolve<IImageCreator>();
        }

        [TearDown]
        public void DeleteImage()
        {
            File.Delete(settings.OutputFilePath);
        }

        [Test]
        public void ImageCreator_ShouldSaveImage()
        {
            imageCreator.CreateImage(settings);

            File.Exists(settings.OutputFilePath).Should().BeTrue();
        }

        [Test]
        public void ImageCreator_CallDependenciesMethodsOnce()
        {
            imageCreator.CreateImage(settings);

            A.CallTo(() => reader.ReadWords(null))
                .WithAnyArguments()
                .MustHaveHappenedOnceExactly();

            A.CallTo(() => wordProcessor.ProcessWords(null))
                .WithAnyArguments()
                .MustHaveHappenedOnceExactly();
        }
    }
}
