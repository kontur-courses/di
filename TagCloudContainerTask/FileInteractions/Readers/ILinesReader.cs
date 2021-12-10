using System.Collections.Generic;

namespace FileInteractions.Readers
{
    public interface ILinesReader
    {
        IEnumerable<string> ReadLines();
    }
}