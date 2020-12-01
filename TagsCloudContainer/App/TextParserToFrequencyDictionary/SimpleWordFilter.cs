using TagsCloudContainer.Infrastructure.TextParserToFrequencyDictionary;

namespace TagsCloudContainer.App.TextParserToFrequencyDictionary
{
    internal class SimpleWordFilter : IWordFilter
    {
        public bool IsBoring(string word)
        {
            return word.Length < 4;
        }
    }
}