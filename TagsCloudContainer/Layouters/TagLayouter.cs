using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using TagsCloudContainer.Common;
using TagsCloudVisualization;
using TagsCloudVisualization.Interfaces;

namespace TagsCloudContainer.Layouters
{
    public abstract class TagLayouter
    {
        protected readonly ICloudLayouter layouter;
        protected readonly Tag.Factory factory;
        protected int MinWordsCount;
        protected int MaxWordsCount;

        protected TagLayouter(ICloudLayouter layouter, Tag.Factory factory)
        {
            this.layouter = layouter;
            this.factory = factory;
        }

        public abstract ICloud<ITag> PlaceTagsInCloud
            (List<SimpleTag> tags, float minHeight, float maxScale);

        protected void SetWordsCounts(List<SimpleTag> tags)
        {
            MinWordsCount = tags.Min(t => t.Count);
            MaxWordsCount = tags.Max(t => t.Count);
        }

        protected SizeF GetSize(string word, float minHeight, float maxScale, int wordsCount)
        {
            var minWidth = GetMinWidth(word, minHeight);
            var minSize = new SizeF(minWidth, minHeight);
            if (wordsCount == MinWordsCount)
                return minSize;
            var scale = wordsCount / MinWordsCount;
            return scale > maxScale ?
                GetScaledSize(minSize, maxScale) :
                GetScaledSize(minSize, scale);
        }

        private SizeF GetScaledSize(SizeF minSize, float scale)
            => new SizeF(minSize.Width * scale, minSize.Height * scale);

        private float GetMinWidth(string word, float minHeight) 
            => word.Length * minHeight * 0.7f;
    }
}