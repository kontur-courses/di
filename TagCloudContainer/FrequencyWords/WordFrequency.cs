namespace TagCloudContainer.FrequencyWords
{
    public class WordFrequency
    {
        public string Word { get; }
        public int Count { get; }
        public WordFrequency(string word, int count)
        {
            Word = word;
            Count = count;
        }
    }
}
