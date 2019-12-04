namespace TagCloud
{
    public interface IExtractor
    {
        string[] ExtractWords(string text);
    }
}
