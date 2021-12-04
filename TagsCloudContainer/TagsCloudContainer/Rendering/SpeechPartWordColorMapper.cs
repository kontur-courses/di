using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using TagsCloudContainer.Layout;
using TagsCloudContainer.Preprocessing;

namespace TagsCloudContainer.Rendering
{
    public class SpeechPartWordColorMapper : IWordColorMapper
    {
        private readonly IWordSpeechPartParser wordSpeechPartParser;
        private readonly Dictionary<SpeechPart, Color> colorMap;
        private readonly Color defaultColor;

        public SpeechPartWordColorMapper(IWordSpeechPartParser wordSpeechPartParser,
            Dictionary<SpeechPart, Color> colorMap, Color defaultColor)
        {
            this.wordSpeechPartParser =
                wordSpeechPartParser ?? throw new ArgumentNullException(nameof(wordSpeechPartParser));

            this.colorMap = colorMap ?? throw new ArgumentNullException(nameof(colorMap));
            this.defaultColor = defaultColor;
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
                wordLayoutColorMap[wordLayout] = colorMap.TryGetValue(speechPart, out var color) ? color : defaultColor;

            return wordLayoutColorMap;
        }
    }

    public interface IWordColorMapper
    {
        public Dictionary<WordLayout, Color> GetColorMap(CloudLayout layout);
    }
}