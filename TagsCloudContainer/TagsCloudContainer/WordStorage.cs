using System.Collections.Generic;
using System.Linq;


namespace TagsCloudContainer
{
    class WordStorage: IWordStorage
    {
        private readonly Dictionary<string, int> _wordsRegister;
        private readonly WordsCustomizer _wordsCustomizer;

        public WordStorage(WordsCustomizer customizer)
        {
            _wordsRegister = new Dictionary<string, int>();
            _wordsCustomizer = customizer;
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

        public List<Word> ToList()
        {
            return _wordsRegister
                .OrderByDescending(e => e.Value)
                .Select(e => new Word(e.Key, e.Value))
                .ToList();
        }
    }
}
