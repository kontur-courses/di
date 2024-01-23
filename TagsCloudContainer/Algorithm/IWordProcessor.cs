namespace TagsCloudContainer.Algorithm
{
    public interface IWordProcessor
    {
        Dictionary<string, int> CalculateFrequencyInterestingWords(string sourceFilePath, string boringFilePath);
    }
}