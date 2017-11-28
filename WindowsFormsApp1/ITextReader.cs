using System.Collections.Generic;

namespace WindowsFormsApp1
{
    public interface ITextReader
    {
        IEnumerable<string> Read(string path);
    }
}