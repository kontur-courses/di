namespace TagsCloudContainer
{
    public class Token
    {
        public string Value { get; }
        public uint Count { get; }

        public Token(string value, uint count)
        {
            Value = value;
            Count = count;
        }
    }
}
