namespace TagCloud.selectors
{
    public class ToLowerCaseHandler : IWordHandler
    {
        public string Handle(string source) => source.ToLower();
    }
}