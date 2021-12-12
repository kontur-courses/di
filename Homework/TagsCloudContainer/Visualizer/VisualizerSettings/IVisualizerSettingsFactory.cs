using TagsCloudContainer.Visualizer.ColorGenerators;

namespace TagsCloudContainer.Visualizer.VisualizerSettings
{
    public interface IVisualizerSettingsFactory : IFactory<IVisualizerSettings>
    {
        public IColorGeneratorsResolver Resolver { get; }
    }
}