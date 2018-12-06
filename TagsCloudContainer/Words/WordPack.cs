namespace TagsCloudContainer.Words
{
    public class WordPack
    {
        public string Key { get; }
        public int Count { get; }

        public WordPack(string key, int count)
        {
            Key = key;
            Count = count;
        }
    }
}
