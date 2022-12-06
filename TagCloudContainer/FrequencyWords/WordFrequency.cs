namespace TagCloudContainer.FrequencyWords
{
    public class WordFrequency
    {
        public string Word;
        public int Count;
        public WordFrequency(string word)
        {
            this.Word = word;
        }
        public void CountPlus()
        {
            Count++;
        }
    }
}
