namespace TagsCloudContainer.Infrastructure.DictionaryGenerator
{
    internal interface IWordFilter
    {
        public bool IsBoring(string word);
    }
}