using System.Collections.Generic;
using SyntaxTextParser;

namespace TagsCloudGenerator
{
    public class RandomTagOrder : ITagOrder
    {
        public List<TextElement> OrderEnumerable(List<TextElement> elements)
        {
            return elements.ShuffleList();
        }
    }
}