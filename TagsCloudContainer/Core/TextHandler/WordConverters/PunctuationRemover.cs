namespace TagsCloudContainer.Core.TextHandler.WordConverters
{
    class PunctuationRemover : IWordConverter
    {
        public string Handle(string word) => word.Trim('[', '-', '.', '?', '!', ')', '(', ',', ':', ']', '\'', '\"');
    }
}
