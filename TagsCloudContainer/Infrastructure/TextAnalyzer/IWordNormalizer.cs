namespace TagsCloudContainer.Infrastructure.TextAnalyzer
{
    internal interface IWordNormalizer
    {
        public string NormalizeWord(string word);
    }
}