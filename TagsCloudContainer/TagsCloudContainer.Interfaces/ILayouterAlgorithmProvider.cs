namespace TagsCloudContainer.Interfaces;

public interface ILayouterAlgorithmProvider
{
    ILayouterAlgorithm Provide();
    
    bool CanProvide { get; }
}