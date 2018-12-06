using System.Collections.Generic;
using System.Linq;

namespace TagsCloudContainer.Processing.Filtering
{
    public class CommonWordsFilter : IWordFilter
    {
        private const int MinimumWordLength = 4;

        private static readonly PartOfSpeech[] commonPartsOfSpeech = {
            PartOfSpeech.Numeral,
            PartOfSpeech.Union,
            PartOfSpeech.Interjection,
            PartOfSpeech.Particle,
            PartOfSpeech.Pretext,
            PartOfSpeech.Pronoun,
            PartOfSpeech.Unknown
        };

        public IEnumerable<string> Filter(IEnumerable<string> words)
        {
            return words.Where(word => word.Length >= MinimumWordLength && !IsCommonPartOfSpeech(word));
        }

        public bool IsCommonPartOfSpeech(string word)
        {
            var partOfSpeech = PartOfSpeechDetector.Detect(word);
            return commonPartsOfSpeech.Contains(partOfSpeech);
        }
    }
}