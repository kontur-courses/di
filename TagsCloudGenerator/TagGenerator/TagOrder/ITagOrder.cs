using System.Collections.Generic;
using SyntaxTextParser;

namespace TagsCloudGenerator
{
    public interface ITagOrder
    {
        List<TextElement> OrderEnumerable(List<TextElement> elements);
    }
}