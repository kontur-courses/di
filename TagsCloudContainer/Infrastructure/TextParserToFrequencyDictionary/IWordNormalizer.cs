namespace TagsCloudContainer.Infrastructure.TextParserToFrequencyDictionary
{
    internal interface IWordNormalizer
    {
        public string NormalizeWord(string word);
    }
}