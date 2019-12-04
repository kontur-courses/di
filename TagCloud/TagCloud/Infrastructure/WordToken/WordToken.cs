namespace TagCloud
{
    public class WordToken
    {
        public readonly string Value;
        public readonly int Count;

        public WordToken(string value, int count)
        {
            Value = value;
            Count = count;
        }
    }
}
