using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using TagsCloud.CloudStructure;

namespace TagsCloud.TagsCloudVisualization
{
    public class TagCloudLayouter: ITagCloudLayouter
    {
        private readonly FontFamily fontFamily;
        public readonly ICloudLayouter cloud;
        public readonly int MinFontSize;
        public readonly int MaxFontSize;

        public TagCloudLayouter(FontFamily fontFamily, ICloudLayouter cloud, int minFontSize = 10, int maxFontSize = 100)
        {
            this.fontFamily = fontFamily;
            this.cloud = cloud;
            MinFontSize = minFontSize;
            MaxFontSize = maxFontSize;
        }

        public List<Tag> GetTags(Dictionary<string, int> wordFrequency)
        {
            var tags = new List<Tag>();
            var maxFrequency = wordFrequency.Values.Max();
            var minFrequency = wordFrequency.Values.Min();
            foreach (var item in wordFrequency)
            {
                var fontSize = GetFontSize(maxFrequency, minFrequency, item.Value);
                var size = TextRenderer.MeasureText(item.Key, new Font(fontFamily, fontSize));
                var posRectangle = cloud.PutNextRectangle(size);
                tags.Add(new Tag(posRectangle, item.Key, fontSize, item.Value));
            }
            return tags;
        }

        private int GetFontSize(int maxFrequency, int minFrequency, int frequency)
        {
            var dFrequency = (maxFrequency - minFrequency);
            if (dFrequency == 0)
                dFrequency = maxFrequency;
            return MaxFontSize - (MaxFontSize - MinFontSize) * (maxFrequency - frequency) / dFrequency;
        }
    }
}
