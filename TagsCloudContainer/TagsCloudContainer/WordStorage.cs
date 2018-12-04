using System.Collections.Generic;
using System.Linq;


namespace TagsCloudContainer
{
    class WordStorage: IWordStorage
    {
        private readonly Dictionary<string, int> _words;
        private readonly WordsCustomizer _wordsCustomizer;

        public WordStorage(WordsCustomizer customizer)
        {
            _words = new Dictionary<string, int>();
            _wordsCustomizer = customizer;
        }

        public void Add(string word)
        {
            word = _wordsCustomizer.CustomizeWord(word);

            if (word == null)
                return;
            
            if (!_words.ContainsKey(word))
                _words.Add(word, 1);
            else
                _words[word]++;
        }

        public void Add(IEnumerable<string> words)
        {
            foreach (var word in words)
                Add(word);
        }

        public List<Word> ToSortedList()
        {
            return _words.
                OrderByDescending(e => e.Value)
                .Select(e => new Word(e.Key, e.Value))
                .ToList();
        }
    }
}
