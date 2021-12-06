using System.Collections.Generic;
using System.Drawing;
using System.Linq;
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
            (Dictionary<string, int> wordsCount, SizeF minSize, float maxScale);

        protected void SetWordsCounts(Dictionary<string, int> wordsCount)
        {
            MinWordsCount = wordsCount.Values.Min();
            MaxWordsCount = wordsCount.Values.Max();
        }

        protected SizeF GetSize(SizeF minSize, float maxScale, int wordsCount)
        {
            if (wordsCount == MinWordsCount)
                return minSize;
            var scale = wordsCount / MinWordsCount;
            return scale > maxScale ?
                GetScaledSize(minSize, maxScale) :
                GetScaledSize(minSize, scale);
        }

        protected SizeF GetScaledSize(SizeF minSize, float scale)
            => new SizeF(minSize.Width * scale, minSize.Height * scale);
    }
}