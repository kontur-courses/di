namespace TagCloudCreator
{
    public class WordStatistic
    {
        public string Word { get; private set; }

        public int Count { get; private set; }

        public WordStatistic(string word, int count)
        {
            Word = word;
            Count = count;
        }
    }
}