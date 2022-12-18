namespace TagsCloudContainer;

public interface IWordsHandler
{
    public Result<Dictionary<string, int>> WordDistribution { get; }
}