namespace TagsCloudContainer.Core.TextHandler.WordHandlers
{
    class PunctuationRemover : IWordHandler
    {
        public string Handle(string word) => word.Trim('[', '-', '.', '?', '!', ')', '(', ',', ':', ']', '\'', '\"');
    }
}
