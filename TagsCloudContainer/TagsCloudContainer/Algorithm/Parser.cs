using DeepMorphy;
using TagsCloudContainer.Extensions;
using TagsCloudContainer.Infrastructure;

namespace TagsCloudContainer.Algorithm
{
    public class Parser : IParser
    {
        private FileSettings fileSettings;
        private MorphAnalyzer morph;

        public Parser(FileSettings fileSettings, MorphAnalyzer morph)
        {
            this.fileSettings = fileSettings;
            this.morph = morph;
        }

        public Dictionary<string, int> GetWordsCountWithoutBoring()
        {
            fileSettings.ThrowExcIfFileNotFound();

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
            while (reader.ReadLine()?.Trim().ToLower() is {} line)
                wordsCount[line] = wordsCount.ContainsKey(line) ? wordsCount[line] + 1 : 1;
            return wordsCount;
        }

        private Dictionary<string, int> RemoveBoringWords(Dictionary<string, int> source)
        {
            source = RemoveCustomBoringWords(source);
            var notBoringTypes = new[] { "сущ", "прил", "гл" };
            return source
                .Where(pair => notBoringTypes.Any(type =>
                    morph.GetGrams(pair.Key)["чр"].Contains(type)))
                .ToDictionary(pair => pair.Key, pair => pair.Value);
        }

        private Dictionary<string, int> RemoveCustomBoringWords(Dictionary<string, int> source)
        {
            var boringWords = GetCustomBoringWords();
            return source
                .Where(pair => !boringWords.Contains(pair.Key))
                .ToDictionary(pair => pair.Key, pair => pair.Value);
        }

        private HashSet<string> GetCustomBoringWords()
        {
            var boringWords = new HashSet<string>();
            using var reader = new StreamReader(fileSettings.CustomBoringWordsFilePath);
            while (reader.ReadLine()?.Trim().ToLower() is {} line)
                boringWords.Add(line);
            return boringWords;
        }
    }
}
