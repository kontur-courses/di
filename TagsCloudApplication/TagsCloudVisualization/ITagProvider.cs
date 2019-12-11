using System.Collections.Generic;

namespace TagsCloudVisualization
{
    public interface ITagProvider
    {
        List<CloudTag> ReadCloudTags(string filePath);
    }
}
