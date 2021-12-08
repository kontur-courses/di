using System.Collections.Generic;
using System.Drawing;
using TagsCloudContainerCore.InterfacesCore;
using TagsCloudVisualization;

namespace TagsCloudContainerCore
{
    public class CloudLayouter : ICloudLayouter
    {
        private readonly IStatisticMaker statisticMaker;
        private readonly CircularCloudLayouter cloudLayouter;

        // ReSharper disable once FieldCanBeMadeReadOnly.Local
        private string fontName;

        // ReSharper disable once FieldCanBeMadeReadOnly.Local
        private float maxFontSize;
        private readonly Graphics graphics;

        public void AddTags(IEnumerable<string> tags)
        {
            foreach (var tag in tags)
            {
                statisticMaker.AddTag(tag);
            }
        }

        private float GetFontSizeForTag(KeyValuePair<string, int> tag)
            => maxFontSize * (tag.Value - statisticMaker.GetMaxTag().Value) /
               (statisticMaker.GetMaxTag().Value - statisticMaker.GetMinTag().Value);

        public IEnumerator<TagToRender> GetWordsToRender()
        {
            // ReSharper disable once LoopCanBeConvertedToQuery
            foreach (var tag in statisticMaker.CountedTags)
            {
                var fontSize = GetFontSizeForTag(tag);
                var font = new Font(fontName, fontSize);
                var tagToRender = new TagToRender(tag.Key, graphics, font);
                var location = cloudLayouter.PutNextRectangle(tagToRender.TagSize).Location;
                tagToRender.Location = location;
                
                yield return tagToRender;
            }
        }

        public CloudLayouter(
            IStatisticMaker statisticMaker,
            CircularCloudLayouter cloudLayouter,
            string fontName,
            Graphics graphics,
            float maxFontSize)
        {
            this.graphics = graphics;
            this.cloudLayouter = cloudLayouter;
            this.fontName = fontName;
            this.maxFontSize = maxFontSize;
            this.statisticMaker = statisticMaker;
        }
    }
}