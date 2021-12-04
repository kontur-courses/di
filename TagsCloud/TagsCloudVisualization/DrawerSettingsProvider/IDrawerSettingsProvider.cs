using TagsCloudDrawer.ColorGenerators;

namespace TagsCloudVisualization.DrawerSettingsProvider
{
    public interface IDrawerSettingsProvider
    {
        FontSettings Font { get; }
        IColorGenerator ColorGenerator { get; }
    }
}