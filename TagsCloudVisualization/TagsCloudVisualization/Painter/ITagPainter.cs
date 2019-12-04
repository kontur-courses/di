using System.Collections.Generic;

namespace TagsCloudVisualization
{
    public interface ITagPainter
    {
        void SetColorsForTagCollection(IEnumerable<Tag> tagCollection);
    }
}