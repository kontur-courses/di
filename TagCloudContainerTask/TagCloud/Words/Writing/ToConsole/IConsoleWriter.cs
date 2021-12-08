using System.Collections.Generic;

namespace TagCloud.Words.Writing.ToConsole
{
    public interface IConsoleWriter
    {
        void WriteToConsole(IEnumerable<string> linesToWrite);
        void TypeToConsole();
    }
}