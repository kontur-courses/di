using TagsCloudContainer.Interfaces;

namespace TagsCloudContainer;

public class CircularCloudLayouterFactory : ILayouterAlgorithmFactory
{
    public (Func<ILayouterAlgorithm>? provider, bool success) Build(LayouterAlgorithmSettings settings)
    {
        if (settings is not CircularLayouterAlgorithmSettings circularLayouterAlgorithmSettings)
            return default;
        return (() => new CircularLayouterAlgorithm(circularLayouterAlgorithmSettings), true);
    }
}