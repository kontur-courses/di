using System.Collections.Generic;
using TagCloud.Models;

namespace TagCloud
{
    public class TagCollection
    {
        public TagCollection(List<Tag> tags)
        {
            Tags = tags;
        }

        public List<Tag> Tags { get; }
    }
}