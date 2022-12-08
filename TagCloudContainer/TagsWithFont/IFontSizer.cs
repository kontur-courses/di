using System.Drawing;
using TagCloudContainer.FrequencyWords;

namespace TagCloudContainer.TagsWithFont
{
    internal interface IFontSizer
    {
        IEnumerable<FontTag> GetTagsWithSize(IEnumerable<WordFrequency> tags, FontFamily font, int maxFont, int minFont);
    }
}
