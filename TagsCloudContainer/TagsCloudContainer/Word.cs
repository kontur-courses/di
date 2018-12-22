namespace TagsCloudContainer
{
    public class Word
    {
        public string Value { get; }
        public int Count { get; }

        public Word(string value, int count)
        {
            Value = value;
            Count = count;
        }
    }
}
