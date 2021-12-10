using TagsCloudApp.Parsers;
using TagsCloudApp.RenderCommand;
using TagsCloudContainer.ColorMappers;
using TagsCloudContainer.DependencyInjection;
using TagsCloudContainer.Settings;

namespace TagsCloudApp.Settings
{
    public class WordColorMapperSettings : IWordColorMapperSettings
    {
        public IWordColorMapper ColorMapper { get; }

        public WordColorMapperSettings(
            IServiceResolver<WordColorMapperType, IWordColorMapper> colorMapperResolver,
            IRenderOptions renderOptions,
            IEnumParser enumParser)
        {
            var type = enumParser.Parse<WordColorMapperType>(renderOptions.ColorMapperType);
            ColorMapper = colorMapperResolver.GetService(type);
        }
    }
}