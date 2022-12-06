using System;
using System.Collections.Generic;
using System.Text;

namespace TagsCloudContainer
{
    public interface IReadTags
    {
        public Dictionary<string, int> ReadTagsFromFile(string path);
    }
}
