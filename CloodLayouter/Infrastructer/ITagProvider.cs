using System.Collections.Generic;

namespace CloodLayouter.Infrastructer
{
    public interface ITagProvider
    {
        List<Tag> Tags { get; set; }
    }
}