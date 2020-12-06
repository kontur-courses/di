using System.Collections.Generic;
using System.Linq;
using TagsCloudVisualization.AppSettings;

namespace TagsCloudVisualization.WordsProcessing.WordsFilters
{
    public class WordFilter : IWordFilter
    {
        private readonly WordsSettings wordsSettings;

        public WordFilter(WordsSettings wordsSettings)
        {
            this.wordsSettings = wordsSettings;
        }

        public IEnumerable<string> FilterWords(IEnumerable<string> words)
        {
            var forbiddenWords = wordsSettings.ForbiddenWords.Select(word => word.ToLower()).ToHashSet();

            return words
                .Where(word =>
                    !forbiddenWords.Contains(word) && 
                    word.Length >= wordsSettings.MinLength && word.Length <= wordsSettings.MaxLength);
        }
    }
}