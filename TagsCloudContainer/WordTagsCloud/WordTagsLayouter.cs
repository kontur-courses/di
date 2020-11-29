using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using RectanglesCloudLayouter.Interfaces;

namespace TagsCloudContainer.WordTagsCloud
{
    public static class WordTagsLayouter
    {
        public static IEnumerable<WordTag> GetWordTags(ICloudLayouter cloudLayouter, string[]words, Size symbolSize)
        {
            return words.Select(word => new WordTag(word, cloudLayouter.PutNextRectangle(word.GetWordSize(symbolSize))));
        }

        private static Size GetWordSize(this string word, Size symbolSize) =>
            new Size(symbolSize.Width * word.Length, symbolSize.Height);
    }
}