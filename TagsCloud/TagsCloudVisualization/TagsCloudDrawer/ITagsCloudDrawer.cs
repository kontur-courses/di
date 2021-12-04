using System.Collections.Generic;
using System.Drawing;

namespace TagsCloudVisualization.TagsCloudDrawer
{
    public interface ITagsCloudDrawer
    {
        void Draw(Image bitmap, IEnumerable<Tag> tags);
    }
}