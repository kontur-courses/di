namespace TagsCloudContainer.Layouter.PointsProviders
{
    public interface IPointsProvidersResolver
    {
        public IPointsProvider Get(LayoutAlrogorithm algorithm);
    }
}