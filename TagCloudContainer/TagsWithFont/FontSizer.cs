using System.Drawing;
using TagCloudContainer.FrequencyWords;

namespace TagCloudContainer.TagsWithFont
{
    public class FontSizer : IFontSizer
    {
        public IEnumerable<FontTag> GetTagsWithSize(IEnumerable<WordFrequency> tags, FontFamily font, int maxFont, int minFont)
        {
            if (maxFont <= 0 || minFont <= 0)
                throw new ArgumentNullException("sizeAvgTagSize must be > 0");
            if (minFont >= maxFont)
                throw new ArgumentNullException("fontMax must be larger than fontMin");

            var sizeList = new List<FontTag>();
            foreach (var tag in tags)
            {
                var size = (int)Math.Round(tag.Count == tags.Last().Count
                    ? (int)Math.Round((double)minFont)
                    : tag.Count / (double)tags.First().Count * (maxFont - minFont) +
                      minFont);
                sizeList.Add(new FontTag(tag.Word, size, font));
            }
            return sizeList;
        }
    }
}
