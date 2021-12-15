using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace TagsCloudContainer.TextParsers
{
    public class TextParser : ITextParser
    {
        private readonly string path;
        private readonly HashSet<string> excludedWords;
        private readonly Dictionary<string, int> wordsCount;
        private int totalWords;

        public TextParser(string path, IExcludingWords excludingWords)
        {
            this.path = path;
            this.excludedWords = excludingWords.GetWords();
            this.totalWords = 0;
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
            using (StreamReader sr = new StreamReader(path, System.Text.Encoding.Default))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    line = line.ToLower();
                    line = line.TrimEnd('\n');
                    if (excludedWords.Contains(line)) continue;
                    if (!words.TryGetValue(line,  out _))
                        words.Add(line, 0);
                    words[line]++;
                    totalWords++;
                }
            }

            return words;
        }
    }
}
