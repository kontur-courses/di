namespace TagsCloudContainer.Interfaces;

public interface ILayouterAlgorithmFactory
{
    (Func<ILayouterAlgorithm>? provider, bool success) Build(LayouterAlgorithmSettings settings);
}