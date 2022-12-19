using TagCloudContainer.FrequencyWords;
using TagCloudContainer.Interfaces;
using TagCloudContainer.Models;

namespace TagCloudContainer.TagsWithFont
{
    public class FontSizer : IFontSizer
    {
        public IEnumerable<TagWithFont> GetTagsWithSize(IEnumerable<TagWithFrequency> tags, IFontSettings settings)
        {
            if (settings.MaxFont <= 0 || settings.MinFont <= 0)
                throw new ArgumentException("Incorrect font size");
            if (settings.MinFont >= settings.MaxFont)
                throw new ArgumentException(nameof(settings.MaxFont) + " must be lower than " + nameof(settings.MinFont));

            var sizeList = new List<TagWithFont>();
            foreach (var tag in tags)
            {
                var size = (int)Math.Round(tag.Count == tags.Last().Count
                    ? (int)Math.Round((double)settings.MinFont)
                    : tag.Count / (double)tags.First().Count * (settings.MaxFont - settings.MinFont) +
                      settings.MinFont);

                sizeList.Add(new TagWithFont(tag.Word, size, settings.Font));
            }
            return sizeList;
        }
    }
}
