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

        public IEnumerable<string> Process(IEnumerable<string> words)
        {
            var lowercasedWords = words.Select(word => word.ToLower());

            var filteredWords = lowercasedWords.Where(word => !IsBoringWord(word));

            return filteredWords;
        }

        private bool IsBoringWord(string word)
        {
            return boringWords.Contains(word);
        }
    }
}
