namespace TagCloud.selectors
{
    public class EmptyFilter : IWordFilter
    {
        public bool Filter(string source) => source.Length > 0;
    }
}