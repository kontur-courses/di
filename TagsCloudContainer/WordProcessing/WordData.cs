namespace TagsCloudContainer.WordProcessing
{
    public class WordData
    {
        public readonly string Word;
        public readonly int Count;

        public WordData(string word, int count)
        {
            Word = word;
            Count = count;
        }
    }
}