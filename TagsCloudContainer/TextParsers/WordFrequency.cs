namespace TagsCloudContainer.TextParsers
{
    public class WordFrequency
    {
        public WordFrequency(string word, int frequency)
        {
            Word = word;
            Frequency = frequency;
        }

        public string Word { get; }
        public int Frequency { get; }
    }
}