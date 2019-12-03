using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using TagsCloudTextPreparation;
using TagsCloudVisualization.Layouters;
using TagsCloudVisualization.Styling.WordSizeCalculators;

namespace TagsCloudVisualization
{
    public class SpiralCloudLayouter : ICloudLayouter
    {
        protected readonly List<RectangleF> arrangedRectangles = new List<RectangleF>();
        protected readonly List<Tag> arrangedTags = new List<Tag>();
        protected readonly IEnumerator<PointF> spiralEnumerator;

        public SpiralCloudLayouter(Spiral spiral)
        {
            if (spiral == null)
                throw new ArgumentException("Tag Cloud spiral can't be null.");

            spiralEnumerator = spiral.GetEnumerator();
        }

        public RectangleF PutNextRectangle(SizeF rectangleSize)
        {
            if (rectangleSize.Height <= 0 || rectangleSize.Width <= 0)
                throw new ArgumentException("Tag Cloud tag size parameters should be positive.");

            var tempRect = new RectangleF(spiralEnumerator.Current, rectangleSize);
            while (arrangedRectangles.Any(r => r.IntersectsWith(tempRect)) && spiralEnumerator.MoveNext())
                tempRect.Location = spiralEnumerator.Current;
            arrangedRectangles.Add(tempRect);
            return tempRect;
        }

        public Tag PutNextTag(FrequencyWord word, SizeF wordSize)
        {
            if (wordSize.Height <= 0 || wordSize.Width <= 0)
                throw new ArgumentException("Tag Cloud tag size parameters should be positive.");

            var tempTag = new Tag(word.Word, word.Count, wordSize, spiralEnumerator.Current);
            while (arrangedTags.Any(r => r.Area.IntersectsWith(tempTag.Area)) && spiralEnumerator.MoveNext())
                tempTag.Location = spiralEnumerator.Current;
            arrangedTags.Add(tempTag);
            return tempTag;
        }
    }
}