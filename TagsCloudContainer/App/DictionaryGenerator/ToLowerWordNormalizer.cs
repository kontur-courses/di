using TagsCloudContainer.Infrastructure;

namespace TagsCloudContainer.App.DictionaryGenerator
{
    internal class ToLowerWordNormalizer : IWordNormalizer
    {
        public string NormalizeWord(string word)
        {
            return word.ToLower();
        }
    }
}