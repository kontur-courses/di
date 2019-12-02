using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using TagsCloudContainer.CloudVisualizers;
using TagsCloudContainer.TextParsing.CloudParsing;

namespace TagsCloudContainer.CloudLayouters
{
    public class CloudLayouter : ICloudLayouter
    {
        private CloudLayouterSettings settings;
        
        public CloudLayouter(CloudLayouterSettings settings)
        {
            this.settings = settings;
        }

        public IEnumerable<CloudVisualizationWord> GetWords(IEnumerable<CloudWord> cloudWords)
        {
            var ordered = cloudWords.OrderByDescending(w => w.Count);
            var sizeMultiplier = GetSizeMultiplier(ordered);
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
            var height = (int) ratio;
            return new Size(width, height);
        }

        private double GetSizeMultiplier(IOrderedEnumerable<CloudWord> ordered)
        {
            var maxCount = (double)ordered.First().Count;
            var minCount = (double)ordered.Last().Count;
            var countDifference = maxCount - minCount == 0 ? 1 : maxCount - minCount;
            return settings.RectangleSquareMultiplier / countDifference;
        }
    }
}