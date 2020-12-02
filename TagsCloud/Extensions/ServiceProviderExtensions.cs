using Microsoft.Extensions.DependencyInjection;
using TagsCloud.ImageProcessing.SaverImage.Factory;
using TagsCloud.ImageProcessing.SaverImage.ImageSavers;
using TagsCloud.Layouter;
using TagsCloud.Layouter.Factory;
using TagsCloud.TagsCloudProcessing.TagsGeneratorFactory;
using TagsCloud.TagsCloudProcessing.TegsGenerators;
using TagsCloud.TextProcessing.Converters;
using TagsCloud.TextProcessing.TextFilters;

namespace TagsCloud.Extensions
{
    public static class ServiceProviderExtensions
    {
        public static ServiceProvider ConfigureFactories(this ServiceProvider serviceProvider)
        {
            serviceProvider.GetService<IImageSaverFactory>()
                .Register(".png", () => serviceProvider.GetService<PngSaver>())
                .Register(".jpg", () => serviceProvider.GetService<JpgSaver>())
                .Register(".bmp", () => serviceProvider.GetService<BmpSaver>());

            serviceProvider.GetService<IRectanglesLayoutersFactory>()
                .Register("По спирали", center => new CircularCloudLayouter(center));

            serviceProvider.GetService<ITagsGeneratorFactory>()
                .Register("Топ 30", () => serviceProvider.GetService<TagsGenerator>());

            serviceProvider.GetService<IConvertersApplier>()
                .Register("Перевести в нижний регистр", () => serviceProvider.GetService<LowerCaseConverter>());

            serviceProvider.GetService<IFiltersApplier>()
                .Register("Исключить служебные символы", () => serviceProvider.GetService<FunctionWordsFilter>());

            return serviceProvider;
        }
    }
}
