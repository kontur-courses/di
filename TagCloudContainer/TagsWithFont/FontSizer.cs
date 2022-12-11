using TagCloudContainer.FrequencyWords;

namespace TagCloudContainer.TagsWithFont
{
    public class FontSizer : IFontSizer
    {
        public IEnumerable<FontTag> GetTagsWithSize(IEnumerable<WordFrequency> tags, IFontSettings settings)
        {
            if (settings.MaxFont <= 0 || settings.MinFont <= 0)
                throw new ArgumentNullException("sizeAvgTagSize must be > 0");
            if (settings.MinFont >= settings.MaxFont)
                throw new ArgumentNullException("fontMax must be larger than fontMin");

            var sizeList = new List<FontTag>();
            foreach (var tag in tags)
            {
                var size = (int)Math.Round(tag.Count == tags.Last().Count
                    ? (int)Math.Round((double)settings.MinFont)
                    : tag.Count / (double)tags.First().Count * (settings.MaxFont - settings.MinFont) +
                      settings.MinFont);
                sizeList.Add(new FontTag(tag.Word, size, settings.Font));
            }
            return sizeList;
        }
    }
}
