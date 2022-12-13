using TagsCloudContainer.Interfaces;

namespace TagsCloudContainer;

public class CircularCloudLayouterFactory : ILayouterAlgorithmFactory
{
    public ILayouterAlgorithmProvider Build(LayouterAlgorithmSettings settings)
    {
        if (settings is not CircularLayouterAlgorithmSettings circularLayouterAlgorithmSettings)
            return new EmptyLayouterAlgorithmProvider();
        return new CircularCloudLayouterProvider(circularLayouterAlgorithmSettings);
    }

    private class CircularCloudLayouterProvider : ILayouterAlgorithmProvider
    {
        private readonly CircularLayouterAlgorithmSettings circularLayouterAlgorithmSettings;

        public CircularCloudLayouterProvider(CircularLayouterAlgorithmSettings circularLayouterAlgorithmSettings)
        {
            this.circularLayouterAlgorithmSettings = circularLayouterAlgorithmSettings;
        }

        public ILayouterAlgorithm Provide()
        {
            return new CircularLayouterAlgorithm(circularLayouterAlgorithmSettings);
        }

        public bool CanProvide => true;
    }
}