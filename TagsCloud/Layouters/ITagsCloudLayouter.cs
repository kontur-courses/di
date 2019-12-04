using System.Collections.Generic;

namespace TagsCloud.Layouters
{
    public interface ITagsCloudLayouter
    {
        void ReallocItems(List<LayoutItem> items);
    }
}
