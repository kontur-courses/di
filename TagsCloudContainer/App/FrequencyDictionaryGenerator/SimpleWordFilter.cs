using TagsCloudContainer.Infrastructure.DictionaryGenerator;

namespace TagsCloudContainer.App.FrequencyDictionaryGenerator
{
    internal class SimpleWordFilter : IWordFilter
    {
        public bool IsBoring(string word)
        {
            return word.Length < 4;
        }
    }
}