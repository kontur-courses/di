using TagCloudContainer.FrequencyWords;

namespace TagCloudContainer.TagsWithFont
{
    public interface IFontSizer
    {
        IEnumerable<FontTag> GetTagsWithSize(IEnumerable<WordFrequency> tags, IFontSettings settings);
    }
}
