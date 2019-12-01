using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using TagsCloudContainer.CloudVisualizers;
using TagsCloudContainer.TextParsing.CloudParsing;

namespace TagsCloudContainer.CloudLayouters
{
    public class CloudLayouter
    {
        private CloudLayouterSettings settings;
        
        public CloudLayouter(CloudLayouterSettings settings)
        {
            this.settings = settings;
        }

        public IEnumerable<CloudVisualizationWord> GetWords(IEnumerable<CloudWord> cloudWords)
        {
            var ordered = cloudWords.OrderByDescending(w => w.Count);
            double maxCount = ordered.First().Count;
            double minCount = ordered.Last().Count;
            var countDifference = maxCount - minCount == 0 ? 1 : maxCount - minCount;
            var sizeMultiplier = settings.RectangleSquareMultiplier/countDifference;
            foreach (var word in ordered)
            {
                var length = word.Word.Length;
                var ratio = word.Count * sizeMultiplier / length;
                var width = (int)ratio;
                var height = (int)(word.Count * sizeMultiplier - ratio);
                var size = new Size(width, height);
                var rect = settings.Algorithm.PutNextRectangle(size);
                yield return new CloudVisualizationWord(rect, word.Word);
            }
        }
    }
}