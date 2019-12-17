using System.Collections.Generic;
using System.Linq;
using SyntaxTextParser;

namespace TagsCloudGenerator
{
    public class TagOrderByAscending : ITagOrder
    {
        public List<TextElement> OrderEnumerable(List<TextElement> elements)
        {
            return elements.OrderBy(x => x.Count).ToList();
        }
    }
}