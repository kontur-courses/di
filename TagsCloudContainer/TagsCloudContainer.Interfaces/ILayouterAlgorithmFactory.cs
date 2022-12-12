namespace TagsCloudContainer.Interfaces;

public interface ILayouterAlgorithmFactory
{
    ILayouterAlgorithmProvider Build(LayouterAlgorithmSettings settings);
}