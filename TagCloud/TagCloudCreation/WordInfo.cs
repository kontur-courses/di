namespace TagCloudCreation
{
    public class WordInfo
    {
        public WordInfo(string word, int count)
        {
            Word = word;
            Count = count;
        }

        public string Word { get; }
        public int Count { get; }
    }
}
