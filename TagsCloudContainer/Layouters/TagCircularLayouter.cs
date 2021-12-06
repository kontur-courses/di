using System.Collections.Generic;
using System.Drawing;
using TagsCloudVisualization;
using TagsCloudVisualization.Interfaces;

namespace TagsCloudContainer.Layouters
{
    public class TagCircularLayouter : TagLayouter
    {
        public TagCircularLayouter(ICloudLayouter layouter, Tag.Factory factory) : 
            base(layouter, factory)
        {
        }

        public override ICloud<ITag> PlaceTagsInCloud
            (Dictionary<string, int> wordsCount, SizeF minSize, float maxScale)
        {
            SetWordsCounts(wordsCount);
            foreach (var word in wordsCount.Keys)
            {
                var size = GetSize(minSize, maxScale, wordsCount[word]);
                var tag = factory(word);
                layouter.PutNextTag(size, tag);
            }
            return layouter.Cloud;
        }
    }
}