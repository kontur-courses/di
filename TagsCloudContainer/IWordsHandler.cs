namespace TagsCloudContainer;

public interface IWordsHandler
{
    public Dictionary<string, int> WordDistribution { get; }
}