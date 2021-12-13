using System.Collections.Generic;
using System.Linq;
using TagsCloudContainer.WordsPreparator;

namespace TagsCloudContainer.WordsFilter
{
    public class SpeechPartsFilter : IWordsFilter
    {
        private readonly SpeechPart[] selectedSpeechParts;


        public SpeechPartsFilter(ITagCloudSettings settings)
        {
            selectedSpeechParts = settings.SelectedSpeechParts.ToArray();
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