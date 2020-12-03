using Microsoft.Extensions.DependencyInjection;
using System.Drawing;
using TagsCloud.Factory;
using TagsCloud.ImageProcessing.Config;
using TagsCloud.ImageProcessing.SaverImage.ImageSavers;
using TagsCloud.Layouter;
using TagsCloud.TagsCloudProcessing.TegsGenerators;
using TagsCloud.TextProcessing.Converters;
using TagsCloud.TextProcessing.TextFilters;

namespace TagsCloud.Extensions
{
    public static class ServiceProviderExtensions
    {
        public static ServiceProvider ConfigureFactories(this ServiceProvider serviceProvider)
        {
            serviceProvider.GetService<IServiceFactory<IImageSaver>>()
                .Register(".png", serviceProvider.GetService<PngSaver>)
                .Register(".jpg", serviceProvider.GetService<JpgSaver>)
                .Register(".bmp", serviceProvider.GetService<BmpSaver>);

            var layouterConfig = serviceProvider.GetService<ImageConfig>();
            serviceProvider.GetService<IServiceFactory<IRectanglesLayouter>>()
                .Register("По спирали",
                () => new CircularCloudLayouter(new Point(layouterConfig.ImageSize.Width / 2,
                layouterConfig.ImageSize.Height / 2)));

            serviceProvider.GetService<IServiceFactory<ITagsGenerator>>()
                .Register("Топ 30", serviceProvider.GetService<TagsGenerator>);

            serviceProvider.GetService<IConvertersApplier>()
                .Register("Перевести в нижний регистр", serviceProvider.GetService<LowerCaseConverter>);

            serviceProvider.GetService<IFiltersApplier>()
                .Register("Исключить служебные символы", serviceProvider.GetService<FunctionWordsFilter>);

            return serviceProvider;
        }
    }
}
