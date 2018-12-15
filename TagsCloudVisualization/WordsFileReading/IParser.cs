using System.Collections.Generic;
using System.IO;

namespace TagsCloudVisualization.WordsFileReading
{
    public interface IParser
    {
        IEnumerable<string> ParseText(string text);
        string GetModeName();
    }
}
