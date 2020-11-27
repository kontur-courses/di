namespace TagsCloud.TextProcessing
{
    public class WordInfo
    {
        public string Value { get; private set; }
        public int Count { get; private set; }

        public WordInfo(string value, int count)
        {
            Value = value;
            Count = count;
        }
    }
}
