using TagsCloudVisualization.ColorGenerators;

namespace TagsCloudVisualization.TagsCloudDrawer.TagsCloudDrawerSettingsProvider
{
    public interface IDrawerSettingsProvider
    {
        FontSettings Font { get; }
        IColorGenerator ColorGenerator { get; }
    }
}