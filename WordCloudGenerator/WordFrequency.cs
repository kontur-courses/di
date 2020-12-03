namespace WordCloudGenerator
{
    public readonly struct WordFrequency
    {
        public WordFrequency(string word, double frequency)
        {
            Word = word;
            Frequency = frequency;
        }

        public string Word { get; }
        public double Frequency { get; }
    }
}