using System.Collections.Generic;
using TagsCloudContainer.DependencyInjection;

namespace TagsCloudContainer.Layout
{
    public interface ITagsCloudLayouter : IService<TagsCloudLayouterType>
    {
        CloudLayout GetCloudLayout(IEnumerable<string> words);
    }
}