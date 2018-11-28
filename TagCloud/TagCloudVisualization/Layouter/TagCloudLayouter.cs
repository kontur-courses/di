using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using TagCloud.Settings;
using TagCloud.TagCloudVisualization.Analyzer;

namespace TagCloud.TagCloudVisualization.Layouter
{
    public class TagCloudLayouter
    {
        private readonly CircularCloudLayouter layouter;
        private readonly FontSettings fontSettings;
        private int minFrequency;
        private int maxFrequency;

        public TagCloudLayouter(FontSettings fontSettings)
        {
            this.fontSettings = fontSettings;
            layouter = new CircularCloudLayouter();
        }

        public List<Tag> GetTags(Dictionary<String, int> weightedWords)
        {
            layouter.Clear();
            minFrequency = weightedWords.Values.Min();
            maxFrequency = weightedWords.Values.Max();
            var tags = new List<Tag>();
            foreach (var weightedWord in weightedWords)
            {
                var fontSize = GetFontSize(weightedWord.Value);
                var font = new Font(fontSettings.FontFamily, fontSize);
                var frameSize = TextRenderer.MeasureText(weightedWord.Key, font);
                var frame = layouter.PutNextRectangle(frameSize);
                tags.Add(new Tag(weightedWord.Key, font, frame));
            }
            return tags;
        }

        private int GetFontSize(int currentWeight)
        {
            var medianFrequency = maxFrequency - minFrequency;
            var medianFontSize = fontSettings.MaxFontSize - fontSettings.MinFontSize;
            var fontScaler = maxFrequency == minFrequency ? 1 : (double)currentWeight / medianFrequency;
            return (int)Math.Round(fontSettings.MinFontSize + fontScaler * medianFontSize);
        }

    }
}
