using TagCloudContainer.FrequencyWords;
using TagCloudContainer.Models;

namespace TagCloudContainer.Interfaces
{
    public interface IFontSizer
    {
        IEnumerable<TagWithFont> GetTagsWithSize(IEnumerable<TagWithFrequency> tags, IFontSettings settings);
    }
}
