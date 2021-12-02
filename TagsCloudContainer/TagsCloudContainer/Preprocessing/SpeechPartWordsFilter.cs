using System;
using System.Collections.Generic;
using System.Linq;

namespace TagsCloudContainer.Preprocessing
{
    public interface IWordsFilter
    {
        IEnumerable<string> Filter(IEnumerable<string> words);
    }

    public class SpeechPartWordsFilter : IWordsFilter
    {
        private readonly IWordSpeechPartParser wordSpeechPartParser;
        private readonly HashSet<SpeechPart> speechPartsToRemove;

        public SpeechPartWordsFilter(IWordSpeechPartParser wordSpeechPartParser,
            HashSet<SpeechPart> speechPartsToRemove)
        {
            this.wordSpeechPartParser =
                wordSpeechPartParser ?? throw new ArgumentNullException(nameof(wordSpeechPartParser));

            this.speechPartsToRemove =
                speechPartsToRemove ?? throw new ArgumentNullException(nameof(speechPartsToRemove));
        }

        public IEnumerable<string> Filter(IEnumerable<string> words)
        {
            if (words == null)
                throw new ArgumentNullException(nameof(words));

            return wordSpeechPartParser.ParseWords(words)
                .Where(word => !speechPartsToRemove.Contains(word.SpeechPart))
                .Select(word => word.Word);
        }
    }
}