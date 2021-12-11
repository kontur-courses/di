using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using TagsCloudContainer.CloudLayouter;

namespace TagsCloudContainer.WordsConverter
{
    public class WordToTagConverter : IWordConverter
    {
        private readonly ICloudLayouter cloudLayouter;
        private readonly IFontCreator fontCreator;

        public WordToTagConverter(ICloudLayouter cloudLayouter, IFontCreator fontCreator)
        {
            this.cloudLayouter = cloudLayouter;
            this.fontCreator = fontCreator;
        }

        public IEnumerable<Tag> ConvertWords(List<string> words)
        {
            var wordsFrequency = GetWordsFrequency(words).OrderByDescending(pair => pair.Value).ToList();
            var maxFrequency = wordsFrequency.First().Value;
            using var graphics = Graphics.FromHwnd(IntPtr.Zero);

            foreach (var (word, frequency) in wordsFrequency)
            {
                var tagFont = GetFont(frequency, maxFrequency);
                var tagRectangleSize = Size.Ceiling(graphics.MeasureString(word, tagFont));
                var tagRectangle = cloudLayouter.PutNextRectangle(tagRectangleSize);
                yield return new Tag(word, tagRectangle, tagFont);
            }
        }

        private Font GetFont(int wordFrequency, int maxFrequency)
        {
            var fontName = fontCreator.FontName;
            var fontSize = fontCreator.GetFontSize(wordFrequency, maxFrequency);
            return new Font(fontName, fontSize);
        }

        private static Dictionary<string, int> GetWordsFrequency(IEnumerable<string> words)
        {
            return words.GroupBy(word => word)
                .ToDictionary(group => group.Key, group => group.Count());
        }
    }
}