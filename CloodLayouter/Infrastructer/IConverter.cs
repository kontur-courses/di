using System.Collections.Generic;

namespace CloodLayouter.Infrastructer
{
    public interface IConverter
    {
        List<Tag> Convert();
    }
}