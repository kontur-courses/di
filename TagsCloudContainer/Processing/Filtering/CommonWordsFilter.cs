using System.Collections.Generic;
using System.Linq;

namespace TagsCloudContainer.Processing.Filtering
{
    public class CommonWordsFilter : IWordFilter
    {
        private const int MinWordLength = 4;

        private static readonly PartOfSpeech[] CommonPartsOfSpeech = {
            PartOfSpeech.Numeral,
            PartOfSpeech.Union,
            PartOfSpeech.Interjection,
            PartOfSpeech.Particle,
            PartOfSpeech.Pretext,
            PartOfSpeech.Pronoun
        };

        public IEnumerable<string> Filter(IEnumerable<string> words)
        {
            var partsOfSpeech = PartOfSpeechDetector.Detect(words);

            return words
                .Where(w => w.Length >= MinWordLength)
                .Where(w => partsOfSpeech.TryGetValue(w, out var part) && !CommonPartsOfSpeech.Contains(part));
        }
    }
}