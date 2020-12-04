namespace TagCloudCreator
{
    public class WordStatistic
    {
        public WordStatistic(string word, int count)
        {
            Word = word;
            Count = count;
        }

        public string Word { get; }

        public int Count { get; }
    }
}