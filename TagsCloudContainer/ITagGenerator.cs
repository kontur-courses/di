using System.Collections.Generic;
using System.Drawing;

namespace TagsCloudContainer
{
    public interface ITagGenerator
    {
        public Dictionary<string, Rectangle> GetTags(Dictionary<string, int> words);
    }
}