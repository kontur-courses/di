using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using TagsCloudContainer.CloudVisualizers;
using TagsCloudContainer.TextParsing.CloudParsing;

namespace TagsCloudContainer.CloudLayouters
{
    public class CloudLayouter : ICloudLayouter
    {
        private Func<CloudLayouterSettings> settingsFactory;
        
        public CloudLayouter(Func<CloudLayouterSettings> settingsFactory)
        {
            this.settingsFactory = settingsFactory;
        }

        public IEnumerable<CloudVisualizationWord> GetWords(IEnumerable<CloudWord> cloudWords)
        {
            var settings = settingsFactory();
            var ordered = cloudWords.OrderByDescending(w => w.Count);
            var sizeMultiplier = GetSizeMultiplier(ordered, settings);
            foreach (var word in ordered)
            {
                var size = GetSize(word, sizeMultiplier);
                var rect = settings.Algorithm.PutNextRectangle(size);
                yield return new CloudVisualizationWord(rect, word.Word);
            }
        }

        private static Size GetSize(CloudWord word, double sizeMultiplier)
        {
            var length = word.Word.Length;
            var ratio = word.Count * sizeMultiplier / length;
            var width = (int) (word.Count * sizeMultiplier - ratio);
            width = width == 0 ? 1 : width;
            var height = (int) ratio == 0 ? 1 : (int)ratio;
            return new Size(width, height);
        }

        private double GetSizeMultiplier(IOrderedEnumerable<CloudWord> ordered, CloudLayouterSettings settings)
        {
            var maxCount = (double)ordered.First().Count;
            var minCount = (double)ordered.Last().Count;
            var countDifference = maxCount - minCount == 0 ? 1 : maxCount - minCount;
            return settings.RectangleSquareMultiplier / countDifference;
        }
    }
}