using System.Collections.Generic;
using SyntaxTextParser;

namespace TagsCloudGenerator
{
    public interface ITagOrder
    {
        List<CountedTextElement> OrderEnumerable(List<CountedTextElement> elements);
    }
}