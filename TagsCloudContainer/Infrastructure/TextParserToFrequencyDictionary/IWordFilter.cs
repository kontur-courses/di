namespace TagsCloudContainer.Infrastructure.TextParserToFrequencyDictionary
{
    internal interface IWordFilter
    {
        public bool IsBoring(string word);
    }
}