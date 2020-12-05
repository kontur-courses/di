namespace TagsCloud.App
{
    public class WordNormalizer : IWordNormalizer
    {
        public string NormalizeWord(string word)
        {
            return word.ToLower();
        }
    }
}