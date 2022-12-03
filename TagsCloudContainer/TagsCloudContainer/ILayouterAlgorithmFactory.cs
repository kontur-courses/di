using TagsCloudContainer.Interfaces;

namespace TagsCloudContainer;

public interface ILayouterAlgorithmFactory
{
    (Func<ILayouterAlgorithm>? provider, bool success) Build(LayouterAlgorithmSettings settings);
}