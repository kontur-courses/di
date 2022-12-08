namespace TagCloudContainer.FrequencyWords
{
    public interface IFrequencyCounter
    {
        IEnumerable<WordFrequency> GetWordsFrequency(IEnumerable<string> words);
    }
}
