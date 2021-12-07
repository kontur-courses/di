using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using TagsCloudContainer.Layout;
using TagsCloudContainer.Preprocessing;
using TagsCloudContainer.Settings.Interfaces;

namespace TagsCloudContainer.Rendering
{
    public class SpeechPartWordColorMapper : IWordColorMapper
    {
        private readonly IWordSpeechPartParser wordSpeechPartParser;
        private readonly ISpeechPartWordColorMapperSettings settings;

        public SpeechPartWordColorMapper(IWordSpeechPartParser wordSpeechPartParser,
            ISpeechPartWordColorMapperSettings settings)
        {
            this.wordSpeechPartParser = wordSpeechPartParser;
            this.settings = settings;
        }

        public Dictionary<WordLayout, Color> GetColorMap(CloudLayout layout)
        {
            if (layout == null)
                throw new ArgumentNullException(nameof(layout));

            var words = layout.WordLayouts.Select(wordLayout => wordLayout.Word);
            var speechParts = wordSpeechPartParser.ParseWords(words)
                .Select(speechPartWord => speechPartWord.SpeechPart);

            var wordLayoutColorMap = new Dictionary<WordLayout, Color>();

            foreach (var (speechPart, wordLayout) in speechParts.Zip(layout.WordLayouts))
                wordLayoutColorMap[wordLayout] = settings.ColorMap.TryGetValue(speechPart, out var color)
                    ? color
                    : settings.DefaultColor;

            return wordLayoutColorMap;
        }
    }
}