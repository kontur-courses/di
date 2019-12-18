using System.Collections.Generic;
using System.Linq;
using SyntaxTextParser;

namespace TagsCloudGenerator
{
    public class TagOrderByDescending : ITagOrder
    {
        public List<TextElement> OrderEnumerable(List<TextElement> elements)
        {
            return elements.OrderByDescending(x => x.Count).ToList();
        }
    }
}