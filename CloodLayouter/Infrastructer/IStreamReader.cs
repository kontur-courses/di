using System.Collections.Generic;

namespace CloodLayouter.Infrastructer
{
    public interface IStreamReader
    {
        IEnumerable<string> Read(string filename);
    }
}
