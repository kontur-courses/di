using System.Collections.Generic;

namespace CloodLayouter.Infrastructer
{
    public interface IConverter
    {
        IEnumerable<Tag> Convert(IEnumerable<string> strings);
    }
}