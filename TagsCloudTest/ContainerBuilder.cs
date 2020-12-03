using Microsoft.Extensions.DependencyInjection;
using System.Drawing;
using System.Reflection;
using TagsCloud.Factory;
using TagsCloud.ImageProcessing.Config;
using TagsCloud.ImageProcessing.SaverImage.ImageSavers;
using TagsCloud.Layouter;
using TagsCloud.TagsCloudProcessing.TegsGenerators;
using TagsCloud.TextProcessing.Converters;
using TagsCloud.TextProcessing.TextFilters;

namespace TagsCloudTest
{
    public static class ContainerBuilder
    {
        public static ServiceProvider BuildContainer()
        {
            var serviceProvider = new ServiceCollection()
                .Scan(scan => scan.FromAssemblyDependencies(Assembly.GetExecutingAssembly())
                                                                                .AddClasses()
                                                                                .AsSelfWithInterfaces()
                                                                                .WithSingletonLifetime())
                .BuildServiceProvider();

            serviceProvider.GetService<IServiceFactory<IImageSaver>>()
            .Register(".png", () => new PngSaver())
            .Register(".jpg", () => new JpgSaver())
            .Register(".bmp", () => new BmpSaver());

            var layouterConfig = serviceProvider.GetService<ImageConfig>();
            serviceProvider.GetService<IServiceFactory<IRectanglesLayouter>>()
                .Register("По спирали",
                () => new CircularCloudLayouter(new Point(layouterConfig.ImageSize.Width / 2,
                layouterConfig.ImageSize.Height / 2)));

            serviceProvider.GetService<IServiceFactory<ITagsGenerator>>()
                .Register("Топ 30", () => serviceProvider.GetService<TagsGenerator>());

            serviceProvider.GetService<IConvertersApplier>()
                .Register("Перевести в нижний регистр", () => new LowerCaseConverter());

            serviceProvider.GetService<IFiltersApplier>()
                .Register("Исключить служебные символы", () => new FunctionWordsFilter());

            return serviceProvider;
        }
    }
}
