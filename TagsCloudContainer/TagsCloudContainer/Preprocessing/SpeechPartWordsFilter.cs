using System.Collections.Generic;
using System.Linq;
using TagsCloudContainer.Settings;

namespace TagsCloudContainer.Preprocessing
{
    public class SpeechPartWordsFilter : IWordsPreprocessor
    {
        private readonly IWordSpeechPartParser wordSpeechPartParser;
        private readonly ISpeechPartFilterSettings settings;

        public SpeechPartWordsFilter(
            IWordSpeechPartParser wordSpeechPartParser,
            ISpeechPartFilterSettings settings)
        {
            this.wordSpeechPartParser = wordSpeechPartParser;
            this.settings = settings;
        }

        public IEnumerable<string> Preprocess(IEnumerable<string> words)
        {
            return wordSpeechPartParser.ParseWords(words)
                .Where(word => !settings.SpeechPartsToRemove.Contains(word.SpeechPart))
                .Select(word => word.Word);
        }
    }
}