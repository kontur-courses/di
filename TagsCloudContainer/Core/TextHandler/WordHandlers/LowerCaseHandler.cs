namespace TagsCloudContainer.Core.TextHandler.WordHandlers
{
    class LowerCaseHandler : IWordHandler
    {
        public string Handle(string word) => word.ToLower();
    }
}
