using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using TagsCloud.ImageProcessing.Config;
using TagsCloud.ImageProcessing.ImageBuilders;
using TagsCloud.ImageProcessing.SaverImage.Factory;
using TagsCloud.ImageProcessing.SaverImage.ImageSavers;
using TagsCloud.Layouter;
using TagsCloud.Layouter.Factory;
using TagsCloud.TagsCloudProcessing;
using TagsCloud.TagsCloudProcessing.TagsGeneratorFactory;
using TagsCloud.TagsCloudProcessing.TegsGenerators;
using TagsCloud.TextProcessing;
using TagsCloud.TextProcessing.Converters;
using TagsCloud.TextProcessing.TextFilters;
using TagsCloud.TextProcessing.TextReaders;
using TagsCloud.TextProcessing.WordConfig;
using TagsCloud.UserInterfaces.GUI;

namespace TagsCloudTest
{
    public static class ContainerBuilder
    {
        public static ServiceProvider BuildContainer()
        {
            var serviceProvider = new ServiceCollection()
                .Scan(scan => scan.FromCallingAssembly().AddClasses().AsSelfWithInterfaces().WithTransientLifetime())

                .AddSingleton<IImageConfig, ImageConfig>()
                .AddSingleton<IWordsConfig, WordConfig>()

                .AddSingleton<TagsCloudCreator, TagsCloudCreator>()
                .AddSingleton<TextProcessor, TextProcessor>()

                .AddSingleton<IWordsReader, DocReader>()
                .AddSingleton<IWordsReader, TxtReader>()

                .AddSingleton<IImageBuilder, ImageBuilder>()
                .AddSingleton<IReadersFactory, ReadersFactory>()
                .AddSingleton<IRectanglesLayoutersFactory, RectanglesLayoutersFactory>()
                .AddSingleton<ITagsGeneratorFactory, TagsGeneratorFactory>()
                .AddSingleton<IConvertersApplier, ConvertersApplier>()
                .AddSingleton<IImageSaverFactory, ImageSaverFactory>()
                .AddSingleton<IFiltersApplier, FiltersApplier>()

                .AddTransient<TagsGenerator, TagsGenerator>()

                .BuildServiceProvider();

            serviceProvider.GetService<IImageSaverFactory>()
            .Register(".png", () => new PngSaver())
            .Register(".jpg", () => new JpgSaver())
            .Register(".bmp", () => new BmpSaver());

            serviceProvider.GetService<IRectanglesLayoutersFactory>()
                .Register("По спирали", center => new CircularCloudLayouter(center));

            serviceProvider.GetService<ITagsGeneratorFactory>()
                .Register("Топ 30", () => serviceProvider.GetService<TagsGenerator>());

            serviceProvider.GetService<IConvertersApplier>()
                .Register("Перевести в нижний регистр", () => new LowerCaseConverter());

            serviceProvider.GetService<IFiltersApplier>()
                .Register("Исключить служебные символы", () => new FunctionWordsFilter());

            return serviceProvider;
        }
    }
}
