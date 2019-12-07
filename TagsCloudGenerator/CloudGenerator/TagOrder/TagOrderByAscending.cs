using System.Collections.Generic;
using System.Linq;
using SyntaxTextParser;

namespace TagsCloudGenerator
{
    public class TagOrderByAscending : ITagOrder
    {
        public List<CountedTextElement> OrderEnumerable(List<CountedTextElement> elements)
        {
            return elements.OrderBy(x => x.Count).ToList();
        }
    }
}