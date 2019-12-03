using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TagCloud.Models;

namespace TagCloud
{
    public class TagCollection
    {
        public List<Tag> Tags { get; }

        public TagCollection(List<Tag> tags)
        {
            Tags = tags;
        }
    }
}
