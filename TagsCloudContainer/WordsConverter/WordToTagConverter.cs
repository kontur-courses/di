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

        public IEnumerable<Tag> ConvertWords(IEnumerable<string> words)
        {
            var wordsFrequency = GetWordsFrequency(words).OrderByDescending(pair => pair.Value);
            var maxFrequency = wordsFrequency.First().Value;
            using var graphics = Graphics.FromHwnd(new IntPtr());
            var tags = new List<Tag>();

            foreach (var (word, frequency) in wordsFrequency)
            {
                var tagFont = GetFont(frequency, maxFrequency);
                var tagRectangleSize = Size.Ceiling(graphics.MeasureString(word, tagFont));
                var tagRectangle = cloudLayouter.PutNextRectangle(tagRectangleSize);
                tags.Add(new Tag(word, tagRectangle, tagFont));
            }

            return tags;
        }

        private Font GetFont(int wordFrequency, int maxFrequency)
        {
            var fontName = fontCreator.FontName;
            var fontSize = fontCreator.GetFontSize(wordFrequency, maxFrequency);
            return new Font(fontName, fontSize);
        }

        private static Dictionary<string, int> GetWordsFrequency(IEnumerable<string> words)
        {
            var frequency = new Dictionary<string, int>();

            foreach (var word in words)
            {
                if (frequency.ContainsKey(word))
                    frequency[word] += 1;
                else
                    frequency[word] = 1;
            }

            return frequency;
        }
    }
}