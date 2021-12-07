using System.Collections.Generic;

namespace TagsCloudContainer.Layout
{
    public interface ITagsCloudLayouter
    {
        CloudLayout GetCloudLayout(IEnumerable<string> words);
    }
}