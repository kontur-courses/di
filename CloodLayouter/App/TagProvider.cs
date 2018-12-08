using System.Collections.Generic;
using CloodLayouter.Infrastructer;

namespace CloodLayouter.App
{
    public class TagProvider : ITagProvider
    {
        public List<Tag> Tags { get; set; } = new List<Tag>();
    }
}