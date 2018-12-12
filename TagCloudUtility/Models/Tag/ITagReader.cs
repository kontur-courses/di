using System.Collections.Generic;
using TagCloud.Models;

namespace TagCloud.Utility.Models.Tag
{
    public interface ITagReader
    {
        List<TagItem> ReadTags(IEnumerable<string> words);
    }
}