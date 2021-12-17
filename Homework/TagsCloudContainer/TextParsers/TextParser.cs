using System.Collections.Generic;

namespace TagsCloudContainer.TextParsers
{
    public class TextParser : ITextParser
    {
        private readonly HashSet<string> excludedWords;
        private readonly Dictionary<string, int> wordsCount;
        private readonly ITextFormatReader textReader;
        private int totalWords;

        public TextParser(string path, IExcludingWords excludingWords,
            ITextFormatReader textReader)
        {
            excludedWords = excludingWords.GetWords();
            totalWords = 0;
            this.textReader = textReader;
            wordsCount = GetWordsFromFile(path);
        }

        public IReadOnlyDictionary<string, int> GetWordsCounts()
            => wordsCount;

        public int GetDistinctWordsAmount()
            => wordsCount.Count;

        private Dictionary<string, int> GetWordsFromFile(string pathToText)
        {
            var words = new Dictionary<string, int>();
            var lines = textReader.GetLines(pathToText);
            foreach (var line in lines)
                ProcessLine(line, words);

            return words;
        }

        private void ProcessLine(string line, IDictionary<string, int> words)
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
