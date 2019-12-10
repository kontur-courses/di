namespace TagsCloudContainer.TokensAndSettings
{
    public class WordToken
    {
        public string Word { get; }
        public int Count { get; }

        public WordToken(string word, int count)
        {
            Word = word;
            Count = count;
        }
    }
}
