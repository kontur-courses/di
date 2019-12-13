namespace TagsCloudContainer.WordProcessing
{
    public class WordData
    {
        public string Word { get; }
        public int Count { get; }

        public WordData(string word, int count)
        {
            Word = word;
            Count = count;
        }
    }
}