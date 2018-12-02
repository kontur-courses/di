using System.Collections.Generic;
using System.Linq;

namespace TagsCloudContainer
{
    public class SimplePreprocessor : IPreprocessor
    {
        //ToDo Создать класс, где слова будут приводиться к одной форме, там же будет исключение разных частей речи
        private IWordExcluder wordExcluder;

        public SimplePreprocessor(IWordExcluder wordExcluder)
        {
            this.wordExcluder = wordExcluder;
        }

        public IEnumerable<string> GetValidWords(IEnumerable<string> words)
        {
            var forbiddenWords = GetForbiddenWords();
            var frequencyDictionary = GetFrequencyDictionary(words.Where(w => !forbiddenWords.Contains(w)));
            var validWords = frequencyDictionary
                .OrderBy(pair => pair.Value)
                .Reverse()
                .Select(pair => pair.Key);

            return validWords;
        }

        private Dictionary<string, int> GetFrequencyDictionary(IEnumerable<string> words)
        {
            var frequencyDictionary = new Dictionary<string, int>();
            foreach (var word in words)
            {
                if (!frequencyDictionary.ContainsKey(word))
                    frequencyDictionary[word] = 1;
                frequencyDictionary[word]++;
            }

            return frequencyDictionary;
        }

        private HashSet<string> GetForbiddenWords()
        {
            return wordExcluder.GetExcludedWords();
        }
    }
}
