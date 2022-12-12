namespace TagsCloudContainer.Interfaces;

public class EmptyLayouterAlgorithmProvider : ILayouterAlgorithmProvider
{
    public ILayouterAlgorithm Provide() => throw new NotImplementedException();

    public bool CanProvide => false;
}