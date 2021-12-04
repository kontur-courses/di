namespace TagsCloudVisualization
{
    public readonly struct WordCount
    {
        public readonly string Word;
        public readonly int Count;

        public WordCount(string word, int count)
        {
            Word = word;
            Count = count;
        }
    }
}