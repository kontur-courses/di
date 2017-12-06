using System.Collections.Generic;

namespace TagCloud
{
    public interface IMystemShell
    {
        IEnumerable<string> Analyze(string filePath);
    }
}