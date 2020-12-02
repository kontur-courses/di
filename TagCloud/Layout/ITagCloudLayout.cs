using System.Collections.Generic;

namespace TagCloud.Layout
{
    public interface ITagCloudLayout
    {
        public void DrawTagCloud(IReadOnlyCollection<TagInfo> tags);
    }
}