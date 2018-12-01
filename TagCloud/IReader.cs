using System.Collections.Generic;
using System.Drawing;

namespace TagCloud
{
    public interface IReader
    {
        IEnumerable<Size> Read(string[] lines);
    }
}