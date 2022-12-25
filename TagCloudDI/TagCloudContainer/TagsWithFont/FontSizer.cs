using TagCloudContainer.FrequencyWords;
using TagCloudContainer.Interfaces;
using TagCloudContainer.Models;

namespace TagCloudContainer.TagsWithFont
{
    public class FontSizer : IFontSizer
    {
        public IEnumerable<TagWithFont> GetTagsWithSize(IEnumerable<TagWithFrequency> tags, IFontSettings settings)
        {
            var sizeList = new List<TagWithFont>();
            foreach (var tag in tags)
            {
                var size = (int)Math.Round(tag.Count == tags.Last().Count
                    ? (int)Math.Round((double)settings.MinFontSize)
                    : tag.Count / (double)tags.First().Count * (settings.MaxFontSize - settings.MinFontSize) +
                      settings.MinFontSize);

                sizeList.Add(new TagWithFont(tag.Word, size, settings.Font));
            }
            return sizeList;
        }
    }
}
