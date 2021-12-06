namespace TagCloud.filters
{
    public class LowerCaseWordFilter : IWordFilter
    {
        public string Filter(string source) => source.ToLower();
    }
}