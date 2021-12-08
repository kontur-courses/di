using System.Collections.Generic;

namespace TagCloud.Words.Reading.Console
{
    public interface IConsoleReader
    {
        IEnumerable<string> ReadFromConsole();
    }
}