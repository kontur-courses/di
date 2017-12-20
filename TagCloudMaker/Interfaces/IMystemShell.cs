using System.Collections.Generic;

namespace TagCloud.Interfaces
{
    public interface IMystemShell
    {
        IEnumerable<string> Analyze(string filePath);
    }
}