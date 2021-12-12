namespace TagsCloudContainer.Visualizer.ColorGenerators
{
    public interface IColorGeneratorsResolver
    {
        public IColorGenerator Get(PalleteType palleteType);
    }
}