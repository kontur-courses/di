using TagsCloudContainer.Infrastructure.TextParserToFrequencyDictionary;

namespace TagsCloudContainer.App.TextParserToFrequencyDictionary
{
    internal class ToLowerWordNormalizer : IWordNormalizer
    {
        public string NormalizeWord(string word)
        {
            return word.ToLower();
        }
    }
}