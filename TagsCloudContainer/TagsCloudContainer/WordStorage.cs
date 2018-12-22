using System.Collections.Generic;
using System.Linq;


namespace TagsCloudContainer
{
    internal class WordStorage : IWordStorage
    {
        private readonly Dictionary<string, int> _wordsRegister;
        private readonly IWordsCustomizer _wordsCustomizer;

        public WordStorage(IWordsCustomizer customizer, IReader wordsReader)
        {
            _wordsRegister = new Dictionary<string, int>();
            _wordsCustomizer = customizer;
            AddRange(wordsReader.ReadWords());
        }

        public WordStorage(WordsCustomizer customizer, List<string> wordsToHandle)
        {
            _wordsRegister = new Dictionary<string, int>();
            _wordsCustomizer = customizer;
            AddRange(wordsToHandle);
        }

        public void Add(string word)
        {
            word = _wordsCustomizer.CustomizeWord(word);

            if (word == null)
                return;

            if (!_wordsRegister.ContainsKey(word))
                _wordsRegister.Add(word, 1);
            else
                _wordsRegister[word]++;
        }

        public void AddRange(IEnumerable<string> words)
        {
            foreach (var word in words)
                Add(word);
        }

        public IOrderedEnumerable<Word> GetOrderedByWordsFrequency()
        {
            return _wordsRegister
                .Select(e => new Word(e.Key, e.Value))
                .OrderByDescending(e => e.Count);
        }
    }
}