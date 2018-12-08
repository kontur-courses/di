using System.Collections.Generic;

namespace CloodLayouter.Infrastructer
{
    public interface IWordProvider
    {
        List<string> Words { get; set; }
    }
}