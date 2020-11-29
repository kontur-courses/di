using System;
using System.Collections.Generic;
using System.Drawing;
using RectanglesCloudLayouter.Interfaces;

namespace TagsCloudContainer.WordTagsCloud
{
    public class WordTagsLayouter
    {
        private ICloudLayouter _cloudLayouter;

        public WordTagsLayouter(ICloudLayouter cloudLayouter)
        {
            _cloudLayouter = cloudLayouter;
        }

        public IEnumerable<WordTag> GetWordTags(Dictionary<string, int> wordsAndFrequency, Font font)
        {
            var list = new List<WordTag>();
            foreach (var word in wordsAndFrequency.Keys)
            {
                var wordFont = new Font(font.FontFamily, font.Size * wordsAndFrequency[word]);
                var wordSize = GetWordSize(Graphics.FromHwnd(IntPtr.Zero).MeasureString(word, wordFont));
                var rectangle = _cloudLayouter.PutNextRectangle(wordSize);
                list.Add(new WordTag(word, rectangle, wordFont));
            }

            return list;
        }

        private static Size GetWordSize(SizeF symbolSize) =>
            new Size((int) Math.Ceiling(symbolSize.Width), (int) Math.Ceiling(symbolSize.Height));
    }
}