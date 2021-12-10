using System.Collections.Generic;
using System.Linq;
using TagsCloudContainer.WordsPreparator;

namespace TagsCloudContainer.WordsFilter
{
    public class SpeechPartsFilter : IWordsFilter
    {
        private readonly SpeechPart[] excludedSpeechParts;

        public FilterType FilterType => FilterType.SpeechPart;

        
        public SpeechPartsFilter(params SpeechPart[] excludedSpeechParts)
        {
            this.excludedSpeechParts = excludedSpeechParts;
        }
        
        public ICollection<WordInfo> Filter(ICollection<WordInfo> words)
        {
            return words
                .Where(word => !excludedSpeechParts.Contains(word.SpeechPart))
                .ToArray();
        }
    }
}