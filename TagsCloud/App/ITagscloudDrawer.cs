using System.Collections.Generic;
using System.Drawing;
using TagsCloud.Infrastructure;

namespace TagsCloud.App
{
    public interface ITagsCloudDrawer
    {
        Image GetTagsCloud(IEnumerable<Tag> tags);
        void SetNewLayouter(IRectanglesLayouter newLayouter);
    }
}
