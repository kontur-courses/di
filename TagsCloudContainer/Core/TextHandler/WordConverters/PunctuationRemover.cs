namespace TagsCloudContainer.Core.TextHandler.WordConverters
{
    class PunctuationRemover : IWordConverter
    {
        public string Convert(string word) =>
            word.Trim('[', '-', ';', '.', '?', '!', ')', '(', ',', ':', ']', '\'', '\"');
    }
}