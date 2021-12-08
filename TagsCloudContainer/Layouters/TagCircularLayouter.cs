using System.Collections.Generic;
using System.Drawing;
using TagCloudContainerTests;
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
            (List<SimpleTag> tags, SizeF minSize, float maxScale)
        {
            SetWordsCounts(tags);
            foreach (var simpleTag in tags)
            {
                var size = GetSize(minSize, maxScale, simpleTag.Count);
                var tag = factory(simpleTag.Word);
                layouter.PutNextTag(size, tag);
            }
            return layouter.Cloud;
        }
    }
}