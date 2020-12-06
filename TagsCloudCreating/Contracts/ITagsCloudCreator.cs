using System.Collections.Generic;
using TagsCloudLayouters.Core.WordProcessors;

namespace TagsCloudLayouters.Contracts
{
    public interface ITagsCloudCreator
    {
        public IEnumerable<Tag> CreateTagsCloud(IEnumerable<string> words);
    }
}