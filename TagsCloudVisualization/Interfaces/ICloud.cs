using System.Collections.Generic;
using System.Drawing;

namespace TagsCloudVisualization.Interfaces
{
    public interface ICloud<TElement>
    {
        IReadOnlyList<TElement> Elements { get; }
        PointF Center { get; }

        internal void AddTag(ITag tag);
        public RectangleF GetCloudBoundingRectangle();
    }
}
