using TagsCloudContainer.ColorMappers;
using TagsCloudContainer.DependencyInjection;

namespace TagsCloudContainer.Settings
{
    public class WordColorMapperSettings : IWordColorMapperSettings
    {
        public IWordColorMapper ColorMapper { get; }

        public WordColorMapperSettings(
            IRenderSettings settings,
            IServiceResolver<WordColorMapperType, IWordColorMapper> resolver)
        {
            ColorMapper = resolver.GetService(settings.ColorMapperType);
        }
    }
}