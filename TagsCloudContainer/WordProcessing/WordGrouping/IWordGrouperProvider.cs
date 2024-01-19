namespace TagsCloudContainer.WordProcessing.WordGrouping;

public interface IWordGrouperProvider
{
    public Dictionary<string, int> GrouppedWords { get; }
}