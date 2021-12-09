using System.Collections.Generic;
using System.Linq;
using TagsCloudApp.Parsers;
using TagsCloudApp.RenderCommand;
using TagsCloudContainer.Rendering;
using TagsCloudContainer.Settings;

namespace TagsCloudApp.Settings
{
    public class WordColorMapperSettings : IWordColorMapperSettings
    {
        public IWordColorMapper ColorMapper { get; }

        public WordColorMapperSettings(
            IEnumerable<IWordColorMapper> colorMappers,
            IRenderOptions renderOptions,
            IEnumParser enumParser)
        {
            var type = enumParser.Parse<ColorMapperType>(renderOptions.ColorMapperType);
            ColorMapper = colorMappers.First(mapper => mapper.Type == type);
        }
    }
}