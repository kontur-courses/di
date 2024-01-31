namespace TagCloudDi.TextProcessing
{
    public interface ITextReader
    {
        IEnumerable<string> GetWordsFrom(string resource);
    }
}
