namespace TagCloud.TextAnalyzer.WordNormalizer
{
    public class WordNormalizer : IWordNormalizer
    {
        public string Normalize(string word)
        {
            return word.ToLower();
        }
    }
}