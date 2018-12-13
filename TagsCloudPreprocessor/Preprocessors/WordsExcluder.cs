using System.Collections.Generic;
using System.Linq;

namespace TagsCloudPreprocessor.Preprocessors
{
    public class WordsExcluder : IPreprocessor
    {
        private readonly IWordExcluder wordExcluder;

        public WordsExcluder(IWordExcluder wordExcluder)
        {
            this.wordExcluder = wordExcluder;
        }

        private HashSet<string> GetForbiddenWords()
        {
            return wordExcluder.GetExcludedWords();
        }

        private IEnumerable<string> ExcludeForbiddenWords(IEnumerable<string> words)
        {
            var forbiddenWords = GetForbiddenWords();
            return words.Where(w=>!forbiddenWords.Contains(w));
        }

        public List<string> PreprocessWords(List<string> words)
        {
            return ExcludeForbiddenWords(words).ToList();
        }
    }
}