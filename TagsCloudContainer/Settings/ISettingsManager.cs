using TagsCloudContainer.ImageCreators;

namespace TagsCloudContainer.Settings
{
    public interface ISettingsManager
    {
        IImageSettings ImageSettings { get; }
        ITextSettings TextSettings { get; }
        IPalette Palette { get; }
    }
}