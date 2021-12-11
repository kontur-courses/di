using System.Collections.Generic;

namespace App.Infrastructure.FileInteractions.Readers
{
    public interface ILinesReader
    {
        IEnumerable<string> ReadLines();
    }
}