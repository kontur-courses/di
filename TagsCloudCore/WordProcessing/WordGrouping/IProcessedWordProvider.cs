namespace TagsCloudCore.WordProcessing.WordGrouping;

public interface IProcessedWordProvider
{
    public Dictionary<string, int> ProcessedWords { get; }
}