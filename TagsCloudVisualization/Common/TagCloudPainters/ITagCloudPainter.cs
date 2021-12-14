using System.Collections.Generic;
using System.Drawing;
using TagsCloudVisualization.Common.Tags;

namespace TagsCloudVisualization.Common.TagCloudPainters
{
    public interface ITagCloudPainter
    {
        public Bitmap Paint(IEnumerable<Tag> tags);
    }
}