using System;
using System.IO;

namespace TagCloud
{
    public interface ITextRiderConfig
    {
        string FilePath { get; }
        Func<string, string> WordFormat { get; }
        Func<string, bool> WordFilter { get; }
    }
}