namespace TagsCloudContainer.Interfaces;

public interface ILayouterAlgorithmProvider
{
    bool CanProvide { get; }
    ILayouterAlgorithm Provide();
}