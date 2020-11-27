namespace TagCloud.Core.WordNormalizers
{
    public interface IWordNormalizer
    {
        public string Normalize(string word);
    }
}