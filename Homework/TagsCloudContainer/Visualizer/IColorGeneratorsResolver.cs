namespace TagsCloudContainer.Visualizer
{
    public interface IColorGeneratorsResolver
    {
        public IColorGenerator Get(PalleteType palleteType);
    }
}