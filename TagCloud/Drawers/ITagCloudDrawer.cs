using System.Collections.Generic;

namespace TagCloud.Drawers
{
    public interface ITagCloudDrawer
    {
        public void DrawTagCloud(HashSet<TagInfo> tags);
    }
}