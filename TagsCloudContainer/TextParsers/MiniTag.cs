namespace TagsCloudContainer.TextParsers
{
    public class MiniTag
    {
        public MiniTag(string word, int count)
        {
            Word = word;
            Count = count;
        }

        public string Word { get; }
        public int Count { get; }
    }
}