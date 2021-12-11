using System.Collections.Generic;
using System.Linq;
using TagsCloudContainer.WordsConverters;

namespace TagsCloudContainer.WordsFilter
{
    public class SpeechPartsFilter : IWordsFilter
    {
        private readonly SpeechPart[] selectedSpeechParts;

        public SpeechPartsFilter(params SpeechPart[] selectedSpeechParts)
        {
            this.selectedSpeechParts = selectedSpeechParts;
        }

        public ICollection<WordInfo> Filter(ICollection<WordInfo> words)
        {
            if (selectedSpeechParts.Length == 0)
                return words;
            return words
                .Where(word => selectedSpeechParts.Contains(word.SpeechPart))
                .ToArray();
        }
    }
}