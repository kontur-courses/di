using TagsCloudContainer.Infrastructure;

namespace TagsCloudContainer.App.FrequencyDictionaryGenerator
{
    internal class ToLowerWordNormalizer : IWordNormalizer
    {
        public string NormalizeWord(string word)
        {
            return word.ToLower();
        }
    }
}