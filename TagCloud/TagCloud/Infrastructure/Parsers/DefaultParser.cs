namespace TagCloud
{
    public class DefaultParser : IParser
    {
        public string Parse(string word) =>
            word.ToLower();
    }
}
