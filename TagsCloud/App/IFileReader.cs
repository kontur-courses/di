using System.Collections.Generic;

namespace TagsCloud.App
{
    public interface IFileReader
    {
        HashSet<string> AvailableFileTypes { get; }
        string[] ReadLines(string fileName);
    }
}