using TagsCloudContainer.ColorMappers;

namespace TagsCloudContainer.Settings
{
    public interface IWordColorMapperSettings
    {
        IWordColorMapper ColorMapper { get; }
    }
}