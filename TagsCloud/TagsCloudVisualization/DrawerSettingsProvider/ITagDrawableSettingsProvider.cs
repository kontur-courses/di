using TagsCloudVisualization.DrawerSettingsProvider.TagColorGenerator;

namespace TagsCloudVisualization.DrawerSettingsProvider
{
    public interface ITagDrawableSettingsProvider
    {
        FontSettings Font { get; }
        ITagColorGenerator ColorGenerator { get; }
    }
}