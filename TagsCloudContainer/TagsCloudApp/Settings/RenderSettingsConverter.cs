using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using TagsCloudApp.Parsers;
using TagsCloudApp.RenderCommand;
using TagsCloudContainer.ColorMappers;
using TagsCloudContainer.MathFunctions;
using TagsCloudContainer.Preprocessing;
using TagsCloudContainer.Settings;

namespace TagsCloudApp.Settings
{
    public class RenderSettingsConverter : IRenderSettingsConverter
    {
        private readonly IRenderArgs renderArgs;
        private readonly IArgbColorParser colorParser;
        private readonly IEnumParser enumParser;
        private readonly IKeyValueParser keyValueParser;

        public RenderSettingsConverter(
            IRenderArgs renderArgs,
            IArgbColorParser colorParser,
            IEnumParser enumParser,
            IKeyValueParser keyValueParser)
        {
            this.renderArgs = renderArgs;
            this.colorParser = colorParser;
            this.enumParser = enumParser;
            this.keyValueParser = keyValueParser;
        }

        public IRenderSettings GetSettings()
        {
            return new RenderSettings(
                renderArgs.FontFamily,
                renderArgs.MaxFontSize,
                renderArgs.MinFontSize,
                renderArgs.ImageSize,
                renderArgs.ImageScale,
                colorParser.Parse(renderArgs.BackgroundColor),
                colorParser.Parse(renderArgs.DefaultColor),
                enumParser.Parse<WordColorMapperType>(renderArgs.ColorMapperType),
                GetSpeechPartColorMap(renderArgs.SpeechPartColorMap),
                enumParser.Parse<MathFunctionType>(renderArgs.WordsScale),
                renderArgs.IgnoredSpeechParts.Select(s => enumParser.Parse<SpeechPart>(s)).ToHashSet()
            );
        }

        private Dictionary<SpeechPart, Color> GetSpeechPartColorMap(string value)
        {
            var map = new Dictionary<SpeechPart, Color>();
            foreach (var (speechPart, color) in keyValueParser.Parse(value))
                map[enumParser.Parse<SpeechPart>(speechPart)] = colorParser.Parse(color);

            return map;
        }
    }
}