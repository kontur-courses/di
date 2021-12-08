using TagsCloudVisualization.Drawable.Tags.Settings.TagColorGenerator;

namespace TagsCloudVisualization.Drawable.Tags.Settings
{
    public interface ITagDrawableSettingsProvider
    {
        FontSettings Font { get; }
        ITagColorGenerator ColorGenerator { get; }
    }
}