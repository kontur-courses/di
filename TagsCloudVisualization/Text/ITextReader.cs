using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace TagsCloudVisualization.Text
{
    public interface ITextReader
    {
        IEnumerable<string> GetAllWords(string filepath);
    }
}