namespace TagCloudGenerator
{
    public class WordCounter
    {
        public Dictionary<string, int> CountWords(IEnumerable<string> text)
        {
            var words = new Dictionary<string, int>();

            foreach (string word in text)
            {
                if (words.ContainsKey(word))
                    words[word]++;
                else words[word] = 1;
            }

            return words
                .OrderByDescending(x => x.Value)
                .ToDictionary(x => x.Key, x => x.Value);
        }
    }
}
