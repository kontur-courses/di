using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using TagsCloudContainer.CloudLayouter;
using TagsCloudContainer.Common;

namespace TagsCloudContainer.TextAnalyzing
{
    public class TagCreator
    {
        private readonly ICloudLayouter cloudLayouter;
        private readonly ImageSettings imageSettings;
        private readonly TextAnalyzer textAnalyzer;

        public TagCreator(ImageSettings imageSettings, ICloudLayouter cloudLayouter, TextAnalyzer textAnalyzer)
        {
            this.imageSettings = imageSettings;
            this.cloudLayouter = cloudLayouter;
            this.textAnalyzer = textAnalyzer;
        }

        public IEnumerable<Tag> GetTagsForVisualization()
        {
            using (cloudLayouter)
            {
                var wordsWithFrequency = textAnalyzer.GetWordWithFrequency();
                var minFrequency = wordsWithFrequency.Min(wordWithFrequency => wordWithFrequency.Value);
                var maxFrequency = wordsWithFrequency.Max(wordWithFrequency => wordWithFrequency.Value);
                foreach (var wordWithFrequency in wordsWithFrequency)
                {
                    var fontSize = GetFontSizeForWord(minFrequency, maxFrequency, wordWithFrequency.Value,
                        wordWithFrequency.Key);
                    if (fontSize < imageSettings.MinFontSize)
                        continue;
                    var rectangleSize = new Size(wordWithFrequency.Key.Length * fontSize, fontSize * 2);
                    yield return new Tag(cloudLayouter.PutNextRectangle(rectangleSize), wordWithFrequency.Key,
                        fontSize);
                }
            }
        }

        public int GetFontSizeForWord(int minFrequency, int maxFrequency, int wordFrequency, string word)
        {
            var fontSize = wordFrequency > minFrequency
                ? imageSettings.MaxFontSize * (wordFrequency - minFrequency) / (maxFrequency - minFrequency)
                : 1;
            if (fontSize == 0)
                fontSize = 1;
            return fontSize;
        }
    }
}