using System.Collections.Generic;

namespace TagsCloudVisualization.ImageCreator
{
    public interface ITagsCloudImageCreator
    {
        void Create(string filename, IEnumerable<Tag> tags);
    }
}