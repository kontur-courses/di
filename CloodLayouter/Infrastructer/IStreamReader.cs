using System.Collections.Generic;

namespace CloodLayouter.Infrastructer
{
    public interface IStreamReader
    {
        List<string> Read();
    }
}