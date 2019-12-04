using System.Collections.Generic;
using System.Linq;
using TagsCloudContainer.WordProcessing.Filtering.PartsOfSpeechQualifying;

namespace TagsCloudContainer.WordProcessing.Filtering
{
    public class ExcludingBoringWordsFilter : IWordFilter
    {
        private readonly IPartOfSpeechQualifier partOfSpeechQualifier;

        public ExcludingBoringWordsFilter(IPartOfSpeechQualifier partOfSpeechQualifier)
        {
            this.partOfSpeechQualifier = partOfSpeechQualifier;
        }

        public IEnumerable<string> FilterWords(IEnumerable<string> words)
        {
            var partsOfSpeech = partOfSpeechQualifier.QualifyPartsOfSpeech(words);
            return partsOfSpeech
                .Where(p => p.Item2 != PartOfSpeech.Pretext && p.Item2 != PartOfSpeech.Pronoun)
                .Select(p => p.Item1);
        }
    }
}