namespace TagsCloudTextProcessing
{
    public class Token
    {
        public string Word { get; }
        public int Count { get; }

        public Token(string word, int count)
        {
            Word = word;
            Count = count;
        }
    }
}