namespace TagCloudContainer.FrequencyWords
{
    public class FrequencyTags : IFrequencyCounter
    {
        public IEnumerable<WordFrequency> GetWordsFrequency(IEnumerable<string> words)
        {
            if (words == null)
                throw new ArgumentNullException();
            var wordFrequencies = new Dictionary<string, WordFrequency>();
            foreach (var word in words)
            {
                if (!wordFrequencies.ContainsKey(word))
                    wordFrequencies[word] = new WordFrequency(word);
                wordFrequencies[word].CountPlus();
            }
            return wordFrequencies.Values.OrderByDescending(x => x.Count);
        }
    }
}