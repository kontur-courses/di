using Microsoft.Extensions.DependencyInjection;
using TagsCloudContainer.Interfaces;
using TagsCloudContainer.Readers;
using TagsCloudContainer.TagsCloud;

namespace TagsCloudContainer
{
    public class Startup
    {
        public static ServiceProvider ConfigureServices()
        {
            return new ServiceCollection()
        .AddSingleton<IFileReader, TxtReader>()
        .AddSingleton<IPreprocessor, WordPreprocessor>()
        .AddSingleton<IImageSettings, ImageSettings>()
        .AddSingleton<FileReader>()
        .AddSingleton<ITagCloudGenerator, TagCloudGenerator>()
        .AddScoped(provider =>
        {
            var fileReader = provider.GetRequiredService<IFileReader>();
            var preprocessor = provider.GetRequiredService<IPreprocessor>();
            var tagCloudGenerator = provider.GetRequiredService<ITagCloudGenerator>();
            var imageSettings = provider.GetRequiredService<IImageSettings>();
            var fReader = provider.GetRequiredService<FileReader>();

            return new TagCloudApp(preprocessor, imageSettings, fReader);
        })
        .BuildServiceProvider();
        }
    }
}
