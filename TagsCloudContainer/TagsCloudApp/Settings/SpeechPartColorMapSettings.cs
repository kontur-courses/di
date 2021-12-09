using System.Collections.Generic;
using System.Drawing;
using TagsCloudApp.Parsers;
using TagsCloudApp.RenderCommand;
using TagsCloudContainer.Preprocessing;
using TagsCloudContainer.Settings;

namespace TagsCloudApp.Settings
{
    public class SpeechPartColorMapSettings : ISpeechPartColorMapSettings
    {
        public Dictionary<SpeechPart, Color> ColorMap { get; } = new();

        public SpeechPartColorMapSettings(
            IRenderOptions renderOptions,
            IEnumParser enumParser,
            IArgbColorParser colorParser,
            IKeyValueParser keyValueParser)
        {
            var pairs = keyValueParser.Parse(renderOptions.SpeechPartColorMap);
            foreach (var (speechPart, color) in pairs)
                ColorMap[enumParser.Parse<SpeechPart>(speechPart)] = colorParser.Parse(color);
        }
    }
}