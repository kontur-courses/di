using System.Collections.Generic;
using System.Linq;
using NHunspell;

namespace TagsCloudPreprocessor.Preprocessors
{
    public class WordsStemer:IPreprocessor
    {
        public List<string> PreprocessWords(List<string> words)
        {
            return GetWordsStem(words).ToList();
        }

        private IEnumerable<string> GetWordsStem(IEnumerable<string> words)
        {
            var h = new Hunspell(@"ru_RU.aff", @"ru_RU.dic");
            
            return words
                .Select(word => h.Stem(word))
                .Select(stem => stem.FirstOrDefault())
                .Where(wordStem => wordStem != null)
                .ToList();
        }
    }
}