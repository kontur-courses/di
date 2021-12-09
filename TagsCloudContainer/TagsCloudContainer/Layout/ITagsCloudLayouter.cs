using System.Collections.Generic;

namespace TagsCloudContainer.Layout
{
    public interface ITagsCloudLayouter
    {
        public CloudLayouterType Type { get; }
        CloudLayout GetCloudLayout(IEnumerable<string> words);
    }
}