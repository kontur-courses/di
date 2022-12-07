using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TagsCloudContainer.Infrastructure;

namespace TagsCloudContainer.Parser
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
            words = RemoveBoringWords(words);
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

        public Dictionary<string, int> RemoveBoringWords(Dictionary<string, int> source)
        {
            var boringWords = GetBoringWords();
            var result = new Dictionary<string, int>();
            foreach (var pair in source)
                if (!boringWords.Contains(pair.Key))
                    result.Add(pair.Key, pair.Value);
            return result;
        }

        public HashSet<string> GetBoringWords()
        {
            var boringWords = new HashSet<string>();
            using var reader = new StreamReader(fileSettings.SourceFilePath);
            while (reader.ReadLine() is {} line)
                boringWords.Add(line);
            return boringWords;
        }
    }
}
