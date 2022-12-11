namespace TagCloudContainer.FrequencyWords
{
    public class FrequencyTags : IFrequencyCounter
    {
        public IEnumerable<WordFrequency> GetWordsFrequency(IEnumerable<string> words)
        {
            if (words == null)
                throw new ArgumentNullException();
            return words
                .GroupBy(w => w)
                .Select(word =>
                    new WordFrequency(word.Key, word.Count()))
                .OrderByDescending(x => x.Count);
        }
    }
}