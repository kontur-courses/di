using System.Collections.Generic;

namespace TagsCloudContainer.TextParsers
{
    public class TextParser : ITextParser
    {
        private readonly string path;
        private readonly HashSet<string> excludedWords;
        private readonly Dictionary<string, int> wordsCount;
        private readonly ITextFormatReader textReader;
        private int totalWords;

        public TextParser(string path, IExcludingWords excludingWords,
            ITextFormatReader textReader)
        {
            this.path = path;
            this.excludedWords = excludingWords.GetWords();
            this.totalWords = 0;
            this.textReader = textReader;
            wordsCount = GetWordsFromFile(path);
        }

        public IReadOnlyDictionary<string, int> GetWordsCounts()
            => wordsCount;

        public int GetDistinctWordsAmount()
            => wordsCount.Count;

        public int GetTotalWordsCount()
            => totalWords;

        private Dictionary<string, int> GetWordsFromFile(string path)
        {
            var words = new Dictionary<string, int>();
            var lines = textReader.GetLines(path);
            foreach (var line in lines)
                ProcessLine(line, words);

            return words;
        }

        private void ProcessLine(string line, Dictionary<string, int> words)
        {
            var word = line.ToLower();
            word = word.TrimEnd('\n');
            if (excludedWords.Contains(word)) return;
            if (!words.TryGetValue(word, out _))
                words.Add(word, 0);
            words[word]++;
            totalWords++;
        }
    }
}
