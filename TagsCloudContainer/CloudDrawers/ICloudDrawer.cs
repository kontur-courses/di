using System.Collections.Generic;
using TagsCloudContainer.Tags;

namespace TagsCloudContainer.CloudDrawers
{
    public interface ICloudDrawer
    {
        void Draw(IEnumerable<Tag> tagsCloud);
    }
}