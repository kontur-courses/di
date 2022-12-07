using TagsCloudContainer.Infrastructure;

namespace TagsCloudContainer.Algorithm
{
    public class Parser
    {
        private FileSettings fileSettings;

        public Parser(FileSettings fileSettings)
        {
            this.fileSettings = fileSettings;
        }

        public Dictionary<string, int> CountWordsWithoutBoring()
        {
            var words = CountWordsInSourceFile();
            words = RemoveBoringWords(words)
                .OrderByDescending(e => e.Value)
                .ToDictionary(e => e.Key, e => e.Value);
            return words;
        }

        public Dictionary<string, int> CountWordsInSourceFile()
        {
            var wordsCount = new Dictionary<string, int>();
            using var reader = new StreamReader(fileSettings.SourceFilePath);
            while (reader.ReadLine() is {} line)
                wordsCount[line] = wordsCount.ContainsKey(line) ? 1 : wordsCount[line] + 1;
            return wordsCount;
        }

        private Dictionary<string, int> RemoveBoringWords(Dictionary<string, int> source)
        {
            var boringWords = GetBoringWords();
            return source
                .Where(pair => !boringWords.Contains(pair.Key))
                .ToDictionary(pair => pair.Key, pair => pair.Value);
        }

        private HashSet<string> GetBoringWords()
        {
            var boringWords = new HashSet<string>();
            using var reader = new StreamReader(fileSettings.SourceFilePath);
            while (reader.ReadLine() is {} line)
                boringWords.Add(line);
            return boringWords;
        }
    }
}
