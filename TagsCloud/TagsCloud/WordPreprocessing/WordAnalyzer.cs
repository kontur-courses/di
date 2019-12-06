using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace TagsCloud.WordPreprocessing
{
    public class WordAnalyzer : IWordAnalyzer
    {
        private readonly Dictionary<string, int> _wordsFrequency;
        private readonly char[] _delimiters = new char[] {',', '.', ' ', ':', ';', '(', ')', '—', '–', '[', ']', '!', '?'};

        public WordAnalyzer(IWordGetter wordGetter)
        {
            var words = wordGetter.GetWords(_delimiters);
            _wordsFrequency =  words
                .Select(s => s.ToLower())
                .GroupBy(g => g)
                .Select(s => new KeyValuePair<string, int>(s.Key, s.Count()))
                .ToDictionary(x => x.Key, x => x.Value);
        }

        public Dictionary<string, int> GetWordsStatistics()
        {
            return _wordsFrequency;
        }
    }
}