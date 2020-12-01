namespace TagsCloudContainer.Interfaces
{
    public interface ITextAnalyzer
    {
        string[] GetInterestingWords(string text, string[] customBoringWords);
    }
}