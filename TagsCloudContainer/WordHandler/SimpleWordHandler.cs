namespace TagsCloudContainer.WordHandler
{
    public class SimpleWordHandler : IWordHandler
    {
        public string Transform(string word)
        {
            return word;
        }
    }
}