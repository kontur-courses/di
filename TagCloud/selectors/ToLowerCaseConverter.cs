namespace TagCloud.selectors
{
    public class ToLowerCaseConverter : IConverter<string>
    {
        public string Convert(string source) => source.ToLower();
    }
}