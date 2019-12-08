using System.Collections.Generic;

namespace TagsCloudVisualization.TagsCloudVisualization
{
    public interface ITagsCloudVisualization<in T>
    {   
        void Draw(Dictionary<string, int> words);
    }
}
