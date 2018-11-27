using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace TagsCloudVisualization
{
    public class TagCloudLayouter
    {
        private readonly CircularCloudLayouter layouter;
        private readonly Dictionary<String, int> weightedWords;
        public readonly int MaxFontSize = 50;
        public readonly int MinFontSize = 10;
        private readonly int maxFrequency;
        private readonly int minFrequency;

        public TagCloudLayouter(CircularCloudLayouter layouter, Dictionary<String, int> weightedWords)
        {
            this.layouter = layouter;
            this.weightedWords = weightedWords;
            maxFrequency = weightedWords.Values.Max();
            minFrequency = weightedWords.Values.Min();
        }

        public List<Tag> GetTags()
        {
            var tags = new List<Tag>();
            foreach (var weightedWord in weightedWords)
            {
                var fontSize = GetFontSize(weightedWord.Value);
                var font = new Font(FontFamily.GenericSansSerif, fontSize);
                var frameSize = TextRenderer.MeasureText(weightedWord.Key, font);
                var frame = layouter.PutNextRectangle(frameSize);
                tags.Add(new Tag(weightedWord.Key, font, frame));
            }
            return tags;
        }

        private int GetFontSize(int currentWeight)
        {
            var fontScaler = maxFrequency == minFrequency ? 1
                : (double)(currentWeight - minFrequency) / (maxFrequency - minFrequency + 1);
            return (int)Math.Round(MinFontSize + fontScaler * (MaxFontSize - MinFontSize));
        }

    }
}
