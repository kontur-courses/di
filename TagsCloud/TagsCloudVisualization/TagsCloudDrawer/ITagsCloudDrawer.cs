using System.Collections.Generic;
using System.Drawing;

namespace TagsCloudVisualization.TagsCloudDrawer
{
    public interface ITagsCloudDrawer
    {
        void Draw(Graphics graphics, Size size, IEnumerable<Tag> tags);
    }
}