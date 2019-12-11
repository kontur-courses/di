namespace TagsCloudContainer.Core.TextHandler.WordConverters
{
    class LowerCaseConverter : IWordConverter
    {
        public string Handle(string word) => word.ToLower();
    }
}
