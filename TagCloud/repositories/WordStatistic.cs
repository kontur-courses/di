namespace TagCloud.repositories
{
    public class WordStatistic
    {
        public readonly string Word;
        public readonly int Count;

        public WordStatistic(string word, int count)
        {
            Word = word;
            Count = count;
        }
    }
}