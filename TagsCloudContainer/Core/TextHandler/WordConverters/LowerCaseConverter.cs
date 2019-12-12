namespace TagsCloudContainer.Core.TextHandler.WordConverters
{
    class LowerCaseConverter : IWordConverter
    {
        public string Convert(string word) => word.ToLower();
    }
}