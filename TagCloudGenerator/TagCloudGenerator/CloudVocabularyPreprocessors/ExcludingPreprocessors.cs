using System.Collections.Generic;
using System.Linq;

namespace TagCloudGenerator.CloudVocabularyPreprocessors
{
    public class ExcludingPreprocessors : CloudVocabularyPreprocessor
    {
        private readonly HashSet<string> excludedWords;

        public ExcludingPreprocessors(CloudVocabularyPreprocessor nextPreprocessor, HashSet<string> excludedWords)
            : base(nextPreprocessor) =>
            this.excludedWords = excludedWords;

        protected override IEnumerable<string> ProcessVocabulary(IEnumerable<string> words) =>
            words.Where(word => !excludedWords.Contains(word));
    }
}