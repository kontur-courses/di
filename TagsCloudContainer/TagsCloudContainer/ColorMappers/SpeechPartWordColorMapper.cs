using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using TagsCloudContainer.Layout;
using TagsCloudContainer.Preprocessing;
using TagsCloudContainer.Settings;

namespace TagsCloudContainer.ColorMappers
{
    public class SpeechPartWordColorMapper : IWordColorMapper
    {
        public WordColorMapperType Type => WordColorMapperType.SpeechPart;

        private readonly IWordSpeechPartParser wordSpeechPartParser;
        private readonly ISpeechPartColorMapSettings colorMapSettings;
        private readonly IDefaultColorSettings defaultColor;

        public SpeechPartWordColorMapper(
            IWordSpeechPartParser wordSpeechPartParser,
            ISpeechPartColorMapSettings colorMapSettings,
            IDefaultColorSettings defaultColor)
        {
            this.wordSpeechPartParser = wordSpeechPartParser;
            this.colorMapSettings = colorMapSettings;
            this.defaultColor = defaultColor;
        }

        public Dictionary<WordLayout, Color> GetColorMap(CloudLayout layout)
        {
            var words = layout.WordLayouts.Select(wordLayout => wordLayout.Word);
            var speechParts = wordSpeechPartParser.ParseWords(words)
                .Select(speechPartWord => speechPartWord.SpeechPart);

            var wordLayoutColorMap = new Dictionary<WordLayout, Color>();
            foreach (var (speechPart, wordLayout) in speechParts.Zip(layout.WordLayouts))
            {
                wordLayoutColorMap[wordLayout] =
                    colorMapSettings.ColorMap.TryGetValue(speechPart, out var color)
                        ? color
                        : defaultColor.Color;
            }

            return wordLayoutColorMap;
        }
    }
}