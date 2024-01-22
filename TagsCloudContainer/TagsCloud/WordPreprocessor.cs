using TagsCloudContainer.Interfaces;

namespace TagsCloudContainer.TagsCloud
{
    public class WordPreprocessor : IPreprocessor
    {
        private readonly HashSet<string> boringWords;

        public WordPreprocessor(IEnumerable<string> boringWords)
        {
            this.boringWords = new HashSet<string>(boringWords, StringComparer.OrdinalIgnoreCase);
        }

        public IEnumerable<string> Process(IEnumerable<string> words, string boringWordsFilePath)
        {
            var lowercasedWords = words.Select(word => word.ToLower());

            var boringWordsFromFile = File.ReadAllLines(boringWordsFilePath).Select(line => line.ToLower()).ToHashSet();

            var filteredWords = lowercasedWords.Where(word => !IsBoringWord(word, boringWordsFromFile));

            return filteredWords;
        }

        private bool IsBoringWord(string word, HashSet<string> boringWords)
        {
            return boringWords.Contains(word);
        }

    }
}
